using Company.G03.BL.Interface;
using Company.G03.BL.Interfaces;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class EmployeesController :Controller
    {
        private readonly IEmployeeRepository _employeeReopsitory;
        private readonly IDepartmentRepository _departmentRepository;
        public EmployeesController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository)
        {
            _employeeReopsitory= employeeRepository; 
            _departmentRepository= departmentRepository;
        }
        public IActionResult Index(string InputSearch)
        {
            var employees=Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                 employees = _employeeReopsitory.GetAll();
            }
            else
            {
                employees = _employeeReopsitory.GetByName(InputSearch);
            }
       
            //ViewData["Test"] = "hello from backend";
            //ViewBag.Test1 = "hello from viewbag";
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments= _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {

                var Count = _employeeReopsitory.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee = _employeeReopsitory.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }
        //update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var Count = _employeeReopsitory.Update(model);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Has Been edit!";
                }
                else
                {
                    TempData["Message"] = "Employee edit failed";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var Count = _employeeReopsitory.Delete(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
