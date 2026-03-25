using Microsoft.AspNetCore.Mvc;

namespace MvcCoreApiClient.Controllers
{
    public class EmpleadosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cliente()
        {
            return View();
        }
    }
}
