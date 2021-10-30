using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Dominio.Dominios
{
    public class Livro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int FotoId { get; set; }
        public int AutorId { get; set; }
        public int QtdePaginas { get; set; }
        public string ImagemUrl { get; set; }

    }
}
