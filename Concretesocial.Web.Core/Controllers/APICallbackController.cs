using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Concretesocial.Web.Core.Controllers
{
    public class APICallbackController : Controller
    {
        private IConfiguration configuration;

        public APICallbackController(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public IActionResult Index(string code)
        {

            if(string.IsNullOrEmpty(code))
            {
                throw new Exception("Code cannot be blank");
            }

            try
            {
                return View(ConcretesocialCore.MakeRequest<CodeAuthorizationResponse>($"code?code={code}",
                    configuration["APISettings:Client_ID"],
                    configuration["APISettings:Client_Secret"],
                    userUrlForAuthorization: true).Profiles);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }

        }
    }
}