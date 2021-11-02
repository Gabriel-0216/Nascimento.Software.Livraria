using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Livraria.Business.EmprestimoService;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api.Controllers.Emprestimo
{
    [Route("api/[controller]")]
    [ApiController]
    public class AluguelController : ControllerBase
    {  
        [HttpPost]
        [Route("GetAlugueis")]
        public async Task<IEnumerable<AluguelModel>> Get([FromServices] AluguelService service, Usuario user)
        {
            return await service.RetornarAlugueis(user);
        }

        [HttpPost]
        [Route("RegistrarAluguel")]
        public async Task<ActionResult> RegistrarAluguel(Aluguel aluguel, [FromServices] AluguelService service)
        {
            if (ModelState.IsValid)
            {
                if(await service.IniciarEmprestimo(aluguel))
                {
                    return Ok();
                }
            }
            return BadRequest();
        }

        //[HttpPost]
        //[Route("GetAluguel")]
        //public async Task<AluguelModel> GetAluguelAsync([FromServices] AluguelService service, [FromBody] int id, Usuario user)
        //{
        //    return await service.RetornarAluguelSelecionado(user, id);
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IEnumerable<Aluguel>> GetAll([FromServices] AluguelService service)
        //{
        //    return await service.getAll();
        //}

        //[HttpDelete]
        //[Route("DeletarAluguel")]
        //public async Task<ActionResult> DeletarAluguel(Aluguel aluguel, [FromServices] AluguelService service)
        //{
        //    if(await service.DeletarAluguel(aluguel))
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}
    }
}
