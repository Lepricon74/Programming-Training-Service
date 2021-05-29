using Microsoft.AspNetCore.Mvc;
using TrainingService.DBRepository.Interfaces;
using TrainingService.DBRepository.Repositories;
using TrainingService.DBRepository;
using Microsoft.AspNetCore.Identity;
using TrainingService.Models;
using TrainingService.Models.RequestsModels;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace TrainingService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TestsController : Controller
    {
        
        ITestRepository _testRepository;
        private readonly UserManager<User> _userManager;
        public TestsController(UserManager<User> userManager, TrainingServiceContext dbcontext)
        {
            _userManager = userManager;
            _testRepository = new SQLTestsRepository(dbcontext);
        }

        [Route("{topicName:maxlength(10)}")]
        [HttpGet]
        public JsonResult GetTestsWithUserRating(string topicName)
        {
            return new JsonResult(_testRepository.GetTestsWithUserRating(_userManager.GetUserId(User), topicName));
        }
        [Route("{topicString:maxlength(10)}/{testId:int}")]
        [HttpGet]
        public JsonResult GetTestWithQuestions(int testId)
        {
            if (testId == 0) return new JsonResult("Error");
            return new JsonResult(_testRepository.GetTestWithQuestions(testId));
        }
        [Route("updaterating")]
        [HttpPut]
        public JsonResult UpdateRating()
        {

            var stream = new StreamReader(Request.Body);
            var body = stream.ReadToEndAsync().Result;
            NewRatingRequest newRating;
            try
            {
                newRating = JsonConvert.DeserializeObject<NewRatingRequest>(body);
            }
            catch (JsonReaderException e)
            {
                return new JsonResult(e.ToString());
            }
            if (TryValidateModel(newRating)==false) return new JsonResult("Note Content Error!");
            _testRepository.UpdateRating(_userManager.GetUserId(User), newRating.TestId, newRating.Rating);
            return new JsonResult("success");
        }
    }
}
