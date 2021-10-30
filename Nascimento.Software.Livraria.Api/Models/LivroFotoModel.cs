using Nascimento.Software.Livraria.Dominio.Dominios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Api.Models
{
    public class LivroFotoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int FotoId { get; set; }
        public int AutorId { get; set; }
        public int QtdePaginas { get; set; }
        public string ImagemURL { get; set; }


    }
}
