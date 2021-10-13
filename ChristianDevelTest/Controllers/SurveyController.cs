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
    public class SurveyController : Controller
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContext _context;

        public SurveyController(ILogger<SurveyController> logger, IHttpContextAccessor httpContextAccessor, DatabaseContext context)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public JsonResult Post([FromBody] Survey survey)
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                survey.CreatedAt = date;
                _context.Survey.Add(survey);
                _context.SaveChanges();
                string url = _httpContextAccessor.HttpContext?.Request.Host.Value + "/response/" + survey.Id;
                JsonResponse response = new JsonResponse
                {
                    Message = url,
                    Data = survey,
                    StatusCode = 200,
                    
                };
                return new JsonResult(response);
            }

            return new JsonResult("Invalid");
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public JsonResult Put(int id, [FromBody] Survey survey)
        {
            if (ModelState.IsValid)
            {
                Survey thisSurvey = _context.Survey.Find(id);

                if (thisSurvey == null)
                {
                    JsonResponse error = new JsonResponse
                    {
                        Message = "Survey Not Found",
                        StatusCode = 422,

                    };
                    return new JsonResult(error);
                }
                thisSurvey = survey;
                _context.SaveChanges();
                JsonResponse response = new JsonResponse
                {
                    Message = "Survey Saved",
                    Data = survey,
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
            Survey survey = _context.Survey.Find(id);

            if (survey != null)
            {
                _context.Survey.Remove(survey);
                _context.SaveChanges();
                JsonResponse response = new JsonResponse
                {
                    Message = "Survey deleted",
                    StatusCode = 200,

                };
                return new JsonResult(response);
            } else
            {
                JsonResponse response = new JsonResponse
                {
                    Message = "Survey Not Found",
                    StatusCode = 422,

                };
                return new JsonResult(response);
            }

            
        }
    }
}

