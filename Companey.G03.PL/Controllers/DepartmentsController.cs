using Company.G03.BL.Interface;
using Company.G03.BL.Repositories;
using Company.G03.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Companey.G03.PL.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        public DepartmentsController(IDepartmentRepository departmentResitory)//ask clr to create object from DepartmentResitory
        {
            _DepartmentRepository = departmentResitory;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var departments= await _DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {

                var Count = await _DepartmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        public async Task< IActionResult> Details(int?id, string viewName= "Details")
        {
            if (id is null) return BadRequest();
            var department = await _DepartmentRepository.Get(id.Value);
            if(department is null) return NotFound();
                return View(viewName,department);
        }
        //update
        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return await Details(id, "Edit");
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
        public async Task< IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);
            return await Details(id, "Delete");
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
