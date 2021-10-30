using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria_WebApp.Models.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public int FotoId { get; set; }
        public int AutorId { get; set; }
        public int QtdePaginas { get; set; }
        public string ImagemUrl { get; set; }

        public IFormFile Foto { get; set; }
        public IEnumerable<Categoria> categorias { get; set; }
        public IEnumerable<AutorModel> autores { get; set; }

    }
}
