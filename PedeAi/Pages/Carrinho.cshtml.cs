using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PedeAi.Pages
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class CarrinhoModel : PaginaBase<CarrinhoModel>
    {
        public List<EnderecoDto> Enderecos { get; set; } = new();

        public CarrinhoModel(ILogger<CarrinhoModel> logger) : base(logger)
        {

        }

        private async Task<List<EnderecoDto>> FetchEnderecosAsync()
        {
            using var httpClient = new HttpClient();
            var apiUrl = "https://localhost:7142/api/endereco";
            var enderecos = await httpClient.GetFromJsonAsync<List<EnderecoDto>>(apiUrl);
            return enderecos;
        }

        protected override IActionResult RedirectByUserRole(string? role)
        {
            Enderecos = FetchEnderecosAsync().Result;

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
