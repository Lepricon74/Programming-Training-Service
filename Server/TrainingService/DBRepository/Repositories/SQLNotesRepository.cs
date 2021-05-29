using System;
using System.Collections.Generic;
using System.Linq;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;
using TrainingService.DBRepository.Interfaces;

namespace TrainingService.DBRepository.Repositories
{
    public class SQLNotesRepository : INoteRepository
    {
        private TrainingServiceContext context;

        public SQLNotesRepository(TrainingServiceContext _db)
        {
            context = _db;
        }

        public List<NoteResponse> GetUserNotes(string userId)
        {
            return context.Notes.Where(note => note.UserId==userId)
                                .Select(note => new NoteResponse { Id=note.Id,Title=note.Title,Text=note.Text})
                                .ToList();
        }

        public void AddNote(Note newNote)
        {
            context.Notes.Add(newNote);
            Save();
        }
        public void DeleteNote(string userId, int noteId)
        {
            Note  noteToRemove = context.Notes
                    .Where(note => note.Id == noteId && note.UserId == userId)
                    .FirstOrDefault();
            if (noteToRemove == null) return;
            context.Notes.Remove(noteToRemove);
            Save();
        }
        public int GetNewUserNoteId(string userId)
        {
            var userNotesList = context.Notes.Where(Note => Note.UserId==userId).ToList();
            return (userNotesList.Count() == 0) ? 1 : userNotesList.Last().Id+1;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
