using Microsoft.AspNetCore.Mvc;
using SmartCardCRM.Data;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System.Linq;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserData UserData;

        public UserController(SmartCardCRMContext context)
        {
            UserData = new UserData(context);
        }

        [HttpPost("Login")]
        public ActionResult<dynamic> Login(UserDTO credentials)
        {
            var user = UserData.Login(credentials.UserName, credentials.Password);
            if (user == null || user.Count == 0)
            {
                return NotFound();
            }

            return new 
            { 
                data = user.Select(u => new
                {
                    u.Id, 
                    u.UserName, 
                    u.Name, 
                    u.LastName,
                    Role = new
                    {
                        Scopes = string.Join(",", u.UserRoles.Select(r => string.Join(",", r.Role.Scopes))).Split(",").Distinct().ToList()
                    }
                }).FirstOrDefault() 
            };
        }
    }
}
