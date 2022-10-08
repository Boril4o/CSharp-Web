using Microsoft.AspNetCore.Mvc;
using MVC_Intro_Demo.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MVC_Intro_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       

        public IActionResult NumbersToN(int count = 3)
        {
            ViewBag.Count = count;
            return View();
        }

        public IActionResult Numbers()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "This is an ASP.Net Core MVC app.";
            return View();
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello World!";
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
    }
}