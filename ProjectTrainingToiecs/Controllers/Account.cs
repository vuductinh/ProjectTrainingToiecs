using Microsoft.AspNetCore.Mvc;

namespace ProjectTrainingToiecs.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
