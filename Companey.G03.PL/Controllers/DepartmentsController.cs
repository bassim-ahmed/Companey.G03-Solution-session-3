using Company.G03.BL.Interface;
using Company.G03.BL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Companey.G03.PL.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        public DepartmentsController(IDepartmentRepository departmentResitory)//ask clr to create object from DepartmentResitory
        {
            _DepartmentRepository = departmentResitory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments= _DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {

                var Count = _DepartmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        public IActionResult Details(int?id, string viewName= "Details")
        {
            if (id is null) return BadRequest();
            var department = _DepartmentRepository.Get(id.Value);
            if(department is null) return NotFound();
                return View(viewName,department);
        }
        //update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int?id,Department model)
        {
            if(id !=model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var Count = _DepartmentRepository.Update(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
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
        public IActionResult Delete([FromRoute] int? id, Department model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var Count = _DepartmentRepository.Delete(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
