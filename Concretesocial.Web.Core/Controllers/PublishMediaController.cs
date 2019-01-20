using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Concretesocial.Web.Core.Controllers
{
    public class PublishMediaController : Controller
    {
        private IConfiguration configuration;

        public PublishMediaController(IConfiguration Configuration)
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

        [HttpPost]
        public IActionResult Publish(string caption, string mediaType, string url, string comment, string[] profiles)
        {
            MediaRequestItem newItem = new MediaRequestItem()
            {
                caption = caption,
                media_type = mediaType,
               // image_url = url,
                media_url = url,
                comment = comment,
                profiles = profiles
            };
            
            APIResponseItem result = ConcretesocialCore.MakeRequest<APIResponseItem>("publish",
                configuration["APISettings:Client_ID"],
                configuration["APISettings:Client_Secret"], 
                newItem);

            if (result.result != null && result.result.Any())
            {
                foreach (var resultItem in result.result)
                {
                    if (string.IsNullOrEmpty(resultItem.media))
                    {
                        return Content(resultItem.response.error.ToString());
                    }
                    else
                    {
                        return Content(resultItem.media.ToString());
                    }
                }
            }

            return null;
        }
    }
}