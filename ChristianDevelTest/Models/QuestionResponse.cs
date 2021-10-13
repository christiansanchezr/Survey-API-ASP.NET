using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChristianDevelTest.Models
{
    public class QuestionResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [Required]
        public string Response { get; set; }

        [Required]
        [ForeignKey("SurveyResponse")]
        public int SurveyResponseId { get; set; }
        public SurveyResponse SurveyResponse { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

