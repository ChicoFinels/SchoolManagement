using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class EnrollmentMetadata
{
    [Display(Name = "Student")]
    public int? StudentId { get; set; }

    [Display(Name = "Class")]
    public int? ClassId { get; set; }
}

[ModelMetadataType(typeof(EnrollmentMetadata))]
public partial class Enrollment { }