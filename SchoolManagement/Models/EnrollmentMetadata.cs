using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class EnrollmentMetadata
{
    [Display(Name = "Student Id")]
    public int? StudentId { get; set; }

    [Display(Name = "Class Id")]
    public int? ClassId { get; set; }

    public string? Grade { get; set; }
}

[ModelMetadataType(typeof(EnrollmentMetadata))]
public partial class Enrollment { }