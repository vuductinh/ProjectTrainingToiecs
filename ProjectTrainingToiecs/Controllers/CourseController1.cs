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
            HttpContext.Session.SetString("Active", "course");
            ViewBag.link = "<i class=\"fa fa-home\" aria-hidden=\"true\"></i><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> <b>Course</b>";
            return View();
        }
        public JsonResult GetCourse(FilterModel filter)
        {
            var order = 0;
            var course = (from i in _context.Course
                          where i.RecordStatusId == (int)ERecordStatus.Actived
                          select new Course()
                          {
                              Id = i.Id,
                              Name = i.Name,
                              Description = i.Description,
                              LastUpdate = i.LastUpdate
                          }).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                course = course.Where(x => x.Name.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
            }
            course.ForEach(x =>
            {
                x.Order = order + 1;
                order++;
            });
            return Json(new { data = course });
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
        [HttpPost]
        public JsonResult GetInfoCourse(int id)
        {
            var course = _context.Course.FirstOrDefault(x => x.Id == id);
            return Json(new { data = course });
        }
        [HttpPost]
        public JsonResult UpdateCourse(Course model)
        {
            if(model.Id > 0)
            {
                var courseOld = _context.Course.FirstOrDefault(x => x.Id == model.Id);
                courseOld.Name = model.Name;
                courseOld.LastUpdate = DateTime.Now;
                courseOld.Description = model.Description;
                _context.Update(courseOld);
            }
            else
            {
                _context.Add(model);
            }
            _context.SaveChanges();
            return Json(new { data = model.Id > 0 });
        }
    }
}
