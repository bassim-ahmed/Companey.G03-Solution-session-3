using Company.G03.BL.Interface;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Repositories
{
    public class DepartmentResitory : IdepartmentRepository
    {
        private readonly AppDbContext _Context;
        public DepartmentResitory(AppDbContext context) //ask clr to create object from APPDbcontext
        {
            _Context = context;
        }
        public int Add(Department entity)
        {
           _Context.Departments.Add(entity);
           return _Context.SaveChanges();
            
        }

        public int Delete(Department entity)
        {
            _Context.Departments.Remove(entity);
           return _Context.SaveChanges();
        }

        public Department Get(int id)
        {
            //return _Context.Departments.FirstOrDefault(D=>D.Id==id);
            return _Context.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _Context.Departments.ToList();
        }

        public int Update(Department entity)
        {
            _Context.Departments.Update(entity);
           return _Context.SaveChanges();
        }
    }
}
