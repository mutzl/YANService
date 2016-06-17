namespace NotesService.DAL
{
	using System.Data.Entity;

	using NotesService.Models;

	public class NoteContext : DbContext
	{
		public NoteContext() : base("NotesConnectionString")
		{
			
		}
		public DbSet<Note> Notes { get; set; } 
	}
}