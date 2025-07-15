using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

using PedeAi.Contracts.DTO;

namespace PedeAi.Pages
{
    public class CatalogoDeRestaurantesModel : PaginaBase<CatalogoDeRestaurantesModel>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CatalogoDeRestaurantesModel(ILogger<CatalogoDeRestaurantesModel> logger, IHttpClientFactory httpClientFactory)
            : base(logger)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public int RestauranteId { get; set; }

        [BindProperty]
        public Dictionary<string, List<AmostraProdutoDto>> ProdutosPorCategoria { get; set; }

        protected override IActionResult RedirectByUserRole(string? role)
        {

            return role switch
            {
                "Cliente" => RedirectToPage("/PaginaInicialCliente"),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                "Restaurante" => RedirectToPage("/RestauranteProdutos"),
                _ => RedirectToPage("/Error")
            };
        }

        public override async Task<IActionResult> HandlePost()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7142/api/produto/categorias/{RestauranteId}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categorias = JsonSerializer.Deserialize<List<CategoriaComProdutosDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                ProdutosPorCategoria = categorias.ToDictionary(
                    cat => cat.Categoria,
                    cat => cat.Produtos.Select(p => new AmostraProdutoDto
                    {
                        IdProduto = p.IdProduto,
                        Nome = p.Nome,
                        Descricao = p.Descricao,
                        Preco = p.Preco,
                        IdCategoria = p.IdCategoria,
                        IdRestaurante = p.IdRestaurante
                    }).ToList()
                );
            }
            else
            {
                ProdutosPorCategoria = new Dictionary<string, List<AmostraProdutoDto>>();
            }
            return Page();
        }

    }
}
