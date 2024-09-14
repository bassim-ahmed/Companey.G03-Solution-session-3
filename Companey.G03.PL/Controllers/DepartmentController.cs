using Company.G03.BL.Interface;
using Company.G03.BL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Companey.G03.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IdepartmentRepository _DepartmentResitory;
        public DepartmentController(IdepartmentRepository departmentResitory)//ask clr to create object from DepartmentResitory
        {
            departmentResitory = _DepartmentResitory;
        }
        public IActionResult Index()
        {
            var departments=_DepartmentResitory?.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }
    }
}
