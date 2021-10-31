using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Servidor.ProcessamentoImagem
{
    public class FotoService
    {
        public string SalvarFotoServidor(IFormFile foto)
        {
            if (foto == null)
            {
                return string.Empty;
            }
            string uniqueFileName = string.Empty;
            string uploadFolder = @"C:\Users\GABRI\source\repos\Nascimento.Software.LivrariaV2\Nascimento.Software.Livraria_WebApp\wwwroot\images\";
            uniqueFileName = Guid.NewGuid().ToString() + "_" + foto.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                foto.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
