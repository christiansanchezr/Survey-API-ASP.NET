using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChristianDevelTest.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChristianDevelTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponseController : Controller
    {
        private readonly ILogger<ResponseController> _logger;
        private readonly DatabaseContext _context;

        public ResponseController(ILogger<ResponseController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("{surveyId:int}")]
        [AllowAnonymous]
        public JsonResult Post(int surveyId, [FromBody] SurveyResponse surveyResponse)
        {
            if (ModelState.IsValid)
            {
                surveyResponse.SurveyId = surveyId;
                _context.SurveyResponse.Add(surveyResponse);
                _context.SaveChanges();

                JsonResponse response = new JsonResponse {
                    Message = "Survey response saved",
                    StatusCode = 200,
                    Data = surveyResponse
                };

                return new JsonResult(response);
            } else
            {
                JsonResponse response = new JsonResponse
                {
                    Message = "Data no valid",
                    StatusCode = 422
                };
                return new JsonResult(response);
            }
        }
    }
}

