using Microsoft.AspNetCore.Mvc;
using Company.G03.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Company.G03.DAL.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Humanizer;
using Company.G03.PL.Helper;

using NuGet.Common;

namespace Company.G03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public UserManager<ApplicationUser> UserManager { get; } 
		public SignInManager<ApplicationUser> SignInManager { get; }

		//sign up
		[HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

		[HttpPost]
		public async Task< IActionResult> SignUp(SignUpViewModel model)
		{
            if (ModelState.IsValid) { 
            //signUP
			var user=await _userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = await _userManager.FindByEmailAsync(model.Email);
					if (user is null)
					{
						user = new ApplicationUser()
						{
							UserName = model.UserName,
							FirstName = model.FirstName,
							LastName = model.LastName,
							Email = model.Email,
							IsAgree = model.IsAgree,


						};
						var result = await _userManager.CreateAsync(user, model.Password);

						if (result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}
						else
						{
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Email has already exists");
					}
				}

				else
				{
					ModelState.AddModelError(string.Empty, "Username has already exists");
				}

            }
			return View();
		}



		//SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		public async Task< IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid )
			{
				var user= await _userManager.FindByEmailAsync(model.Email);
				if (user != null) {

					//check password
					var flag= await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag) {
						// sign in

						var result= await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "InvalidLogin !!");
					};


				}
				else
				{
					ModelState.AddModelError(string.Empty, "InvalidLogin !!");
				};

			}
			return View(model);
		}


		//Sign out

		public new async Task< IActionResult> SignOut()
		{
		 await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn");
		}
		//Forget password
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task< IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model )
		{
			if (ModelState.IsValid) {
				 var user= await _userManager.FindByEmailAsync(model.Email);
				if (user is not  null) {
					//create  a token
					var token=await _userManager.GeneratePasswordResetTokenAsync(user);

					//create link (Reset Password Url)
					var url= Url.Action("ResetPassword","Account", new {Email = model.Email,token=token},Request.Scheme);
					//https://localhost:44329/Account/ResetPassword/?email=bassim@gmail.com

					//create email
					var email = new Email()
					{
						To = user.Email,
						Subject = "forget Password",
						Body= url

					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));

				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid Email");
				}

			}
			return View(model);

		}
		[HttpGet]
		public  IActionResult CheckYourInbox()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ResetPassword(string email,string token )
		{
			TempData["email"]=email;
			TempData["token"]=token;
			return View();

		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user= await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
				 var reset=await _userManager.ResetPasswordAsync(user,token,model.Password);
					if (reset.Succeeded) {
						return RedirectToAction(nameof(SignIn));
					}
				}

			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid Operation ");
			}
			return View(model);

		}


		public IActionResult AccessDenied()
		{
			return View();
		}

    }
}
