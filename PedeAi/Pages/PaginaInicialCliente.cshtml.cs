using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class PaginaInicialClienteModel : PaginaBase<PaginaInicialClienteModel>
    {
        public PaginaInicialClienteModel(ILogger<PaginaInicialClienteModel> logger) : base(logger)
        {
        }
        protected override IActionResult RedirectByUserRole(string? role)
        {
            return role switch
            {
                "Cliente" => Page(),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                "Restaurante" => RedirectToPage("/RestauranteProdutos"),
                _ => RedirectToPage("/Error")
            };
        }

        public void OnUnload()
        {
            TempData.Keep("Role");
        }
    }
}

