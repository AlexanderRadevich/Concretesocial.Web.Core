using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Concretesocial.Web.Core.Controllers
{
    public class ListConnectedProfilesController : Controller
    {
        private IConfiguration configuration;

        public ListConnectedProfilesController(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public IActionResult Index()
        {
            try
            {
                return View(ConcretesocialCore.MakeRequest<ProfileItem[]>("profiles",
                    configuration["APISettings:Client_ID"],
                    configuration["APISettings:Client_Secret"]));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}