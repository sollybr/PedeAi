using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{


    public class LogoutModel : PaginaBase<LogoutModel>
    {
        public LogoutModel(ILogger<LogoutModel> logger) : base(logger)
        {
        }

        public override async Task<IActionResult> HandlePost()
        {
            await HttpContext.SignOutAsync();
            return RedirectToPage("/LandingPage");
        }
    }
}