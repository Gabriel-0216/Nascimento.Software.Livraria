using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos
{
    public class Aluguel
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public char? Devolvido_checkbox { get; set; }



    }
}
