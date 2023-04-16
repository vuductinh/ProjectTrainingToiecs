using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Newtonsoft.Json;
using ProjectTrainingToiecs.Models;
using System.Net.WebSockets;

namespace ProjectTrainingToiecs.Controllers
{
    public class PartController : Controller
    {
        private readonly DbTrainingToiecsContext _context;
        public PartController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Part1(int? id = null)
        {
            ViewBag.Id = id.HasValue ? id : 0;
            return View();
        }
        public JsonResult GetDetailPart(string id)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var idInt = Convert.ToInt32(id);
            var model = idInt == 0 ? _context.TestDetails.FirstOrDefault() : _context.TestDetails.FirstOrDefault(x=>x.Id == idInt);
            model.Description = model.Description.Replace("\n", "<br />");
            ViewBag.model = model;
            var lstQuesion = _context.TestDetails.Where(x => x.DocumentId == model.DocumentId && x.ItemOrder > 0);
            //check tổng câu hỏi
            model.TotalQuesion = lstQuesion.Count();
            //check vị trí câu hỏi
            model.PosiontLast = lstQuesion.Max(x=>x.ItemOrder) == model.ItemOrder;
            model.PosiontFirst = lstQuesion.Min(x => x.ItemOrder) == model.ItemOrder;
            //lấy ra id trước và sau
            model.IdPre = model.PosiontFirst ? 0 : lstQuesion.FirstOrDefault(x => x.ItemOrder == model.ItemOrder - 1).Id;
            model.IdNext = model.PosiontLast ? 0 : lstQuesion.FirstOrDefault(x => x.ItemOrder == model.ItemOrder + 1).Id;
            //kiểm tra xem đã test chưa
            model.OptionResult = _context.StatusStudies.Any(x => x.IdTest == model.Id && x.UserId == userId) ?
                _context.StatusStudies.FirstOrDefault(x => x.IdTest == model.Id && x.UserId == userId).Option  : "";
            return Json(new {data = model});
        }
        public JsonResult SaveStatusStudy(StatusStudy model)
        {
            model.UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            _context.StatusStudies.Add(model);
            _context.SaveChanges();
            return Json(new { data = model.Id > 0 });
        }
    }
}
