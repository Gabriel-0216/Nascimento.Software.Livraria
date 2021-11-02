using Nascimento.Software.Livraria.Dominio.Dominios.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Dominio.Dominios.Compra
{
    public class Compra
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Livro Livro { get; set; }
        public string LivroNome { get; set; }
        public DateTime DataCompra { get; set; }
    }
}
