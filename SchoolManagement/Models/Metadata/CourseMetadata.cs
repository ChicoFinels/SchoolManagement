using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class CourseMetadata
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public int Credits { get; set; }
}

[ModelMetadataType(typeof(CourseMetadata))]
public partial class Course { }