using ManyToManyEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManyToManyEF.Controllers
{
    public class StudentController : Controller
    {
        private readonly RankenContext _context;

        public StudentController(RankenContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View(await _context.Students.ToListAsync());
        }

        public async Task<IActionResult> ChangeCourse(int courseId)
        {
            List<Student> studentsInCourse;
            ViewBag.Courses = await _context.Courses.ToListAsync();
            ViewBag.SelectedCourseId = courseId;
            if (courseId != 0)
            {
                studentsInCourse = await _context.Students
                    .Where(s => s.Courses
                    .Any(c => c.CourseId == courseId))
                    .ToListAsync();
            }
            else
            {
                studentsInCourse = await _context.Students.ToListAsync();
            }

            return View("Index", studentsInCourse);
        }

        [HttpGet]
        public async Task<IActionResult> EnrollNewStudent()
        {
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnrollNewStudent(Student s, string[] enrolledCourses)
        {
            if (enrolledCourses != null)
            {
                foreach (var courseId in enrolledCourses)
                {
                    Course course = _context.Courses.Find(int.Parse(courseId));
                    s.Courses.Add(course);
                }
            }
            _context.Students.Add(s);
            await _context.SaveChangesAsync();
            List<Student> students = await _context.Students.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View("Index", students);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEnrollment(int id)
        {
            var student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> ModifyStudentEnrollmentCourses(Student s, string[] enrolledCourses)
        {
            Student student = await _context.Students
                .Where(s => s.StudentId == s.StudentId)
                .Include(st => st.Courses)
                .FirstOrDefaultAsync();
            s.Courses.Clear();
            if (enrolledCourses != null)
            {
                foreach (var courseId in enrolledCourses)
                {
                    Course course = _context.Courses.Find(int.Parse(courseId));
                    s.Courses.Add(course);
                }
            }

            await _context.SaveChangesAsync();
            List<Student> students = await _context.Students.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View("Index", students);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students
                .FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Student s)
        {
            _context.Students.Remove(s);
            await _context.SaveChangesAsync();
            List<Student> students = await _context.Students.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View("Index", students);
        }
    }
}
