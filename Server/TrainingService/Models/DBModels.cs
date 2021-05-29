using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TrainingService.Models
{
    public class User : IdentityUser
    {
        
    }
    public class Lesson
    { 
            public int Id { get; set; }
            [MaxLength(50)]
            public string Name { get; set; }
            public string Path { get; set; }
            public int SectionId { get; set; }
            public int SectionTopicId { get; set; }                    
    }

    public class Section
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public string Name { get; set; }       
        public List<Lesson> Lessons { get; set; }
    }

    public class Topic
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public List<Section> Sections { get; set; }
    }

    public class Note
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class Article
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        [MaxLength(100)]
        public string Path { get; set; }
        [MaxLength(100)]
        public string ImagePath { get; set; }
        public string Date { get; set; }
    }

    public class Test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public int TopicId{ get; set; }
        public Topic Topic { get; set; }
        public List<Question> Questions { get; set; }
    }
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public string QuestionText { get; set; }
        public bool Type { get; set; }
        public string Options { get; set; }
        public string Correct { get; set; }
    }

    public class UserTest
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int Rating { get; set; }
    }
}
