using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Doesnt Match Password ")]
        public string ConfirmPassword { get; set; }
    }
}
