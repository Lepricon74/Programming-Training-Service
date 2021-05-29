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
    public class NotesController : Controller
    {
        INoteRepository _notesRepository;
        private readonly UserManager<User> _userManager;
        public NotesController(UserManager<User> userManager, TrainingServiceContext dbcontext)
        {
            _userManager = userManager;
            _notesRepository = new SQLNotesRepository(dbcontext);                
        }

        [Route("")]
        [HttpGet]
        public JsonResult GetUserNotes()
        {
            return new JsonResult(_notesRepository.GetUserNotes(_userManager.GetUserId(User)));
        }

        [Route("addnote")]
        [HttpPost]
        public JsonResult AddNote()
        {
            //определяем специальный класс StreamReader для чтения данных из тела запроса
            var stream = new StreamReader(Request.Body);
            //считываем данные
            var body = stream.ReadToEndAsync().Result;
            //создаем экземпляр класса описывающего заметку для добавления
            NewNoteRequest newNote;
            //пытаемся конвертировать данные JSON в объект класса NewNoteRequest
            try
            {
                newNote = JsonConvert.DeserializeObject<NewNoteRequest>(body);
            }
            //в случае возникновения отлавливаем ошибки
            catch (JsonReaderException e)
            {
                return new JsonResult(e.ToString());
            }
            //проверяем, что все поля заполнены
            if (!TryValidateModel(newNote)) return new JsonResult("Note Content Error!");
            //получаем id пользователя
            string userId = _userManager.GetUserId(User);
            //определяем id новой заметки
            int newId = _notesRepository.GetNewUserNoteId(userId);
            //добавляем новую заметку, преобразуя данные в класс модели БД - Note
            _notesRepository.AddNote(new Note { UserId = userId, Id = newId, Text = newNote.Text, Title = newNote.Title });
            //сообщаем об успешном добавлении
            return new JsonResult("success");
        }

        //[Route("removenote")]
        //public JsonResult DeleteNote(int noteId)
        //{
        //    _notesRepository.DeleteNote(_userManager.GetUserId(User), noteId);
        //    return new JsonResult("success");
        //}
    }
}
