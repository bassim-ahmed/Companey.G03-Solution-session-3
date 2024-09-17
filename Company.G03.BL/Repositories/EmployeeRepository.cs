using Company.G03.BL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        
        public EmployeeRepository(AppDbContext context):base(context) {
        
           
        }
  

      

     

       
    }
}
