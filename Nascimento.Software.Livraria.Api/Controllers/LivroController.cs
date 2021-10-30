using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria.Api.Models;
using Nascimento.Software.Livraria.Business.LivroServices;
using Nascimento.Software.Livraria.Dominio;
using Nascimento.Software.Livraria.Dominio.Dominios;
using Nascimento.Software.Livraria.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<Livro>> Get([FromServices] LivroServices livroServices)
        {
            return await livroServices.Get();
        }
        [HttpGet("{id}")]
        public async Task<Livro> Get([FromServices] LivroServices livroServices, int id)
        {
            return await livroServices.GetLivro(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromServices] LivroServices livroServices, LivroFotoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var livro = new Livro()
            {
                Id = model.Id,
                Nome = model.Nome,
                AutorId = model.AutorId,
                CategoriaId = model.CategoriaId,
                Descricao = model.Descricao,
                FotoId = model.FotoId,
                QtdePaginas = model.QtdePaginas,
            };
            var foto = new Foto()
            {
                FotoId = model.FotoId,
                ImagemURL = model.ImagemURL,
            };
            if(await livroServices.InserirLivro(livro, foto))
            {
                return Ok();
            }
            //ToDo: Criar método pra dividir Foto e Livro em dois objetos e passar pro livroServices
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromServices] LivroServices livroServices, LivroFotoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var livro = new Livro()
            {
                Id = model.Id,
                Nome = model.Nome,
                AutorId = model.AutorId,
                CategoriaId = model.CategoriaId,
                Descricao = model.Descricao,
                FotoId = model.FotoId,
                QtdePaginas = model.QtdePaginas,
            };
            var foto = new Foto()
            {
                FotoId = model.FotoId,
                ImagemURL = model.ImagemURL,
            };
            if (await livroServices.UpdateLivro(livro, foto))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromServices] LivroServices livroServices, int id)
        {
            var livro = await livroServices.GetLivro(id);
            var foto = await livroServices.GetFoto(id);
            if(livro==null || foto == null)
            {
                return BadRequest();
            }
            if (await livroServices.RemoverLivro(livro, foto)){
                return Ok();
            }
            return BadRequest();

        }
       
    }
}
