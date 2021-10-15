using Microsoft.AspNetCore.Mvc;

namespace WeChip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}