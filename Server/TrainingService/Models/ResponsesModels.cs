using System;
using System.Collections.Generic;
using System.Text;


namespace TrainingService.Models.ResponsesModels
{
    public class ResponseLesson
    {
        public int Id { get; set; }
        public string Name { get; set; }      
    }

    public class ResponseSection
    {
        public int Id { get; set; }
        public string SectionName { get; set; }

        public int TopicId { get; set; }

        public List<Lesson> Lessons { get; set; }
    }

    public class UserCheckOutResponse
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }

    }
    public class NoteResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text{ get; set; }
    }
    public class TestResponse
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
    }

    public class TestWithQuestionsResponse
    {
        public int Id { get; set; }
        public List<QuestionResponse> Questions { get; set; }       
    }

    public class QuestionResponse
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int Type { get; set; }
        public List<string> Options { get; set; }
        public string Correct { get; set; }
    }

    public class ArticleResponse
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
    }
}
