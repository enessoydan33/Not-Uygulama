using Microsoft.AspNetCore.Mvc;
using NotUyg.Models;
using System.Diagnostics;

namespace NotUyg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       
       
    }
}
