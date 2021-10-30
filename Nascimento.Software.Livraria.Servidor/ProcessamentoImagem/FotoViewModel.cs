using Microsoft.AspNetCore.Http;

namespace Nascimento.Software.Livraria.Servidor.ProcessamentoImagem
{
    public class FotoViewModel
    {
        public IFormFile Foto { get; set; }
        public string ImagemURL { get; set; }
    }

}