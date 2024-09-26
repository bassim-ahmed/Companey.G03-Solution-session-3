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
    public class GenericRepository<T> : IGenericRespository<T>where T : BaseEntity
    {
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E=>E.WorkFor).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> Get(int id)
        {
          return await _context.Set<T>().FindAsync(id);
        }

        public async Task<int> Add(T entity)
        {
          await  _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }
        public int Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges();
        }

      
      

    }
}
