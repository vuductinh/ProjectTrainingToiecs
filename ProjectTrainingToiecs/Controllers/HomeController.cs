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
            
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var unit = _context.Units.OrderBy(x=>x.Id).FirstOrDefault();
                ViewBag.lesson = _context.Lesson.FirstOrDefault(x => x.UnitId == unit.Id);
                ViewBag.unit = unit;
                ViewBag.userName = userName;
                return View();
            }
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