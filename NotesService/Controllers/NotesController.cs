using NotesService.DAL;
using NotesService.Models;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NotesService.Controllers
{
	public class NotesController : ApiController
    {
        private readonly NoteContext db = new NoteContext();

        // GET: api/Tenant123/Notes
        public IQueryable<Note> GetNotes(string tenantId)
        {
            return db.Notes.Where(n => n.TenantId == tenantId);
        }

        // GET: api/Tenant123/Notes/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult GetNote(string tenantId, string id)
        {
            Note note = db.Notes.Find(id);
            if (note == null || note.TenantId != tenantId)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Tenant123/Notes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNote(string tenantId, int id, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id || tenantId != note.TenantId)
            {
                return BadRequest();
            }

            db.Entry(note).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(tenantId, id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tenant123/Notes
        [ResponseType(typeof(Note))]
        public IHttpActionResult PostNote(string tenantId, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            note.TenantId = tenantId;

            db.Notes.Add(note);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NoteExists(tenantId, note.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = note.Id }, note);
        }

        // DELETE: api/Tenant123/Notes/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult DeleteNote(string tenantId, int id)
        {
            Note note = db.Notes.Find(id);
            if (note == null || note.TenantId != tenantId)
            {
                return NotFound();
            }

            db.Notes.Remove(note);
            db.SaveChanges();

            return Ok(note);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoteExists(string tenantId, int id)
        {
            return db.Notes.Count(e => e.Id == id && e.TenantId == tenantId) > 0;
        }
    }
}