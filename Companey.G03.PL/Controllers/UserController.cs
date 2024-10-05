﻿using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Company.G03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.G03.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		//Get - GetALL -Update -Delete
		//Index-Details- edit -delete 

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}
    

		public async Task<IActionResult> Index(string InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(InputSearch))
			{
				users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result

				}).ToListAsync();
			}
			else
			{
				users = await _userManager.Users.Where(U => U.Email.ToLower().Contains(InputSearch.ToLower())).Select(U => new UserViewModel()
				{

					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result


				}).ToListAsync();
			

			}


			return View(users);
		}

        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var userFromDb = await _userManager.FindByIdAsync(id);       
            if (userFromDb is null) return NotFound();

            var user = new UserViewModel()
            {
                Id= userFromDb.Id,
                FirstName= userFromDb.FirstName,
                LastName= userFromDb.LastName,
                Email=userFromDb.Email,
                Roles= _userManager.GetRolesAsync(userFromDb).Result


            };
            return View(viewName, user);
        }
        //update
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]
       
        public async Task< IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

               var userFromDb= await _userManager.FindByIdAsync(id);
              
                if (userFromDb is null) return NotFound();
                 userFromDb.FirstName= model.FirstName;
                userFromDb.LastName= model.LastName;
                userFromDb.Email= model.Email;

                await _userManager.UpdateAsync(userFromDb);

          
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
         
            return await Details(id, "Delete");
        }

        [HttpPost]
     
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var userFromDb = await _userManager.FindByIdAsync(id);

                if (userFromDb is null) return NotFound();


                await _userManager.DeleteAsync(userFromDb);


                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
