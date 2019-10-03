using MyKanban.Models;
using System.Web.Mvc;

namespace MyKanban.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Items", "KanbanBoards", new { id = 1 });
        }
    }
}