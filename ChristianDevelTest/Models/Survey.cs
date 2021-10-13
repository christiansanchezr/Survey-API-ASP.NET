using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ChristianDevelTest.Models
{
    public class Survey
    {
        private readonly ILazyLoader _lazyLoader;
        private List<Question> _questions;
        private List<SurveyResponse> _surveyResponses;


        public Survey(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<SurveyResponse> SurveyResponses
        {
            get => _lazyLoader.Load(this, ref _surveyResponses);
            set => _surveyResponses = value;
        }
        public List<Question> Questions
        {
            get => _lazyLoader.Load(this, ref _questions);
            set => _questions = value;
        }
    }
}

