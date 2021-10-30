using Microsoft.AspNetCore.Cors;
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
    public class AutorController : ControllerBase
    {

        [HttpGet]
        [Route("get")]
        public async Task<IEnumerable<Autor>> Get([FromServices] AutorRepositorio autorRepositorio)
        {
            return await autorRepositorio.GetAll();
        }

        [HttpPost]
        [Route("post")]
        public async Task<ActionResult> Create([FromServices] AutorRepositorio autorRepositorio, Autor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await autorRepositorio.Add(model))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<Autor> GetOne([FromServices] AutorRepositorio autorRepositorio, int id)
        {
            return await autorRepositorio.Get(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update([FromServices] AutorRepositorio autorRepositorio, Autor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await autorRepositorio.Update(model))
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromServices] AutorRepositorio autorRepositorio, int id)
        {
            var autorRepo = await autorRepositorio.Get(id);
            if(autorRepo == null)
            {
                return BadRequest();
            }
            if(await autorRepositorio.Delete(autorRepo))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
