using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Interfaces
{
    public interface IGenericRespository<T>
    {
        Task< IEnumerable<T>> GetAll();
       Task<T> Get(int id);
       Task< int> Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
