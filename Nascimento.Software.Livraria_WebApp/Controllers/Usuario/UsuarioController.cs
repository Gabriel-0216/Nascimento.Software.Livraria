using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers.Usuario
{
    [Authorize]
    public class UsuarioController : Controller
    {//Painel usuário

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] //Listar os empréstimos do usuário, se checkbox "devolvido" for N, listar como azul
        //Se checkbox for 'S' listar como verde [background]
        public async Task<IActionResult> MeusEmprestimos()
        {
            return View();
        }



    }
}
