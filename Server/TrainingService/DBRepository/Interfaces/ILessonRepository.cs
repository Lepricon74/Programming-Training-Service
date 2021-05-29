using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;

namespace TrainingService.DBRepository.Interfaces
{
	public interface ILessonRepository //: IDisposable
	{
		Lesson GetLesson(string topicName, int sectionId, int lessonId);
		void AddLesson(Lesson newLesson);
		List<Topic> GetTopics();
		int GetNewLessonId(int sectionId, int topicId);
		public List<ResponseSection> GetLessonsList(string topicName);
	}
}
