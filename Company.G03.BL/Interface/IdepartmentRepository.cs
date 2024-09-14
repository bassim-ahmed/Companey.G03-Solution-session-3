using Company.G03.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Interface
{
    public interface IdepartmentRepository
    {
       public IEnumerable<Department> GetAll();
        Department Get(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);
    }
}
