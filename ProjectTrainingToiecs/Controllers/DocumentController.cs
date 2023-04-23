using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol;
using ProjectTrainingToiecs.Common.Enum;
using System.Web;
namespace ProjectTrainingToiecs.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public DocumentController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var lst = _context.Documents.Where(x => x.RecordStatusId == (int)ERecordStatus.Actived).ToList();
            var order = 0;
            lst.ForEach(x =>
            {
                x.Order = order + 1;
                order++;
            });
            ViewBag.lst = lst;
            return View();
        }
        public IActionResult Create(int? id = 0)
        {
            var lessons = _context.Lesson.ToList();
            var model = new ProjectTrainingToiecs.Models.Document();
            if(id > 0)
            {
                model = _context.Documents.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = model;
            ViewBag.lesson = lessons;
            return View();
        }
        public JsonResult CreateDocument(ProjectTrainingToiecs.Models.Document model)
        {
            
            if(model.Id > 0)
            {
                _context.Update(model);
            }
            else
            {
                _context.Add(model);
            }
            _context.SaveChanges();
            return Json(new { data = model.Id });
        }
        public JsonResult DeleteDocument(int id)
        {
            var val = false;
            var user = _context.Documents.FirstOrDefault(x => x.Id == id);
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
