using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class ModificarProdutoModel : PaginaBase<ModificarProdutoModel>
    {
        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public decimal Preco { get; set; }

        [BindProperty]
        public string Descricao { get; set; }
        public ModificarProdutoModel(ILogger<ModificarProdutoModel> logger) : base((ILogger<ModificarProdutoModel>)logger)
        {
        }
    }
}
