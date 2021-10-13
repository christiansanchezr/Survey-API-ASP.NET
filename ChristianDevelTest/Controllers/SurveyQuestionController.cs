using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChristianDevelTest.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChristianDevelTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyQuestionController : Controller
    {
        private readonly ILogger<SurveyQuestionController> _logger;
        private readonly DatabaseContext _context;

        public SurveyQuestionController(ILogger<SurveyQuestionController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{surveyId:int}")]
        [AllowAnonymous]
        public JsonResult Get(int SurveyId)
        {
            Survey survey = _context.Survey.Find(SurveyId);

            if (survey != null)
            {
                JsonResponse response = new JsonResponse
                {
                    Message = $"{survey.Name} Questions",
                    Data = survey.Questions,
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

