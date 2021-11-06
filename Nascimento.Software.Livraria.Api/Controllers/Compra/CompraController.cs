using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria.Business.CompraService;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api.Controllers.Compra
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _service;
        public CompraController(ICompraService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("RetornarCompras")]
        public async Task<ActionResult> RetornarCompras(Usuario usuario)
        {
            return Ok();
        }
        [HttpPost]
        [Route("RegistrarCompra")]
        public async Task<ActionResult> RegistrarCompra(Dominio.Dominios.Compra.Compra compra)
        {
            if (ModelState.IsValid)
            {
                if (await _service.Start(compra)) return Ok("Cadastro realizado com sucesso");
            }

            return BadRequest("Ocorreu um problema");
        }
        [HttpPost]
        [Route("EstornarCompra")]
        public async Task<ActionResult> EstornarCompra(Dominio.Dominios.Compra.Compra compra)
        {
            return Ok();
        }
        [HttpGet]//admin
        [Route("RetornarComprasTodas")]
        public async Task<ActionResult> RetornarTodasCompras()
        {
            var lista = await _service.GetCompras();
            if (lista != null)
            {
                return Ok(lista);
            }
            return BadRequest("Ocorreu um erro");
        }
    }
}
