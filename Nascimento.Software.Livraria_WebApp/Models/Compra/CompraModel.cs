using Nascimento.Software.Livraria_WebApp.Models.Identity.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Models.Compra
{
    public class CompraModel
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public LivroFotoModel Livro { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
