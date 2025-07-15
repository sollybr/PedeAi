using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class RestauranteProdutosModel : PaginaBase<RestauranteProdutosModel>
    {

        [BindProperty]
        public int RestauranteId { get; set; }

        public RestauranteProdutosModel(ILogger<RestauranteProdutosModel> logger) : base(logger)
        {

        }

        protected override IActionResult RedirectByUserRole(string? role)
        {
            return role switch
            {
                "Cliente" => RedirectToPage("/PaginaInicialCliente"),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                "Restaurante" => Page(),
                _ => RedirectToPage("/Error")
            };
        }

    }
}
