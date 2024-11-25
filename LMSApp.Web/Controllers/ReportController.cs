using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult LcLgWiseSales()
        {
            return View();
        }
        public IActionResult ProviderWiseSales()
        {
            return View();
        }
    }
}
