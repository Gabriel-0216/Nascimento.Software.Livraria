using Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos;
using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using Nascimento.Software.Livraria.Infraestrutura.Emprestimos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Business.EmprestimoService
{
    public class DevolucaoService
    {
        private processo_devolucao _processo_Devolucao;
        public DevolucaoService()
        {
            _processo_Devolucao = new processo_devolucao();
        }
        public async Task<bool> IniciarDevolucao(Devolucao devolucao)
        {
            if (await _processo_Devolucao.IniciarDevolucao(devolucao))
            {
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Devolucao>> RetornarDevolucoes(Usuario user)
        {
            return await _processo_Devolucao.GetDevolucoes(user);
        }
    }
}
