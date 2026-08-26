using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
{
     [ValidateNever]  //this will allow not to validate it ever
    public int Id { get; set; }

    [Required]                                                 //these are the validations
    [StringLength(100)]
    public string StudentName { get; set; }

    [EmailAddress(ErrorMessage ="Email Address is mandatory")]   //these error msg are manual set

    [Range(10,20)]
    public int Age { get; set; }
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

        //public string Password { get; set; }

        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }

    [DateCheck]    //custom validation
    public DateTime AdmissionDate { get; set; }


}
}
