using System;

namespace NotesService.Models
{
	public class Note 
	{
		public int Id { get; set; }

		public string TenantId { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public DateTime CreatedAt { get; set; }

		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}