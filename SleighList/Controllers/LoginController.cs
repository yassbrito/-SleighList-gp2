using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SleighList.Contexts;
using SleighList.Models;

namespace SleighList.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly Context _context;

        public LoginController(ILogger<LoginController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        // Action para exibir a página de login
        public IActionResult Index()
        {
            return View();
        }

        // Action para fazer login
        [Route("Login")]
        public IActionResult Logar(IFormCollection form)
        {
            // Recebe os dados do formulário
            string emailInformado = form["Email"].ToString();
            string senhaInformada = form["Senha"].ToString();

            // Busca o usuário no banco de dados
            Usuario usuarioBuscado = _context.Usuario
                .FirstOrDefault(u => u.Email == emailInformado && u.Senha == senhaInformada);

            if (usuarioBuscado == null)
            {
                // Caso o usuário não seja encontrado, exibe mensagem de erro
                ViewBag.Message = "Dados inválidos!";
                return View("Index");
            }
            else
            {
                // Caso o usuário seja encontrado, armazena os dados na sessão
                HttpContext.Session.SetString("UsuarioID", usuarioBuscado.UsuarioID.ToString());
                // Aqui você pode colocar lógica para verificar se o usuário é admin
                HttpContext.Session.SetString("UsuarioNome", usuarioBuscado.Nome);
                return LocalRedirect("~/Home/Index"); // Redireciona para a página inicial ou outra página
            }
        }

        // Caso precise de uma página de erro
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error");
        // }
    }
}
