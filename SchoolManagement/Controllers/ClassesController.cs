using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class ClassesController : Controller
    {
        private readonly SchoolManagementContext _context;
        private readonly INotyfService _notyfService;

        public ClassesController(SchoolManagementContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var schoolManagementContext = _context.Classes.Include(c => c.Course).Include(c => c.Lecturer);
            return View(await schoolManagementContext.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            CreateSelectList();
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,CourseId,Time")] Class @class)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateSelectList();
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            CreateSelectList();
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,CourseId,Time")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            CreateSelectList();
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class != null)
            {
                _context.Classes.Remove(@class);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageEnrollments(int? classId)
        {
            if (classId == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(c => c.Course)
                .Include(c => c.Lecturer)
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == classId);

            if (@class == null)
            {
                return NotFound();
            }

            var students = await _context.Students.ToListAsync();

            var model = new ClassEnrollmentViewModel();

            model.Class = new ClassViewModel
            {
                Id = @class.Id,
                CourseName = $"{@class.Course.Code} - {@class.Course.Name}",
                Time = @class.Time.ToString(),
                LecturerName = $"{@class.Lecturer.FirstName} {@class.Lecturer.LastName}"
            };

            foreach (var student in students)
            {
                model.Students.Add(new StudentEnrollmentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    IsEnrolled = (@class?.Enrollments?.Any(e => e.StudentId == student.Id)).GetValueOrDefault()
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageEnrollments(int classId, int studentId, bool shouldEnroll)
        {
            if (shouldEnroll)
            {
                await _context.AddAsync(new Enrollment
                {
                    ClassId = classId,
                    StudentId = studentId,
                });
                _notyfService.Success("Student Enrolled Successfully");
            }
            else
            {
                var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.ClassId == classId && e.StudentId == studentId);
                if (enrollment != null)
                {
                    _context.Remove(enrollment);
                    _notyfService.Warning("Student Unenrolled Successfully");
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageEnrollments), new { classId });
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }

        private void CreateSelectList()
        {
            var courses = _context.Courses.Select(l => new
            {
                l.Id,
                CourseName = $"{l.Code} - {l.Name}"
            });

            ViewData["CourseId"] = new SelectList(courses, "Id", "CourseName");

            var lecturers = _context.Lecturers.Select(l => new
            {
                l.Id,
                FullName = $"{l.FirstName} {l.LastName}"
            });

            ViewData["LecturerId"] = new SelectList(lecturers, "Id", "FullName");
        }
    }
}
