using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomatedInstagramBot.Pages;

public class YourPageModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public YourPageModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

