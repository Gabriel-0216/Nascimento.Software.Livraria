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
    public class AluguelService
    {
        private Processo_Emprestimo _processo_Emprestimo;

        public AluguelService()
        {
            _processo_Emprestimo = new Processo_Emprestimo();
        }

        public async Task<bool> IniciarEmprestimo(Aluguel aluguel)
        {
            aluguel.DataEmprestimo = DateTime.Today;
            aluguel.DataDevolucao = DateTime.Today.AddDays(6);
            //PEGAR PARAMETRIZAÇÃO DE QUANTIDADE DE DIAS EMPRESTADOS E CALCULAR ETC
            if (await _processo_Emprestimo.InicioEmprestimo(aluguel))
            {
                
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<AluguelModel>> RetornarAlugueis(Usuario user)
        {
            return await _processo_Emprestimo.GetAlugueis(user);
        }

        public async Task<AluguelModel> RetornarAluguelSelecionado(Usuario user, int id)
        {
            return await _processo_Emprestimo.GetAluguelSelecionado(user, id);
        }
    }
}
