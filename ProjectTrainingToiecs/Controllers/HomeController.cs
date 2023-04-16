using Microsoft.AspNetCore.Mvc;
using ProjectTrainingToiecs.Models;
using System.Diagnostics;

namespace ProjectTrainingToiecs.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbTrainingToiecsContext _context;

        public HomeController(DbTrainingToiecsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                //kiểm tra trại thái học
                var idLesson = 0;
                var idDocument = 0;
                var unitId = 0;
                var idTest = 0;
                var started = _context.StatusStudies.Where(x => x.UserId == userId);
                var btnLable = "Start";
                
                if (started.Any())
                {
                    idTest = started.OrderByDescending(x => x.Id).FirstOrDefault().IdTest;
                    idDocument = _context.TestDetails.FirstOrDefault(x => x.Id == idTest).DocumentId;
                    idLesson = _context.Documents.FirstOrDefault(x => x.Id == idDocument).LessonId;
                    unitId = _context.Lesson.FirstOrDefault(x => x.Id == idLesson).UnitId;
                    btnLable = "Continue";
                }
                var unit = unitId > 0 ? _context.Units.FirstOrDefault(x=>x.Id == unitId) 
                    : _context.Units.OrderBy(x=>x.Id).FirstOrDefault();
                ViewBag.lesson = _context.Lesson.FirstOrDefault(x => x.UnitId == unit.Id);
                ViewBag.unit = unit;
                ViewBag.userName = userName;
                ViewBag.btnLable = btnLable;
                ViewBag.idTest = idTest;
                //tính trạng thái hoàn thành
                var total = _context.TestDetails.Count();
                var totalDoing = _context.StatusStudies.Where(x => x.UserId == userId).Count();
                ViewBag.Process = (totalDoing *100)/total;

                return View();
            }
        }
        public IActionResult ListPart()
        {
            return View();
        }
        public IActionResult ListTest()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}