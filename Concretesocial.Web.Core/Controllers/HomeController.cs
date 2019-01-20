using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Concretesocial.Web.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Dynamic;


namespace Concretesocial.Web.Core.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;

        public HomeController(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        public IActionResult Index()
        {
            var context = HttpContext.Request;
            dynamic model = new ExpandoObject();
            model.ClientID = configuration["APISettings:Client_ID"];
            model.CallBackURL = $"{context.Scheme}://{context.Host}{context.Path}APICallback";
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
