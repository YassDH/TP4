using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TP4.Data;
using TP4.Models;

namespace TP4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UniversityContext uContext = UniversityContext.GetContext();
            List<Student> s = uContext.Student.ToList();
            foreach (Student student in s)
            {
                Debug.WriteLine("****************************************");
                Debug.WriteLine("ID : "+ student.id);
                Debug.WriteLine("First Name : "+ student.first_name);
                Debug.WriteLine("Last Name : "+ student.last_name);
                Debug.WriteLine("Phone Number : "+ student.phone_number);
                Debug.WriteLine("University : " + student.university);
                Debug.WriteLine("Course : " + student.course);
                Debug.WriteLine("TimeStamp : " + student.timestamp);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            UniversityContext uContext = UniversityContext.GetContext();
            return View();
        }
        public IActionResult Courses()
        {
            UniversityContext uContext = UniversityContext.GetContext();
            StudentRepository StudentRepo = new StudentRepository(uContext);

            List<string> courses = (List<string>)StudentRepo.GetCourses();

            return View(courses);
        }
        [HttpGet]
        [Route("/Course/{courseName}")]
        public IActionResult CoursesStudents(string courseName)
        {
            UniversityContext uContext = UniversityContext.GetContext();
            StudentRepository StudentRepo = new StudentRepository(uContext);

            IEnumerable<Student> StudentsPerCourse = (IEnumerable<Student>)StudentRepo.GetStudentsPerCourse(courseName);
            ViewBag.CourseName = courseName;

            return View(StudentsPerCourse);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}