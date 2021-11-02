using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Aluguel;
using Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Devolucao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers.Emprestimo.Devolucao
{
    [Authorize]
    public class DevolucaoController : Controller
    {
        private readonly IHttpClientFactory _client;
        public DevolucaoController(IHttpClientFactory client)
        {
            _client = client;
        }
        public async Task<IActionResult> ListarEmprestimos()
        {
            var usuario = GetUsuario();
            var client = _client.CreateClient("Api");
            var request = await client.PostAsJsonAsync("/api/Aluguel/GetAlugueis", usuario);
            if (request.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<IEnumerable<DevolucaoViewModel>>(await request.Content.ReadAsStringAsync());
                return View(content);
            }
            return View();
        }
        [HttpGet]      
        public async Task<IActionResult> EmprestimoSelecionado(int idEmprestimo)
        {
            var usuario = GetUsuario();
            var client = _client.CreateClient("Api");
            var request = await client.PostAsJsonAsync("/api/Aluguel/GetAlugueis", usuario);
            if (request.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<IEnumerable<DevolucaoViewModel>>(await request.Content.ReadAsStringAsync());
                return View(content.First(p => p.Id == idEmprestimo));
            }
            return await Falha();
        }
        [HttpPost]
        public async Task<IActionResult> EmprestimoSelecionado(DevolucaoViewModel model)
        {
            if (ModelState.IsValid || !(model.Devolvido == 'S'))
            {
                var client = _client.CreateClient("Api");
                var request = await client.PostAsJsonAsync<DevolucaoViewModel>("/api/Devolucao/RegistrarDevolucao", model);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return await Falha();
        }
        public async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");

        private Models.Identity.Usuario.Usuario GetUsuario()
        {
            return new Models.Identity.Usuario.Usuario()
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
            };
        }
    }
}
