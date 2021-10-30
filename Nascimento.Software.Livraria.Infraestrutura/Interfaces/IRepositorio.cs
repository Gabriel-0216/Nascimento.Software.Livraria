using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nascimento.Software.Livraria.Infraestrutura
{
    public interface IRepositorio<T>
    {
        Task<bool> Add(T entidade);
        Task<bool> Update(T entidade);
        Task<bool> Delete(T entidade);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        
    }
}
