using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Nascimento.Software.Livraria.Servidor.ProcessamentoImagem;
using Nascimento.Software.Livraria_WebApp.Models;
using Nascimento.Software.Livraria_WebApp.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Controllers
{
    public class LivroController : Controller
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        public LivroController(IHttpClientFactory client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            var client = _client.CreateClient("Api");

            var resposta = await client.GetAsync("api/Livro");
            if (resposta.IsSuccessStatusCode)
            {
                var retorno = JsonConvert.DeserializeObject<IEnumerable<LivroViewModel>>(await resposta.Content.ReadAsStringAsync());
                if (retorno != null)
                {
                
                    var listaRetorno = new List<LivroViewModel>();
                    foreach(var item in retorno)
                    {
                        listaRetorno.Add(new LivroViewModel()
                        {
                            Id = item.Id,
                            ImagemUrl = item.ImagemUrl,
                            AutorId = item.AutorId,
                            CategoriaId = item.CategoriaId,
                            FotoId = item.FotoId,
                            Descricao = item.Descricao,
                            Nome = item.Nome,
                            QtdePaginas = item.QtdePaginas,
                            autores = await GetAutores(),
                            categorias = await GetCategorias(),
                        });
                    }
                    return View(listaRetorno);

                }
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create()
        {

            var livroVm = new LivroViewModel();
            livroVm.autores = await GetAutores();
            livroVm.categorias = await GetCategorias();            
            return View(livroVm);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] FotoService fotoService, LivroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var client = _client.CreateClient("Api");

            var stringImagem = fotoService.SalvarFotoServidor(model.Foto);
            var livroFoto = new LivroFotoModel()
            {
                Id = model.Id,
                AutorId = model.AutorId,
                CategoriaId = model.CategoriaId,
                Descricao = model.Descricao,
                FotoId = model.FotoId,
                ImagemURL = stringImagem,
                Nome = model.Nome,
                QtdePaginas = model.QtdePaginas,
            };
            var resposta = await client.PostAsJsonAsync<LivroFotoModel>("api/Livro", livroFoto);
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var client = _client.CreateClient("Api");
            var resposta = await client.GetAsync($"api/Livro/{id}");
            if (resposta.IsSuccessStatusCode)
            {
                var retorno = JsonConvert.DeserializeObject<LivroFotoModel>(await resposta.Content.ReadAsStringAsync());
                if (retorno != null)
                {
                    return View(retorno);
                }
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _client.CreateClient("Api");

            var resposta = await client.DeleteAsync($"api/Livro?id={id}");
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var client = _client.CreateClient("Api");
            var resposta = await client.GetAsync($"api/Livro/{id}");
            if (resposta.IsSuccessStatusCode)
            {
                var retorno = JsonConvert.DeserializeObject<LivroViewModel>(await resposta.Content.ReadAsStringAsync());
                if (retorno != null)
                {
                    retorno.autores = await GetAutores();
                    retorno.categorias = await GetCategorias();
                    return View(retorno);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LivroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var client = _client.CreateClient("Api");
            var envio = await client.PutAsJsonAsync<LivroViewModel>("api/Livro/", model);
            if (envio.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        #region Private methods
        private async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var client = _client.CreateClient("Api");
            var request = await client.GetAsync("/api/Categoria/GetAll");
            if (request.IsSuccessStatusCode)
            {
                List<Categoria> result = JsonConvert.DeserializeObject<List<Categoria>>(await request.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
        private async Task<IEnumerable<AutorModel>> GetAutores()
        {
            var client = _client.CreateClient("Api");
            var request = await client.GetAsync("/api/Autor/Get");
            if (request.IsSuccessStatusCode)
            {
                List<AutorModel> result = JsonConvert.DeserializeObject<List<AutorModel>>(await request.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }

        #endregion
    }
}
