using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebsiteStockManagement.Models;

namespace WebsiteStockManagement.Controllers
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
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }

            if(UsersController.usernameId == 0)
            {
                RedirectToAction("Login", "Users");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
