 using Company.G03.BL.Interface;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Repositories
{
    public class DepartmentResitory :GenericRepository<Department>, IDepartmentRepository
    {
       
        public DepartmentResitory(AppDbContext context):base(context)//ask clr create object from appdbcontext
        {
           
        }
     
      

     

      
    }
}
