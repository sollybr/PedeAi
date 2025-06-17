using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PedeAi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger) => _logger = logger;

        public IActionResult OnGet()
        {
            _logger.LogInformation("Get" + HttpContext.Request.Path);
            {
                return RedirectToPage("/LandinPage");
            }
        }
    }
}
