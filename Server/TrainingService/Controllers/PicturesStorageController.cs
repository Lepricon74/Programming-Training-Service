using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TrainingService.Controllers
{
    [Route("api/[controller]")]

    public class PicturesStorageController : Controller
    {
      
        IWebHostEnvironment _appEnvironment;
     
        public PicturesStorageController( IWebHostEnvironment appEnvironment)
        {          
            _appEnvironment = appEnvironment;
        }

        [Route("")]
        [HttpGet]
        public IActionResult AddPicture()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> AddPicture(IFormFile uploadedPicture)
        {
            if (uploadedPicture != null)
            {
                string PicturePath = await PicturesStorageLogic.AddPicture(uploadedPicture, _appEnvironment);
                return Content(HttpContext.Request.Host + PicturePath);
            }
            return Content("Error");
        }
    }
    public static class PicturesStorageLogic
    {
        public static async Task<String> AddPicture(IFormFile uploadedPicture, IWebHostEnvironment appEnvironment)
        {
            //проверка существования файлы
            if (uploadedPicture != null)
            {
                //id картинки получаем посредством вычисления хэша
                int pictureId = uploadedPicture.OpenReadStream().GetHashCode();
                //формируем относительный путь с именем файла
                string CommonPicturePath = "/Files/PicturesStorage/" + pictureId.ToString() + new Random().Next(0, 100).ToString() + ".jpg";
                //формируем полный путь с именем файла, используя информацию о среде выполнения
                string LocalPicturePath = appEnvironment.WebRootPath + CommonPicturePath;
                //создаем файл используя полный путь
                using (var fileStream = new FileStream(LocalPicturePath, FileMode.Create))
                {
                    //копируем звгруженный файл в файловую систему
                    await uploadedPicture.CopyToAsync(fileStream);
                }
                //возврвщаем путь, по которому можно получить доступ к файлу
                return  CommonPicturePath;
            }
            return "error";
        }

    }
}
