using Microsoft.AspNetCore.Mvc;
using PedeAi.Contracts.DTO;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PedeAi.Pages
{
    public class DetalhesPedidoModel : PaginaBase<DetalhesPedidoModel>
    {
        private readonly HttpClient _httpClient;

        public DetalhesPedidoModel(ILogger<DetalhesPedidoModel> logger, HttpClient httpClient) : base(logger)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public int PedidoId { get; set; }


        protected override IActionResult RedirectByUserRole(string? role)
        {
            if (TempData.ContainsKey("PedidoId"))
            {
                if (int.TryParse(TempData["PedidoId"]?.ToString(), out var pedidoIdLocal))
                {
                    PedidoId = pedidoIdLocal;
                }

            }

            return role switch
            {
                "Cliente" or "Entregador" => Page(),
                "Restaurante" => RedirectToPage("/RestauranteProdutos"),
                _ => RedirectToPage("/Error")
            };
        }
    }

}
