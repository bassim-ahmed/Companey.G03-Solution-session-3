using Company.G03.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.G03.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        [Range(25, 60, ErrorMessage = "Age Must be Betwwen 25,60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumbr { get; set; }
        public bool IsActive { get; set; }
       
        public DateTime HiringDate { get; set; }
       

        public int? WorkForId { get; set; }//fk
        public Department? WorkFor { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName {  get; set; }
    }
}
