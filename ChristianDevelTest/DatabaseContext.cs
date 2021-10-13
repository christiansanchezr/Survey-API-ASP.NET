using System;
using ChristianDevelTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ChristianDevelTest
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Survey> Survey { get; set; }
        public DbSet<Question> Question { get; set; }

        public DbSet<SurveyResponse> SurveyResponse { get; set; }
        public DbSet<QuestionResponse> QuestionResponse { get; set; }

        public DbSet<User> User { get; set; }
    }
}

