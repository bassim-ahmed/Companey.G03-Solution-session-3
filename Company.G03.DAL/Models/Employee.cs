using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.DAL.Models
{
    public class Employee:BaseEntity
    {
        
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Range(25,60,ErrorMessage ="Age Must be Betwwen 25,60")]
        public int? Age {  get; set; }
        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary {  get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }
        [Phone]
        public string PhoneNumbr {  get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted {  get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }= DateTime.Now;
    }
}
