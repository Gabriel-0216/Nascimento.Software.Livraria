using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria_WebApp.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Nascimento.Software.Livraria_WebApp.Controllers
{
    public class AutorController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public AutorController(IConfiguration config, IHttpClientFactory client)
        {
            _clientFactory = client;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("Api");
            var request = await client.GetAsync($"/api/Autor/Get");

            if (request.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<IEnumerable<AutorModel>>(await request.Content.ReadAsStringAsync()));
            }
            return await Falha();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AutorModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("Api");
                var request = await client.PostAsJsonAsync<AutorModel>($"/api/Autor/post", model);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return await Falha();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var client = _clientFactory.CreateClient("Api");
                var request = await client.GetAsync($"/api/Autor/{id}");
                if (request.IsSuccessStatusCode)
                {
                    AutorModel resultado = JsonConvert.DeserializeObject<AutorModel>(await request.Content.ReadAsStringAsync());
                    return View(resultado);
                }
            }

            return await Falha();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AutorModel model)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("Api");
                var request = await client.PutAsJsonAsync<AutorModel>($"/api/Autor/Update", model);
                if (request.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }       
            return await Falha();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var client = _clientFactory.CreateClient("Api");
                var request = await client.GetAsync($"/api/Autor/{id}");
                if (request.IsSuccessStatusCode)
                {
                    AutorModel resultado = JsonConvert.DeserializeObject<AutorModel>(await request.Content.ReadAsStringAsync());
                    return View(resultado);

                }
            }

            return await Falha();

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("Api");
            var request = await client.DeleteAsync($"/api/Autor/Delete?id={id}");
            if (request.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return await Falha();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var client = _clientFactory.CreateClient("Api");
                var request = await client.GetAsync($"/api/Autor/{id}");
                if (request.IsSuccessStatusCode)
                {
                    AutorModel resultado = JsonConvert.DeserializeObject<AutorModel>(await request.Content.ReadAsStringAsync());
                    return View(resultado);

                }
            }

            return await Falha();
        }
        public async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");
    }
}
