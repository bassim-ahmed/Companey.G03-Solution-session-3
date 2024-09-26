using AutoMapper;
using Company.G03.BL.Interface;
using Company.G03.BL.Interfaces;
using Company.G03.DAL.Models;
using Company.G03.PL.Helper;
using Company.G03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Reflection.Metadata;

namespace Company.G03.PL.Controllers
{
    public class EmployeesController :Controller
    {
        //private readonly IEmployeeRepository _employeeReopsitory;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeesController(/*IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository*/ IUnitOfWork unitOfWork,IMapper mapper)
        {
            //_employeeReopsitory= employeeRepository; 
            //_departmentRepository= departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task< IActionResult> Index(string InputSearch)
        {
            var employees=Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(InputSearch))
            {
                 employees = await _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = await _unitOfWork.EmployeeRepository.GetByName(InputSearch);
            }
             var result=_mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            //ViewData["Test"] = "hello from backend";
            //ViewBag.Test1 = "hello from viewbag";
            return View(result);
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            var departments=  await _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }
                //Casting  EmployeeviewModel(ViewModel)------------>Employee(Model)
                //mapping
                //1-manual mapping
                //Employee employee=new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Salary= model.Salary,
                //    Age= model.Age,
                //    HiringDate= model.HiringDate,
                //    IsActive= model.IsActive,
                //    WorkFor= model.WorkFor,
                //    WorkForId= model.WorkForId,
                //    Email= model.Email,
                //    PhoneNumbr=model.PhoneNumbr,


                //};
                //2-auto mapping

              var employee=  _mapper.Map<Employee>(model);


                var Count = await _unitOfWork.EmployeeRepository.Add(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId,
            //    Email = employee.Email,
            //    PhoneNumbr = employee.PhoneNumbr,


            //}; 
            var employeeViewModel= _mapper.Map<EmployeeViewModel>(employee);
            return View(viewName, employeeViewModel);
        }
        //update
        [HttpGet]
        public async Task< IActionResult> Edit(int? id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            //if (id is null) return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null) return NotFound();
            //return View(department);

            return await Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                if(model.ImageName is not null)
                {
                    DocumentSettings.Delete(model.ImageName, "images");
                }

                if (model.Image is not null)
                {
                    model.ImageName = DocumentSettings.Upload(model.Image, "images");
                }
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Salary = model.Salary,
                //    Age = model.Age,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId,
                //    Email = model.Email,
                //    PhoneNumbr = model.PhoneNumbr,


                //};
                var employee= _mapper.Map<Employee>(model);
                var Count = _unitOfWork.EmployeeRepository.Update(employee);
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
        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
             
                //Employee employee = new Employee()
                //{
                //    Id = model.Id,
                //    Name = model.Name,
                //    Salary = model.Salary,
                //    Age = model.Age,
                //    HiringDate = model.HiringDate,
                //    IsActive = model.IsActive,
                //    WorkFor = model.WorkFor,
                //    WorkForId = model.WorkForId,
                //    Email = model.Email,
                //    PhoneNumbr = model.PhoneNumbr,


                //};
                var employee=_mapper.Map<Employee>(model);

                var Count = _unitOfWork.EmployeeRepository.Delete(employee);
                if (Count > 0)
                {
                    if (model.ImageName is not null)
                    {
                        DocumentSettings.Delete(model.ImageName, "images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
