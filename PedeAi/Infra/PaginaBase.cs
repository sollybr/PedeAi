using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;
using System.Security.Claims;

namespace PedeAi.Pages
{
    public abstract class PaginaBase<T> : PageModel
    {
        protected readonly ILogger<T> _logger;

        protected PaginaBase(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected virtual IActionResult RedirectByUserRole(string? role)
        {
            return role switch
            {
                "Cliente" => RedirectToPage("/PaginaInicialCliente"),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                "Restaurante" => RedirectToPage("/RestauranteProdutos"),
                _ => RedirectToPage("/Error")
            };
        }

        private Task<IActionResult> HandleUnauthenticatedAccessAsync()
    => RequiresSignOut(HttpContext.Request.Path) switch
    {
        true => SignOutAndRedirectAsync(HttpContext.Request.Path),
        false => Task.FromResult<IActionResult>(Page())
    };

        private static bool RequiresSignOut(PathString path)
            => !path.Equals("/Login");

        private async Task<IActionResult> SignOutAndRedirectAsync(PathString path)
        {
            if (UserRole is not null)
                TempData["Role"] = UserRole;

            await HttpContext.SignOutAsync();

            return path.ToString() switch
            {
                "/" or "/LandingPage" => Page(),
                _ => RedirectToPage("/LandingPage")
            };
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("Get " + HttpContext.Request.Path);
            TempData.Keep("Role");

            return await ValidateLogin(TempData["Role"]?.ToString())
                ?? await HandleUnauthenticatedAccessAsync();
        }

        private Task<IActionResult?> ValidateLogin(string? role) =>
            User.Identity?.IsAuthenticated ?? false &&
            UserEmail is not null && 
            !string.IsNullOrWhiteSpace(role)
                ? Task.FromResult<IActionResult?>(RedirectByUserRole(role))
                : Task.FromResult<IActionResult?>(null);

        public async Task<IActionResult> OnPost()
        {
            _logger.LogInformation("Post" + HttpContext.Request.Path);
            return await this.HandlePost();
        }

        public virtual async Task<IActionResult> HandlePost()
        {
            await Task.Yield();
            return Page();
        }

        public string? UserEmail => User.FindFirst(ClaimTypes.Email)?.Value;
        public string? UserRole => User.FindFirst(ClaimTypes.Role)?.Value;

        public bool IsCliente => UserRole?.Equals("Cliente") ?? false;
        public bool IsEntregador => UserRole?.Equals("Entregador") ?? false;
        public bool IsRestaurante => UserRole?.Equals("Restaurante") ?? false;
    }
}
