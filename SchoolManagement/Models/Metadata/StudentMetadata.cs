using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class StudentMetadata
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Display(Name = "Date Of Birth")]
    public DateOnly? DateOfBirth { get; set; }
}

[ModelMetadataType(typeof(StudentMetadata))]
public partial class Student { }