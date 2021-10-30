using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria_WebApp.Models;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nascimento.Software.Livraria_WebApp.Controllers
{
    public class AutorController : Controller
    {
        private HttpClient _client;
        public AutorController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44342/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        private HttpClient GetClient()
        {
            return _client;
        }


        public async Task<IActionResult> Index()
        {
            var resposta = await GetClient().GetAsync("api/Autor/Get");
            if (resposta.IsSuccessStatusCode)
            {
                List<AutorModel> result = JsonConvert.DeserializeObject<List<AutorModel>>(await resposta.Content.ReadAsStringAsync());
                if (result != null){
                    return View(result);
                }
                   
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
            if (!ModelState.IsValid)
            {
                await Falha();
            }
            var resposta = await GetClient().PostAsJsonAsync<AutorModel>("api/Autor/Post", model);
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return await Falha();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return await Falha();
            }
            var resposta = await GetClient().GetAsync($"api/Autor/{id}");
            if (resposta.IsSuccessStatusCode) {
                AutorModel resultado = JsonConvert.DeserializeObject<AutorModel>(await resposta.Content.ReadAsStringAsync());
                return View(resultado);

            }
            return await Falha();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AutorModel model)
        {
            if (!ModelState.IsValid)
            {
                return await Falha();
            }
            var resposta = await GetClient().PutAsJsonAsync<AutorModel>("api/Autor/Update", model);
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return await Falha();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return await Falha();
            }
            var resposta = await GetClient().GetAsync($"api/Autor/{id}");
            if (resposta.IsSuccessStatusCode)
            {
                AutorModel resultado = JsonConvert.DeserializeObject<AutorModel>(await resposta.Content.ReadAsStringAsync());
                return View(resultado);

            }
            return await Falha();

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var resposta = await GetClient().DeleteAsync($"api/Autor/Delete?id={id}");
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return await Falha();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return await Falha();
            }
            var resposta = await GetClient().GetAsync($"api/Autor/{id}");
            if (resposta.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<AutorModel>(await resposta.Content.ReadAsStringAsync()));
            }
            return await Falha();
        }
        public async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");
    }
}
