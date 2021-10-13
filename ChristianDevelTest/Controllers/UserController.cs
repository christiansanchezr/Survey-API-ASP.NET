using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChristianDevelTest.Models;
using System.Web.Helpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChristianDevelTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly DatabaseContext _context;

        public UserController(ILogger<UserController> logger, DatabaseContext context)
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
                var hash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                DateTime date = DateTime.Now;

                user.Password = hash;
                user.CreatedAt = date;
                _context.User.Add(user);
                _context.SaveChanges();

                JsonResponse response = new JsonResponse
                {
                    Message = "User created",
                    Data = user,
                    StatusCode = 200,

                };
                return new JsonResult(response);
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

