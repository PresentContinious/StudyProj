using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentHub.Pages;

/// <summary>
/// Error page model
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Request id
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Show request id
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>
    /// On get
    /// </summary>
    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
