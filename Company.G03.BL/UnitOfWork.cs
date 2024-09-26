using Company.G03.BL.Interface;
using Company.G03.BL.Interfaces;
using Company.G03.BL.Repositories;
using Company.G03.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UnitOfWork(AppDbContext context)
        {
            _departmentRepository=new DepartmentResitory(context);
            _employeeRepository= new EmployeeRepository(context);
            _context = context;

        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository;
    }
}
