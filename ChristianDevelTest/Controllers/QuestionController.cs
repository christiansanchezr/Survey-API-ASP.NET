using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChristianDevelTest.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChristianDevelTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly DatabaseContext _context;

        public QuestionController(ILogger<QuestionController> logger, IHttpContextAccessor httpContextAccessor, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public JsonResult Post([FromBody] Question question)
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                question.CreatedAt = date;
                _context.Question.Add(question);
                _context.SaveChanges();
                JsonResponse response = new JsonResponse
                {
                    Message = "Question created",
                    Data = question,
                    StatusCode = 200,

                };
                return new JsonResult(response);
            }

            return new JsonResult("Invalid");
        }


        [HttpPut("{id:int}")]
        [Authorize]
        public JsonResult Put(int id, [FromBody] Question question)
        {
            if (ModelState.IsValid)
            {
                Question thisQuestion = _context.Question.Find(id);

                if (thisQuestion == null)
                {
                    JsonResponse error = new JsonResponse
                    {
                        Message = "Question Not Found",
                        StatusCode = 422,

                    };
                    return new JsonResult(error);
                }
                thisQuestion = question;
                _context.SaveChanges();
                JsonResponse response = new JsonResponse
                {
                    Message = "Question Saved",
                    Data = question,
                    StatusCode = 200,

                };
                return new JsonResult(response);
            }

            return new JsonResult("Invalid");
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public JsonResult Delete(int id)
        {
            Question question = _context.Question.Find(id);

            if (question != null)
            {
                _context.Question.Remove(question);
                _context.SaveChanges();
                JsonResponse response = new JsonResponse
                {
                    Message = "Question deleted",
                    StatusCode = 200,

                };
                return new JsonResult(response);
            }
            else
            {
                JsonResponse response = new JsonResponse
                {
                    Message = "Question Not Found",
                    StatusCode = 422,

                };
                return new JsonResult(response);
            }


        }
    }
}

