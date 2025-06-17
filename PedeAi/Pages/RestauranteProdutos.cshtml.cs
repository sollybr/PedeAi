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
                "Cliente" => Page(),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                "Restaurante" => RedirectToPage("/RestauranteProdutos"),
                _ => RedirectToPage("/Error")
            };
        }

    }
}
