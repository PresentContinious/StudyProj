using Microsoft.AspNetCore.Http;

namespace StudentHub.Core.Interfaces;

using File = Google.Apis.Drive.v3.Data.File;

public interface IDriveService
{
    Task<string> CreateFolder(string folderName, params string[] parents);
    Task<(string id, long? durationInMin)> UploadFile(IFormFile file, string? folder = null);
    Task<string> DeleteFile(string fileId);
    Task<IEnumerable<File>> GetFiles(string? folderId);
    Task<long> GetVideoDuration(string? id);
    Task SaveFileIdAsync(string fileId);
}
