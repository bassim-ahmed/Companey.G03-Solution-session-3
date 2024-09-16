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
    public class DepartmentResitory : IDepartmentRepository
    {
        private readonly AppDbContext _Context;
        public DepartmentResitory(AppDbContext context)//ask clr create object from appdbcontext
        {
            _Context=context;
        }
        public IEnumerable<Department> GetAll()
        {
           return _Context.Departments.ToList();
        }
        public Department Get(int id)
        {
            return _Context.Departments.FirstOrDefault(D => D.Id == id);
        }
        public int Add(Department entity)
        {
             _Context.Departments.Add(entity);
            return _Context.SaveChanges();
        }
        public int Update(Department entity)
        {
            _Context.Departments.Update(entity);
            return _Context.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _Context.Departments.Remove(entity);
            return _Context.SaveChanges();
        }

      

     

      
    }
}
