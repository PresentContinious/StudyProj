using System.Dynamic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using StudentHub.Core.Interfaces;
using File = Google.Apis.Drive.v3.Data.File;

namespace StudentHub.Core.Services;

public class GoogleDriveService : IDriveService
{
    private DriveService DriveService { get; }
    private string RootFileId { get; }

    public GoogleDriveService(IConfiguration configuration)
    {
        var driveAuth = configuration.GetSection("googleAuth");

        var tokenResponse = new TokenResponse
        {
            AccessToken = driveAuth["accessToken"],
            RefreshToken = driveAuth["refreshToken"]
        };

        var applicationName = driveAuth["applicationName"];
        var username = driveAuth["username"];

        var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
        {
            ClientSecrets = new ClientSecrets
            {
                ClientId = driveAuth["clientId"],
                ClientSecret = driveAuth["clientSecret"],
            },
            Scopes = new[] { DriveService.Scope.Drive },
            DataStore = new FileDataStore(applicationName)
        });

        var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = applicationName,
        });

        DriveService = service;

        if (string.IsNullOrEmpty(driveAuth["rootFileId"]))
            SaveFileIdAsync(CreateFolder("StudentHub Photos").Result).Wait();

        RootFileId = driveAuth["rootFileId"]!;
    }

    public async Task<string> CreateFolder(string folderName, params string[] parents)
    {
        var driveFolder = new File
        {
            Name = folderName,
            MimeType = "application/vnd.google-apps.folder",
            Parents = parents
        };
        var command = DriveService.Files.Create(driveFolder);
        var file = await command.ExecuteAsync();
        return file.Id;
    }

    private async Task<(string id, long? durationInMin)>
        UploadFile(Stream file, string fileName, string fileMime, string? folder = null)
    {
        folder ??= RootFileId;

        var driveFile = new File
        {
            Name = fileName,
            MimeType = fileMime,
            Parents = new[] { folder }
        };

        var request = DriveService.Files.Create(driveFile, file, fileMime);
        request.Fields = "id";

        var response = await request.UploadAsync();
        if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
            throw response.Exception;

        var durationInMin = request.ResponseBody.VideoMediaMetadata?.DurationMillis / 1000 / 60;

        return (request.ResponseBody.Id, durationInMin);
    }

    public async Task<(string id, long? durationInMin)> UploadFile(IFormFile file, string? folder = null)
    {
        return await UploadFile(file.OpenReadStream(), file.FileName, file.ContentType, folder);
    }

    public async Task<string> DeleteFile(string fileId)
    {
        var command = DriveService.Files.Delete(fileId);
        return await command.ExecuteAsync();
    }

    public async Task<IEnumerable<File>> GetFiles(string? folder)
    {
        if (folder is null)
            return Array.Empty<File>();

        var fileList = DriveService.Files.List();
        fileList.Q = $"mimeType!='application/vnd.google-apps.folder' and '{folder}' in parents";
        fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

        var result = new List<File>();
        string? pageToken = null;
        do
        {
            fileList.PageToken = pageToken;
            var filesResult = await fileList.ExecuteAsync();
            var files = filesResult.Files;
            pageToken = filesResult.NextPageToken;
            result.AddRange(files);
        } while (pageToken != null);

        return result;
    }

    public Task<long> GetVideoDuration(string? id)
    {
        if (id is null)
            return Task.FromResult(0L);

        var command = DriveService.Files.Get(id);
        command.Fields = "videoMediaMetadata";
        var file = command.Execute();
        return Task.FromResult(file.VideoMediaMetadata?.DurationMillis ?? 0L);
    }

    public async Task SaveFileIdAsync(string fileId)
    {
        var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        var json = await System.IO.File.ReadAllTextAsync(appSettingsPath);

        var jsonSettings = new JsonSerializerSettings();
        jsonSettings.Converters.Add(new ExpandoObjectConverter());
        jsonSettings.Converters.Add(new StringEnumConverter());

        dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings)!;

        config.googleAuth.rootFileId = fileId;

        var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);

        await System.IO.File.WriteAllTextAsync(appSettingsPath, newJson);
    }
}
