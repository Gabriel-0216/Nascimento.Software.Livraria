using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoriaController : ControllerBase
    {

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Categoria>> Get([FromServices] CategoriaRepositorio categoriaRepositorio)
        {
            return await categoriaRepositorio.GetAll();
        }
        [HttpGet]
        [Route("Get")]
        public async Task<Categoria> Get([FromServices] CategoriaRepositorio categoriaRepositorio, int id)
        {
            return await categoriaRepositorio.Get(id);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromServices] CategoriaRepositorio categoriaRepositorio, Categoria model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await categoriaRepositorio.Add(model))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("Edit")]
        public async Task<ActionResult> Edit([FromServices] CategoriaRepositorio categoriaRepositorio, Categoria model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await categoriaRepositorio.Update(model))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete([FromServices] CategoriaRepositorio categoriaRepositorio, int id)
        {
            var categoria = await categoriaRepositorio.Get(id);
            if (categoria == null)
            {
                return BadRequest();
            }
            if(await categoriaRepositorio.Delete(categoria))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
