using Microsoft.AspNetCore.Mvc;
using ProjectTrainingToiecs.Models;

namespace ProjectTrainingToiecs.Controllers
{
    public class UnitController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public UnitController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var coures = _context.Units.ToList();
            var order = 0;
            coures.ForEach(x =>
            {
                x.Order = order + 1;
                order++;
            });
            ViewBag.lst = coures;
            return View();
        }
        public IActionResult Create(int? id = null)
        {
            var course = new Units();
            var types = _context.Course.Select(x => new CourseModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            if (id > 0)
            {
                course = _context.Units.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = course;
            ViewBag.types = types;
            return View();
        }
        public JsonResult CreateUnit(Units data)
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
