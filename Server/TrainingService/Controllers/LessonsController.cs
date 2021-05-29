using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingService.DBRepository.Interfaces;
using TrainingService.DBRepository.Repositories;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using TrainingService.DBRepository;


namespace TrainingService.Controllers
{
    [Route("api/[controller]")]
    public class LessonsController : Controller
    {
        ILessonRepository _lessonsRepository;
        IWebHostEnvironment _appEnvironment;
      
        public LessonsController(TrainingServiceContext dbcontext, IWebHostEnvironment appEnvironment)
        {
            _lessonsRepository = new SQLLessonsRepository(dbcontext);
            _appEnvironment = appEnvironment;
        }

        [Route("{topicName:maxlength(10)}")]
        [HttpGet]
        public JsonResult GetLessons(string topicName)
        {         
            return new JsonResult(_lessonsRepository.GetLessonsList(topicName));
        }
        [Route("topics")]
        [HttpGet]
        public JsonResult GetTopics()
        {           
            return new JsonResult(_lessonsRepository.GetTopics());
        }

        [Route("{topicName:maxlength(10)}/{sectionId:int}/{lessonId:int}")]
        [HttpGet]
        public VirtualFileResult GetLesson(string topicName, int sectionId, int lessonId)
        {
            //находим урок по переданным параметрам
            Lesson lesson = _lessonsRepository.GetLesson(topicName, sectionId, lessonId);           
            //если урок не найден - возвращаем страницу с ошибкой
            if (lesson == null) return File("/Files/Error.html", "text/html");
            //по указанному в БД пути возвращаем html документ
            return File(lesson.Path, "text/html");
        }

        
        [Route("addlesson")]
        [HttpGet]
        public IActionResult AddLesson()
        {
            return View();
        }

        [Route("addlesson")]
        [HttpPost]
        public async Task<IActionResult> AddLesson(int topicId, int sectionId, string newLessonName, IFormFile uploadedNewLessonHTML)
        {
            if (uploadedNewLessonHTML != null)
            {
                int newId = _lessonsRepository.GetNewLessonId(topicId, sectionId);
                ////формируем путь к папке с уроками
                string newLessonFolderPath = _appEnvironment.WebRootPath + "/Files/Lessons";
                string htmlPath = newLessonFolderPath + "/" + uploadedNewLessonHTML.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(htmlPath, FileMode.Create))
                {
                    await uploadedNewLessonHTML.CopyToAsync(fileStream);
                }
                Lesson newLesson = new Lesson { Id=newId, Name = newLessonName, Path = "/Files/Lessons/" + uploadedNewLessonHTML.FileName,SectionId=sectionId,SectionTopicId=topicId };
                _lessonsRepository.AddLesson(newLesson);
            }
            return Redirect("~/");
        }
    }
}



//[HttpGet]
//public ActionResult Picture()
//{
//    if ((Int32.TryParse(Request.Query["lessonId"].ToString(), out int lessonId) == false) || (lessonId < 1)) return new JsonResult(null);
//    if ((Int32.TryParse(Request.Query["position"].ToString(), out int imagePosition) == false) || (imagePosition < 1)) return new JsonResult(null);
//    var result = _lessonRepository.GetPicture(lessonId, imagePosition);
//    return base.File(result.Picture, "image/png");
//}



//string newLessonPicturesFolderPath = newLessonFolderPath + "/pictures";
//if (!Directory.Exists(newLessonPicturesFolderPath)) Directory.CreateDirectory(newLessonPicturesFolderPath);

//foreach (var uploadedPicture in uploadedLessonPictures)
//{
//    // путь к картинке в папке Lessons/id/pictures
//    string picturePath = newLessonPicturesFolderPath + "/" + uploadedPicture.FileName;
//    // сохраняем файл в папку Files в каталоге wwwroot
//    using (var fileStream = new FileStream(picturePath, FileMode.Create))
//    {
//        await uploadedPicture.CopyToAsync(fileStream);
//    }
//}