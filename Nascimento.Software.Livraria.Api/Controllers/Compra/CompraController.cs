using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok();
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
            return Ok();
        }

    }
}
