using System.Web.Mvc;

namespace Softox.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Index of the page
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
    }
}