using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.Data;

public class ClassMetadata
{
    [Display(Name = "Lecturer Id")]
    public int? LecturerId { get; set; }

    [Display(Name = "Course Id")]
    public int? CourseId { get; set; }

    public TimeOnly? Time { get; set; }
}

[ModelMetadataType(typeof(ClassMetadata))]
public partial class Class { }