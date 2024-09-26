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
        
      
        public string Name { get; set; }
       
        public int? Age {  get; set; }
  
        public decimal Salary {  get; set; }
        
        public string Email {  get; set; }
      
        public string PhoneNumbr {  get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted {  get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; }= DateTime.Now;

        public int? WorkForId {  get; set; }//fk
        public Department? WorkFor {  get; set; }
    }
}
