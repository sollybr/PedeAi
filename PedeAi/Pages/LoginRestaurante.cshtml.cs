using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PedeAi.Pages
{
    public class LoginRestauranteModel : PaginaBase<LoginRestauranteModel>
    {

        [BindProperty]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail deve conter @aluno.uece.br ou @uece.br")]
        public required string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public required string Senha { get; set; }

        public LoginRestauranteModel(ILogger<LoginRestauranteModel> logger) : base(logger)
        {
        }
    }
}
