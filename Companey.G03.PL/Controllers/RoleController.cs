using Company.G03.DAL.Models;
using Company.G03.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Company.G03.PL.Controllers
{
    [Authorize(Roles="Admin")]
	public class RoleController : Controller
	{
		

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        //Get - GetALL -Update -Delete
        //Index- create -Details- edit -delete 

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(string InputSearch)
        {
            var roles = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name

                }).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(R => R.Name.ToLower().Contains(InputSearch.ToLower())).Select(R => new RoleViewModel()
                {

                    Id = R.Id,
                    RoleName = R.Name


                }).ToListAsync();


            }


            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var role = new IdentityRole()
                {
                    Name = model.RoleName,
                };
                await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest();
            var roleFromDb = await _roleManager.FindByIdAsync(id);
            if (roleFromDb is null) return NotFound();

            var role = new RoleViewModel()
            {

                Id = roleFromDb.Id,
                RoleName = roleFromDb.Name


            };
            return View(viewName, role);
        }
        //update
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }


        [HttpPost]

        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb is null) return NotFound();

                roleFromDb.Name = model.RoleName;

                await _roleManager.UpdateAsync(roleFromDb);


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

        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {

                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb is null) return NotFound();

                roleFromDb.Name = model.RoleName;

                await _roleManager.DeleteAsync(roleFromDb);


                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public async Task< IActionResult> AddOrRemoveUsers(string roleId) {

            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is  null) return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInRole = new List<UserInRoleViewModel>();
            var users=await _userManager.Users.ToListAsync();
            foreach (var user in users) {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else { 
                  userInRole.IsSelected= false;
                }
                usersInRole.Add(userInRole);
            
            }
         return View(usersInRole);
        
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleViewModel> users)
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            if (ModelState.IsValid) {
               

                foreach (var user in users) {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                        if (appUser is not null) {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }

                   
                }
                return RedirectToAction( nameof(Edit),new {id=roleId});
            }
            return View(users);



        }
    }
}
