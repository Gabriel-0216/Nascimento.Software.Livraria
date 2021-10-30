using Microsoft.AspNetCore.Mvc;
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
        private HttpClient _client;
        public LivroController()
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
            var resposta = await GetClient().GetAsync("api/Livro");
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
                            ImagemUrl = @$"C:\Users\GABRI\source\repos\Nascimento.Software.LivrariaV2\Nascimento.Software.Livraria.Servidor\images\{item.ImagemUrl}", // isso aqui é uma das coisas mais bizarras que já fiz
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
            var resposta = await GetClient().PostAsJsonAsync<LivroFotoModel>("api/Livro", livroFoto);
            if (resposta.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }
        private async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var resposta = await GetClient().GetAsync("api/Categoria/GetAll");
            if (resposta.IsSuccessStatusCode)
            {
                List<Categoria> result = JsonConvert.DeserializeObject<List<Categoria>>(await resposta.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
        private async Task<IEnumerable<AutorModel>> GetAutores()
        {
            var resposta = await GetClient().GetAsync("api/Autor/Get");
            if (resposta.IsSuccessStatusCode)
            {
                List<AutorModel> result = JsonConvert.DeserializeObject<List<AutorModel>>(await resposta.Content.ReadAsStringAsync());
                return result;
            }
            return null;
        }
       

    }
}
