﻿using Company.G03.BL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL.Repositories
{
    public class GenericRepository<T> : IGenericRespository<T>where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T Get(int id)
        {
          return _context.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
          _context.Add(entity);
            return _context.SaveChanges();
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
