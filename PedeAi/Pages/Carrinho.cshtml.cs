using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class CarrinhoModel : PaginaBase<CarrinhoModel>
    {
        public CarrinhoModel(ILogger<CarrinhoModel> logger) : base(logger)
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
