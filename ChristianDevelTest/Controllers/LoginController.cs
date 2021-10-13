using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChristianDevelTest.Models;
using Microsoft.Extensions.Logging;
using ChristianDevelTest.Auth;
using System.Web.Helpers;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChristianDevelTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DatabaseContext _context;

        public LoginController(ILogger<LoginController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                User thisUser = _context.User.Where<User>(u => u.Username.ToLower() == user.Username.ToLower()).First<User>();


                if (thisUser != null)
                {
                    if (!BCrypt.Net.BCrypt.Verify(user.Password, thisUser.Password))
                    {
                        JsonResponse error = new JsonResponse
                        {
                            Message = "Bad credentials",
                            StatusCode = 422,

                        };
                        return new JsonResult(error);
                    }
                    var token = GenerateToken.CreateToken(thisUser);
                    user.Password = "";

                    JsonResponse response = new JsonResponse
                    {
                        Message = "Login",
                        Data = new
                        {
                            User = thisUser,
                            token = token
                        },
                        StatusCode = 200,

                    };
                    return new JsonResult(response);
                } else
                {
                    JsonResponse response = new JsonResponse
                    {
                        Message = "Bad credentials 2",
                        StatusCode = 422,

                    };
                    return new JsonResult(response);
                }
            } else
            {
                JsonResponse response = new JsonResponse
                {
                    Message = "Invalid data",
                    
                    StatusCode = 422,

                };
                return new JsonResult(response);
            }
        }

    }
}

