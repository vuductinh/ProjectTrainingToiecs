using Microsoft.AspNetCore.Mvc;
using ProjectTrainingToiecs.Common.Enum;
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
            var course = (from i in _context.Course
                         where i.RecordStatusId ==(int)ERecordStatus.Actived
                         select new Course()
                         {
                             Id = i.Id,
                             Name = i.Name,
                             Description = i.Description
                         }).ToList();
            course.ForEach(x =>
            {
                x.Order = order + 1;
                order++;
            });
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
        public JsonResult DeleteCourse(int id)
        {
            var val = false;
            var user = _context.Course.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.RecordStatusId = (int)ERecordStatus.Deleted;
                _context.Update(user);
                _context.SaveChanges();
                val = true;
            }
            return Json(new { Data = val });
        }
    }
}
