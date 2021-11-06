using Nascimento.Software.Livraria.Dominio.Dominios.Compra;
using Nascimento.Software.Livraria.Infraestrutura.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Business.CompraService
{
    public class CompraService : ICompraService
    {
        private readonly processo_compra _prc_compra;
        public CompraService(processo_compra context)
        {
            _prc_compra = context;
        }

        public async Task<IEnumerable<Compra>> GetCompras()
        {
            try
            {
                var entity = await _prc_compra.GetCompras();
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public async Task<bool> Start(Compra compra)
        {
            try
            {
                await _prc_compra.Start(compra);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
