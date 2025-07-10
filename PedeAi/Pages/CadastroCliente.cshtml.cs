using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PedeAi.Pages
{
    public class CadastroClienteModel : PaginaBase<CadastroClienteModel>
    {

        public CadastroClienteModel(ILogger<CadastroClienteModel> logger) : base(logger)
        {
            ;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public required string Senha { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas n�o coincidem.")]
        public required string ConfirmarSenha { get; set; }

    }
}
