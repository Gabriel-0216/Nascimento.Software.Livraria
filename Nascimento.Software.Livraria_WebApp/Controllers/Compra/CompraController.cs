using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria_WebApp.Models;
using Nascimento.Software.Livraria_WebApp.Models.Compra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers.Compra
{
    public class CompraController : Controller
    {
        private readonly IHttpClientFactory _client;
        public CompraController(IHttpClientFactory client)
        {
            _client = client;
        }
        //Listar livros 
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Livro");
        }

        [HttpGet]
        public async Task<IActionResult> Comprar(int Id) 
        {//get de um livro
            var client = _client.CreateClient("Api");
            var resposta = await client.GetAsync($"api/Livro/{Id}");
            if (resposta.IsSuccessStatusCode)
            {
                var retorno = JsonConvert.DeserializeObject<LivroFotoModel>(await resposta.Content.ReadAsStringAsync());
                if (retorno != null)
                {
                    var compraModel = new CompraModel()
                {
                    Usuario = GetUsuario(),
                    Livro = retorno,
                    DataCompra = DateTime.Today.Date,
                };
                
                    return View(compraModel);
                }
            }

            return View();
        }

        [HttpPost] //Enviar um CompraModel pra API
        public async Task<IActionResult> Comprar(LivroFotoModel livro)
        {
            if (ModelState.IsValid)
            {
                var compraModel = new CompraModel()
                {
                    Livro = livro,
                    Usuario = GetUsuario(),
                    DataCompra = DateTime.Today.Date,
                };
                var client = _client.CreateClient("Api");
                var request = await client.PostAsJsonAsync<CompraModel>("api/Compra/RegistrarCompra", compraModel);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return await Falha();
        }

        private async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");
        private Models.Identity.Usuario.Usuario GetUsuario()
        {
            return new Models.Identity.Usuario.Usuario()
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email)
            };
        }
        
    }
}
