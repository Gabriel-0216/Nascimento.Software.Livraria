using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria_WebApp.Models;
using Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Aluguel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers.Emprestimo.Aluguel
{
    [Authorize]
    public class AlugarController : Controller
    {
        private readonly IHttpClientFactory _client;
        public AlugarController(IHttpClientFactory client)
        {
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> ListarLivros()
        {
            var client = _client.CreateClient("Api");
            var request = await client.GetAsync("api/Livro");
            if (request.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<IEnumerable<LivroFotoModel>>(await request.Content.ReadAsStringAsync()));
            }
            return await Falha();
        }

        [HttpGet] //Ao clicar em um livro, o usuário é direcionado para uma página que mostra todos os detalhes do livro, com uma opção de "alugar"
        public async Task<IActionResult> LivroSelecionado(int id)
        {
            ViewData["User"] = User.FindFirstValue(ClaimTypes.Name);
            var client = _client.CreateClient("Api");
            var request = await client.GetAsync($"api/Livro/{id}");
            if (request.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<LivroFotoModel>(await request.Content.ReadAsStringAsync()));
            }
            return await Falha();
        }
        [HttpPost] // Usuário clicou em alugar na tela de LivroSelecionado
        public async Task<IActionResult> LivroSelecionado(Livro livro)
        {
            AluguelViewModel aluguel = new AluguelViewModel()
            {
                Livro = livro,
                Usuario = new Models.Identity.Usuario.Usuario()
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    UserName = User.FindFirstValue(ClaimTypes.Name),
                    Email = User.FindFirstValue(ClaimTypes.Email),
                }
            };
            var client = _client.CreateClient("Api");
            var request = await client.PostAsJsonAsync("api/Aluguel/RegistrarAluguel", aluguel);
            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            return await Falha();
        }

        [HttpGet]
        public async Task<IActionResult> Sucesso()
        {
            return View();
        }

        public async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");

    }
}
