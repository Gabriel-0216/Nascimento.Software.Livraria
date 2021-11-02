using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria.Business.EmprestimoService;
using Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api.Controllers.Emprestimo
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucaoController : ControllerBase
    {

        [Route("getDevolucoes")]
        [HttpGet]
        public async Task<IEnumerable<Devolucao>> GetDevolucaosAsync([FromServices] DevolucaoService services, Usuario usuario)
        {
            return await services.RetornarDevolucoes(usuario);
        }

        [HttpPost]
        [Route("RegistrarDevolucao")]
        public async Task<ActionResult> RegistrarDevolucao([FromServices] DevolucaoService service, Devolucao devolucao)
        {
            if (ModelState.IsValid)
            {
                if(await service.IniciarDevolucao(devolucao))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        //[HttpGet]
        //[Route("getAll")]
        //public async Task<ActionResult> GetAll([FromServices] DevolucaoService services)
        //{
        //    return services.GetAll():
        //}

        //[HttpDelete]
        //public async Task<ActionResult> DeleteDevolucao([FromServices] DevolucaoService services, Devolucao devolucao)
        //{
        //    if(await services.deletarDevolucao(devolucao))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

    }
}
