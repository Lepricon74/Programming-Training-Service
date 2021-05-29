using System.Collections.Generic;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;


namespace TrainingService.DBRepository.Interfaces
{
	public interface INoteRepository 
	{
		List<NoteResponse> GetUserNotes(string userId);
		void DeleteNote(string userId, int noteId);
		void AddNote(Note newNote);
		int GetNewUserNoteId(string userId);
	}
}
