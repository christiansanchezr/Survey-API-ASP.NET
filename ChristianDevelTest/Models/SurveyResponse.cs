using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ChristianDevelTest.Models
{
    public class SurveyResponse
    {
        private readonly ILazyLoader _lazyLoader;
        private List<QuestionResponse> _questionResponses;

        public SurveyResponse(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Survey")]
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        [Required]
        public string ClientFullname { get; set; }
        public int Finished { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<QuestionResponse> QuestionResponses
        {
            get => _lazyLoader.Load(this, ref _questionResponses);
            set => _questionResponses = value;
        }
    }
}

