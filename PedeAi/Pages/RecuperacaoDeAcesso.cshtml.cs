using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PedeAi.Pages
{
    public class RecuperacaoDeAcessoModel : PaginaBase<RecuperacaoDeAcessoModel>
    {
        [BindProperty]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail deve conter @aluno.uece.br ou @uece.br")]
        public required string Email { get; set; }
        public RecuperacaoDeAcessoModel(ILogger<RecuperacaoDeAcessoModel> logger) : base(logger)
        {
        }
    }
}
