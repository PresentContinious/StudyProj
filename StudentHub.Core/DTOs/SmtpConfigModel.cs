namespace StudentHub.Core.DTOs;

public class SmtpConfigModel
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string SenderAddress { get; set; } = null!;
    public string SenderDisplayName { get; set; } = null!;
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public bool IsBodyHtml { get; set; }
}
