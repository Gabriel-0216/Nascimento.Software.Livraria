using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nascimento.Software.Livraria_WebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string _urlApi;

        public CategoriaController([FromServices] IHttpClientFactory clientFactory, IConfiguration config)
        {
            _config = config;
            _clientFactory = clientFactory;
            _urlApi = _config.GetValue<string>("Kestrel:EndPoints:Http:Url");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Categoria/GetAll");
            var client = _clientFactory.CreateClient("Api");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return View(JsonConvert.DeserializeObject<IEnumerable<Categoria>>(await response.Content.ReadAsStringAsync()));
            }
            return await Falha();
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Categoria model)
        {
            if (ModelState.IsValid) 
            {

                var client = _clientFactory.CreateClient("Api");

                var request = await client.PostAsJsonAsync($"/api/Categoria/Create", model);
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
            if(id!= null)
            {
                var client = _clientFactory.CreateClient("Api");
                var get = await client.GetAsync($"/Categoria/Get?id={id}");
                if (get.IsSuccessStatusCode)
                {
                    var retorno = JsonConvert.DeserializeObject<Categoria>(await get.Content.ReadAsStringAsync());
                    return View(retorno);
                }
            }
            return await Falha();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("Api");
            var delete = await client.GetAsync($"/Categoria/Delete?id={id}");
            if (delete.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return await Falha();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id != null)
            {
                var client = _clientFactory.CreateClient("Api");
                var get = await client.GetAsync($"/api/Categoria/Get?id={id}");
                if (get.IsSuccessStatusCode)
                {
                    var retorno = JsonConvert.DeserializeObject<Categoria>(await get.Content.ReadAsStringAsync());
                    return View(retorno);
                }
            }
            return await Falha();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Categoria model)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("Api");
                var put = await client.PutAsJsonAsync($"/api/Categoria/Edit", model);
                if (put.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction("Falha404", "Home");
        }
        public async Task<IActionResult> Falha() => RedirectToAction("Falha404", "Home");

    }
}
