using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class LandingPageModel : PaginaBase<LandingPageModel>
    {
        public LandingPageModel(ILogger<LandingPageModel> logger) : base(logger)
        {
        }

        public IActionResult OnPostCliente()
        {
            TempData["Role"] = "Cliente";
            return RedirectToPage("/Login");
        }
        public IActionResult OnPostRestaurante()
        {
            TempData["Role"] = "Restaurante";
            return RedirectToPage("/ProdutosRestaurante");
        }
        public IActionResult OnPostEntregador()
        {
            TempData["Role"] = "Entregador";
            return RedirectToPage("/Login");
        }
    }
}
