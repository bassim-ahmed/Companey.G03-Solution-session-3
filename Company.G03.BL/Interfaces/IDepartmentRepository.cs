using Company.G03.BL.Interfaces;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Interface
{
    public interface IDepartmentRepository :IGenericRespository<Department>
    {
        //IEnumerable<Department> GetAll();
        //Department Get(int id);
        //int Add(Department entity);
        //int Update(Department entity);
        //int Delete(Department entity);
    }
}
