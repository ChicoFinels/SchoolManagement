using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class LecturerMetadata
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}

[ModelMetadataType(typeof(LecturerMetadata))]
public partial class Lecturer { }