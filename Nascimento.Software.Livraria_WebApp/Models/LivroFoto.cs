using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Models
{
    public class LivroFoto
    {
        public FotoModel Foto { get; set; }
        public Livro Livro { get; set; }

    }
}
