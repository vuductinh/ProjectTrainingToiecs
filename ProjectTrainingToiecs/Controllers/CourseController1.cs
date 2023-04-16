using Microsoft.AspNetCore.Mvc;
using ProjectTrainingToiecs.Models;
using System.Net.WebSockets;

namespace ProjectTrainingToiecs.Controllers
{
    public class CourseController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public CourseController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var order = 0;
            var course = from i in _context.Course
                         join c in _context.Units
                         on i.Id equals c.CourseId
                         select new Course()
                         {
                             Id = c.Id,
                             Name = c.Name,
                             Description = c.Description
                         };
            ViewBag.lst = course;
            return View();
        }
        public IActionResult Create(int? id = null)
        {
            var course = new Course();
            if(id > 0)
            {
                course = _context.Course.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = course;
            return View();
        }
        public JsonResult CreateCourse(Course data)
        {
            if(data.Id == 0)
            {
                data.Process = "Start";
                _context.Add(data);
                _context.SaveChanges();
            }
            else
            {
                _context.Update(data);
                _context.SaveChangesAsync();
            }
            return Json(new {data = data.Id});
        }
    }
}
