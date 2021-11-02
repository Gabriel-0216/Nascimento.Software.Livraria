using Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Aluguel;
using Nascimento.Software.Livraria_WebApp.Models.Identity.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Models.Emprestimo.Devolucao
{
    public class DevolucaoViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int LivroId { get; set; }
        public string NomeLivro { get; set; }

        [Display(Name ="Data do empréstimo")]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name ="Data marcada para devolução")]
        public DateTime DataDevolucao { get; set; }

        [Display(Name ="Status da devolução: 'S/N'")]
        public char Devolvido { get; set; }

    }
}
