using DataMigration.Models;
using HTOTools;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ClearHistory(Order = 1)]
        public IActionResult Index()
        {
            return View();
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


        public IActionResult AjaxNotFound(string message)
        {
            ///The callback function will find the data as text
            ///It can be parsed with:  JSON.parse(data).responseText
            Response.StatusCode = 404;
            return new JsonResult(new { responseText = message });

            ///The callback function will find the data as text
            ///No parsing is needed.  It can be displayed directly.
            /*
            Response.StatusCode = 404;
            return Content(message);
          
             */

        }


    }
}
