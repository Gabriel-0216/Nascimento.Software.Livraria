using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura
{
    public interface IConnection
    {
        DbConnection GetConnection();
        string GetConnectionString();
    }
}
