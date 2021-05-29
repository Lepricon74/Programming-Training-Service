using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;
using TrainingService.DBRepository.Interfaces;
using System;

namespace TrainingService.DBRepository.Repositories
{
    public class SQLTestsRepository : ITestRepository
    {
        private TrainingServiceContext db;

        public SQLTestsRepository(TrainingServiceContext _db)
        {
            db = _db;
        }
        public List<TestResponse> GetTestsWithUserRating(string userId, string topicName)
        {
            //получаем Id темы по названию  
            int topicId = db.Topics.FirstOrDefault(topic => topic.Name == topicName).Id;
            //создаем список объектов TestResponse, по умолчанию полю рейтинг присваиваем значение 0
            List<TestResponse> result = db.Tests.Where(test => test.TopicId== topicId).Select(test => new TestResponse { Id = test.Id, Image = test.ImagePath, Title = test.Title, Rating = 0 }).ToList();
            //если данные не найдены - возврвщаем пустой список 
            if (result == null) return result;
            //Получаем результаты всех тестов, пройденных пользователем
            List<UserTest> usersTestsList = db.UsersTests.Where(userTest => userTest.UserId==userId).ToList();
            //Для каждого теста пройденного пользователем
            foreach (var usertest in usersTestsList)
            {
                //ищем соответствующий тест в итоговом списке
                var test = result.Find(testResp => testResp.Id == usertest.TestId);
                if (test == null) continue;
                //меняем рейтинг с 0 на результат пользователя
                else test.Rating = usertest.Rating;
            }
            return result;              
        }

        public TestWithQuestionsResponse GetTestWithQuestions(int testId)
        {
            //определяем символ по которому будет разбиваться строка
            string[] questionSeparator = { "&&&" };
            //выбираем вопросы данного теста
            var questions = db.Questions.Where(question => question.TestId==testId)
                //преобразуем данные в необходимый формат
                                        .Select(question => new QuestionResponse { 
                                            Id = question.Id, Question= question.QuestionText, 
                                            Type= Convert.ToInt32(question.Type), 
                                            Correct= question.Correct,
                                            //Преобразуем строку в массив вариантов ответов
                                            Options = question.Options.Split(questionSeparator, System.StringSplitOptions.RemoveEmptyEntries).ToList()})
                                        .ToList();
            return new TestWithQuestionsResponse { Id = testId, Questions = questions };        
        }


        public void UpdateRating (string userId, int testId, int newRating)
        {
            var userTest = db.UsersTests.FirstOrDefault(userTest => userTest.UserId == userId && userTest.TestId == testId);
            if (userTest == null)
            {
                db.UsersTests.Add(new UserTest { UserId = userId, TestId = testId, Rating = newRating });
                Save();
                return;
            }
            userTest.Rating = newRating; 
            db.UsersTests.Update(userTest);
            Save();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}




//context.UsersTests.Where(usertest => usertest.UserId == userId)
//                     .Join(context.Tests,
//                           usertest => usertest.TestId,
//                           test => test.Id,
//                           (usertest, test) => new TestResponse { Id = test.Id, Image = test.ImagePath, Title = test.Title, Rating = usertest.Rating })
//                     .ToList();
