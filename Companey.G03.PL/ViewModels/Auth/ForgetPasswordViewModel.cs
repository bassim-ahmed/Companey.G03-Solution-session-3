﻿using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
