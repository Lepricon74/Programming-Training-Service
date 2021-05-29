using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;
using Microsoft.EntityFrameworkCore;
using TrainingService.DBRepository.Interfaces;

namespace TrainingService.DBRepository.Repositories
{
    public class SQLLessonsRepository : ILessonRepository
	{
		private TrainingServiceContext context;
		public SQLLessonsRepository(TrainingServiceContext _db) {
			context = _db;
		}

		public Lesson GetLesson(string topicName, int sectionId, int lessonId)
		{
			var topic = context.Topics.FirstOrDefault(topic => topic.Name == topicName);
			if (topic == null) return null;
			int topicId = topic.Id;
			var result = new Lesson();
			result = context.Lessons.FirstOrDefault(Lesson => Lesson.Id == lessonId && Lesson.SectionId == sectionId && Lesson.SectionTopicId == topicId);
			return result;
		}
		public List<Topic> GetTopics()
		{
			return context.Topics.Include(topic => topic.Sections).ToList();
		}

		public List<ResponseSection> GetLessonsList(string topicName)
		{
			//по имени темы находим  id
			var topic = context.Topics.FirstOrDefault(topic => topic.Name == topicName);
			if (topic == null) return null;
			int topicId = topic.Id;
			//получаем все разделы этой темы и собираем данные каждого раздела в класс ResponseSection, после чего формируем список
			return context.Sections.Where(section => section.TopicId == topicId)
								   .Select(section => new ResponseSection { TopicId = section.TopicId, Id = section.Id, SectionName = section.Name, Lessons = section.Lessons })
								   .ToList();
		}

		public int GetNewLessonId(int topicId, int sectionId)
		{
			var lessonsList = context.Lessons.Where(Lesson => Lesson.SectionId == sectionId && Lesson.SectionTopicId == topicId).ToList();
			return (lessonsList.Count() == 0) ? 1 : lessonsList.Last().Id+1;
		}

		public void AddLesson(Lesson newLesson)
		{
			context.Lessons.Add(newLesson);
			Save();
		}
		public void Save()
		{
			context.SaveChanges();
		}
	}
}
