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
    public class ResponseQuestionController : Controller
    {
        private readonly ILogger<ResponseQuestionController> _logger;
        private readonly DatabaseContext _context;

        public ResponseQuestionController(ILogger<ResponseQuestionController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Post([FromBody] QuestionResponse questionResponse)
        {
            if (ModelState.IsValid)
            {
               

                SurveyResponse surveyResponse = _context.SurveyResponse.Find(questionResponse.SurveyResponseId);
                Question question = _context.Question.Find(questionResponse.QuestionId);

                if (surveyResponse == null)
                {
                    JsonResponse error = new JsonResponse
                    {
                        Message = "Survey response not found",
                        StatusCode = 422
                    };
                    return new JsonResult(error);
                }

                if (question == null)
                {
                    JsonResponse error = new JsonResponse
                    {
                        Message = "Question not found",
                        StatusCode = 422
                    };
                    return new JsonResult(error);
                }

                _context.QuestionResponse.Add(questionResponse);
                _context.SaveChanges();

                Survey survey = _context.Survey.Find(question.SurveyId);
                int SurveyQuestionsCount = survey.Questions.ToList<Question>().Count;
                int SurveyQuestionResponsesCount = surveyResponse.QuestionResponses.ToList<QuestionResponse>().Count;
                if (SurveyQuestionsCount == SurveyQuestionResponsesCount)
                {
                    surveyResponse.Finished = 1;
                    _context.SaveChanges();
                }
                

                JsonResponse response = new JsonResponse
                {
                    Message = "Question response saved",
                    StatusCode = 200,
                    Data = questionResponse
                };

                return new JsonResult(response);
            }
            else
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

