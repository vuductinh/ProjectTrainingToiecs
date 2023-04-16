using Microsoft.AspNetCore.Mvc;
using ProjectTrainingToiecs.Models;

namespace ProjectTrainingToiecs.Controllers
{
    public class LessonController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public LessonController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var lessons = (from i in _context.Lesson
                          join u in _context.Units
                          on i.UnitId equals u.Id
                          select new Lesson()
                          {
                              Id = i.Id,
                              Name = i.Name,
                              Description = i.Description,
                              NameUnit = u.Name
                          }).ToList();
            var order = 0;
            lessons.ForEach(x =>
            {
                x.Order = order + 1;
                order++;
            });
            ViewBag.lst = lessons;
            return View();
        }
        public IActionResult Create(int? id = null)
        {
            var course = new Lesson();
            var types = _context.Units.Select(x => new CourseModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            if (id > 0)
            {
                course = _context.Lesson.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = course;
            ViewBag.types = types;
            return View();
        }
        public JsonResult CreateLesson(Lesson data)
        {
            if (data.Id == 0)
            {
                _context.Add(data);
                _context.SaveChanges();
            }
            else
            {
                _context.Update(data);
                _context.SaveChangesAsync();
            }
            return Json(new { data = data.Id });
        }
    }
}
