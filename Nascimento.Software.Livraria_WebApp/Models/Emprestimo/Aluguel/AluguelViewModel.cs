using Nascimento.Software.Livraria_WebApp.Models.Identity.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Aluguel
{
    public class AluguelViewModel
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

    }
}
