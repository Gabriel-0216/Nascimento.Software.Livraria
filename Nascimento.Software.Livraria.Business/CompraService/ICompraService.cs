using Nascimento.Software.Livraria.Dominio.Dominios.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Business.CompraService
{
    public interface ICompraService
    {
        Task<bool> Start(Compra compra);
        Task<IEnumerable<Compra>> GetCompras();
    }
}
