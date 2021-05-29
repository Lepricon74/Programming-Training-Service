using System.Collections.Generic;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;

namespace TrainingService.DBRepository.Interfaces
{
    public interface ITestRepository
    {
        List<TestResponse> GetTestsWithUserRating(string userId, string topicName);
        TestWithQuestionsResponse GetTestWithQuestions(int testId);
        void UpdateRating(string userId, int testId, int newRating);
    }
}
