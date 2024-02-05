using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System.Collections.Generic;
using System.IO;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoterController : ControllerBase
    {
        private readonly QuoterData QuoterData;
        public QuoterController(SmartCardCRMContext context) 
        {
            QuoterData = new QuoterData(context);
        }

        [HttpPost]
        [Route("[action]")]
        public void SendQuote(QuoterDTO quote)
        {
            QuoterData.SendQuote(quote);
        }

        [HttpGet]
        [Route("[action]")]
        public ActionResult<dynamic> LandingAndAssets()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            var images = new List<string>();
            var videos = new List<string>();

            foreach (var item in Directory.GetFiles(@".\Documents\QuoterAssets\Images"))
            {
                images.Add($"{baseUrl}/Images/{Path.GetFileName(item)}");
            }

            foreach (var item in Directory.GetFiles(@".\Documents\QuoterAssets\Videos"))
            {
                videos.Add($"{baseUrl}/Videos/{Path.GetFileName(item)}");
            }

            return new
            {
                data = new
                {
                    Images = images,
                    Videos = videos
                }
            };
        }
    }
}
