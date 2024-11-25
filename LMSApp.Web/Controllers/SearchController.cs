using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using LMSApp.Model.ServiceProviderMaster;
using LMSApp.Repository.Repositories.Interfaces;
using LMSApp.Model.Entities.MASTERS;
namespace LMSApp.Web
{
    public class SearchController : Controller
    {

        public IConfiguration Configuration;
        private readonly IMASTERSRepository _MASTERSRepository;
        private IWebHostEnvironment _hostingEnvironment;
        public SearchController(IConfiguration configuration, IMASTERSRepository MASTERSRepository, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _MASTERSRepository = MASTERSRepository;

            _hostingEnvironment = hostingEnvironment;
        }

        #region MyRegion
        [HttpGet]
        public IActionResult Enqiry()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewEnqiry()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BookingOrder()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Orderprocessing()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Centralplaning()
        {
            return View();
        }
        [HttpGet]
        public IActionResult manufacturing()
        {
            return View();
        }
        [HttpGet]
        public IActionResult markting()
        {
            return View();

        }
    }
}
        #endregion






