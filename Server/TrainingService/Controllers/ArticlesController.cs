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
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace TrainingService.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        IArticleRepository _articlesRepository;
        IWebHostEnvironment _appEnvironment;
        private readonly UserManager<User> _userManager;

        public ArticlesController(UserManager<User> userManager, TrainingServiceContext dbcontext, IWebHostEnvironment appEnvironment)
        {
            _articlesRepository = new SQLArticlesRepository(dbcontext);
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        [Route("")]
        [HttpGet]
        public JsonResult GetArticles()
        {                    
            return new JsonResult(_articlesRepository.GetArticlesList());
        }

        [Route("{articleId:int}")]
        [HttpGet]
        public VirtualFileResult GetArticle(int articleId)
        {
            //получаем информацию о статье с переданным id из БД
            var article = _articlesRepository.GetArticle(articleId);
            //Если статья не найдена - возвращаем страницу и информацией об ошибке
            if (article == null) return File("/Files/Error.html", "text/html");
            //Обращаемся к файловой системе и возвращаем файл по полученному из БД пути и заданного типа
            return File(article.Path, "text/html");
        }


        [Route("addarticle")]
        [HttpGet]
        public IActionResult AddArticle()
        {
            return View();
        }

        [Route("addarticle")]
        [HttpPost]
        public async Task<IActionResult> AddArticle(string title, string description, IFormFile uploadedNewArticleHTML, IFormFile uploadedArticlePicture)
        {
            if (uploadedNewArticleHTML != null)
            {
                //формируем путь к папке с уроками
                string newLessonFolderPath = _appEnvironment.WebRootPath + "/Files/Articles";
                //формируем путь к самому файлу
                string htmlPath = newLessonFolderPath + "/" + uploadedNewArticleHTML.FileName;
                //создаем файл используя созданный путь
                using (var fileStream = new FileStream(htmlPath, FileMode.Create))
                {
                    // сохраняем файл в папку Files в каталоге wwwroot
                    await uploadedNewArticleHTML.CopyToAsync(fileStream);
                }
                //создаем новую статью используя полученную информацию
                Article newArticle = new Article { 
                    //присваиваем id автора
                    UserId = _userManager.GetUserId(User),
                    Title =title,
                    Description=description, 
                    Path = "/Files/Articles/" + uploadedNewArticleHTML.FileName, 
                    //добавляем картинку в файловой системе и сохраняем путь к ней
                    ImagePath = await PicturesStorageLogic.AddPicture(uploadedArticlePicture, _appEnvironment), 
                    //определяем дату добавления
                    Date = DateTime.Now.Date.ToString().Split(' ')[0]
                };
                //добавляем информацию в соответствующую таблицу в БД
                _articlesRepository.AddArticle(newArticle);
            }
            return Redirect("~/");
        }
    }
}
