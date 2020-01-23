using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}