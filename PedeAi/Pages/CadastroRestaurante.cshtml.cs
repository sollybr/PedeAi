using Microsoft.AspNetCore.Mvc;

namespace PedeAi.Pages
{
    public class CadastroRestauranteModel : PaginaBase<CadastroRestauranteModel>
    {
        [BindProperty]
        public string Nome { get; set; }
        [BindProperty]
        public string CNPJ { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Senha { get; set; }

        public CadastroRestauranteModel(ILogger<CadastroRestauranteModel> logger) : base(logger)
        {
        }
    }
}
