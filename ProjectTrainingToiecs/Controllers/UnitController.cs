using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectTrainingToiecs.Common.Enum;
using ProjectTrainingToiecs.Models;
using ProjectTrainingToiecs.Service.Service;
using ProjectTrainingToiecs.Service.Service.Impl;

namespace ProjectTrainingToiecs.Controllers
{
    public class UnitController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public UnitController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index(int courseId)
        {
            var course = _context.Course.FirstOrDefault(x => x.Id == courseId);
            ViewBag.courseId = courseId;
            ViewBag.nameCourse = course.Name;
            ViewBag.link = string.Format("<i class=\"fa fa-home\" aria-hidden=\"true\"></i><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> Course <i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> <b>{0}</b>",course.Name);
            return View();
        }
        public IActionResult UnitClient(int courseId)
        {
            var course = _context.Course.FirstOrDefault(x => x.Id == courseId);
            ViewBag.courseId = courseId;
            ViewBag.nameCourse = course.Name;
            return View();
        }
        public JsonResult GetUnits(FilterModel filter)
        {
            var units = _context.Units.Where(x => x.RecordStatusId == (int)ERecordStatus.Actived && x.CourseId == filter.CourseId).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                units = units.Where(x => x.Name.ToLower().Contains(filter.TextSearch) 
                || x.Description.ToLower().Contains(filter.TextSearch)).ToList();
            }
            units = units.Take(filter.PageSize).ToList();
            var role = HttpContext.Session.GetString("Role");
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var lastSeen = new Dictionary<int, DateTime>();
            var markeds = new Dictionary<int, int>();
            if (role == "User")
            {
                ICommonService service = new AppCommonService();
                var logs = _context.AuditLogs.Where(x => x.UserId == userId && x.PageAccess.Contains("GetDocument"))
                .GroupBy(x => x.PageAccess).
                    Select(x => new Auditdetail()
                    {
                        Page = x.Key + ';',
                        LastSeen = x.OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt
                    }).ToList();
                logs.ForEach(x =>
                {
                    var unitId = service.GetBetween(x.Page, "GetDocument/", ";") ?? "0";
                    x.UnitId = Convert.ToInt32(unitId);
                });
                lastSeen = logs.ToDictionary(x => x.UnitId, x => x.LastSeen);
                // thống kê số câu hỏi người dùng đã làm
                markeds = _context.StatusStudies.Where(x => x.UserId == userId)
                    .GroupBy(x=>x.UnitId).Select(x=> new {x.Key, val = x.Count()})
                    .ToDictionary(x=>x.Key,x=>x.val);
            
            }
            var order = 0;
            units.ForEach(x =>
            {
                if (lastSeen.Any())
                {
                    x.LastSeen = lastSeen.ContainsKey(x.Id) ? lastSeen[x.Id] : null;
                }
                if (markeds.Any())
                {
                    x.Marks = markeds.ContainsKey(x.Id) ? markeds[x.Id] : 0;
                }
                x.Order = order + 1;
                order++;
            });
            units.Add(new Units() { 
                Order = order  +1,
                Name = "Unit Test",
                Description = "Purchasing refers to the act of buying something. It can include things like groceries, clothes, and electronics.",
                Count = 100});
            return Json(new { data = units });
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
        public JsonResult DeleteUnit(int id)
        {
            var val = false;
            var user = _context.Units.FirstOrDefault(x => x.Id == id);
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
        public JsonResult GetInfoUnit(int id)
        {
            var unit = _context.Units.FirstOrDefault(x => x.Id == id);
            return Json(new { data = unit });
        }
        [HttpPost]
        public JsonResult UpdateUnit(Units model)
        {
            if(model.Id > 0)
            {
                var unitOld =_context.Units.FirstOrDefault(x => x.Id == model.Id);
                unitOld.Description = model.Description;
                unitOld.Count = model.Count;
                unitOld.LastUpdate = DateTime.Now;
                unitOld.Name = model.Name;
                _context.Units.Update(unitOld);
            }
            else
            {
                _context.Units.Add(model);
            }
            _context.SaveChanges();
            return Json(new { data = model.Id > 0 });
        }
    }
}
