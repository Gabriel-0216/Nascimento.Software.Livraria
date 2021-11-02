using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Dominio.Dominios.Emprestimos
{
    public class Devolucao
    {
        public int Id { get; set; } 
        public int IdEmprestimo { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public int LivroId { get; set; }
        public string NomeLivro { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public DateTime DataDevolucaoConfirmada { get; set; }


    }
}
