using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PedeAi.Contracts.DTO;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace PedeAi.Pages
{
    public class LoginModel : PaginaBase<LoginModel>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(ILogger<LoginModel> logger, IHttpClientFactory httpClientFactory)
            : base(logger)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail deve conter @aluno.uece.br ou @uece.br")]
        public required string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public required string Senha { get; set; }

        private string Name { get; set; }
        private int UserId { get; set; }

        private async Task SetPerms(string TypePerms)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, Email),
                new Claim(ClaimTypes.Name, Name),
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
                new Claim(ClaimTypes.Role, TypePerms)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public override async Task<IActionResult> HandlePost()
        {
            if (!ModelState.IsValid)
                return Page();
            var role = TempData["Role"]?.ToString();
            var apiUrl = role switch
            {
                "Cliente" => "https://localhost:7142/api/usuario",
                "Entregador" => "https://localhost:7142/api/entregador",
                _ => string.Empty
            };
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(apiUrl + "/login", new
            {
                Email,
                Senha
            });

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<bool>();

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Erro ao autenticar.");
                return Page();
            }
            response = await client.PostAsJsonAsync(apiUrl + "/PegarId", new
            {
                Email,
                Senha
            });
            UserId = await response.Content.ReadFromJsonAsync<int>();
            response = await client.PostAsJsonAsync(apiUrl + "/PegarDados", UserId);
            var dto = await response.Content.ReadFromJsonAsync<DadosUsuarioDto>();
            Name = dto?.Nome ?? "Usuário";

            await SetPerms(role);

            return role switch
            {
                "Cliente" => RedirectToPage("/PaginaInicialCliente"),
                "Entregador" => RedirectToPage("/PedidosEntregador"),
                _ => RedirectToPage("/Error")
            };

        }

        public IActionResult OnPostRedirectSignup()
        {
            return RedirectToPage("/CadastroCliente");
        }
    }
}
