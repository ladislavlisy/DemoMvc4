using System.Web.Mvc;

namespace DemoMvc4
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Examples()
        {
            return View();
        }
    }
}
