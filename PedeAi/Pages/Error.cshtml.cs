using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PedeAi.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PaginaBase<ErrorModel>
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorModel(ILogger<ErrorModel> logger) : base(logger) { }

        public new async Task<IActionResult> OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return await base.OnGet();
        }
    }

}
