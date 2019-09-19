using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using APIMasterCode.Models;

namespace APIMasterCode.Controllers
{
    public class TypeBlocsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/TypeBlocs
        public IQueryable<TypeBloc> GetTypeBloc()
        {
            return db.TypeBloc;
        }

        // GET: api/TypeBlocs/5
        [ResponseType(typeof(TypeBloc))]
        public async Task<IHttpActionResult> GetTypeBloc(int id)
        {
            TypeBloc typeBloc = await db.TypeBloc.FindAsync(id);
            if (typeBloc == null)
            {
                return NotFound();
            }

            return Ok(typeBloc);
        }

        // PUT: api/TypeBlocs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTypeBloc(int id, TypeBloc typeBloc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeBloc.IdTypeBloc)
            {
                return BadRequest();
            }

            db.Entry(typeBloc).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeBlocExists(id))
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

        // POST: api/TypeBlocs
        [ResponseType(typeof(TypeBloc))]
        public async Task<IHttpActionResult> PostTypeBloc(TypeBloc typeBloc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TypeBloc.Add(typeBloc);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = typeBloc.IdTypeBloc }, typeBloc);
        }

        // DELETE: api/TypeBlocs/5
        [ResponseType(typeof(TypeBloc))]
        public async Task<IHttpActionResult> DeleteTypeBloc(int id)
        {
            TypeBloc typeBloc = await db.TypeBloc.FindAsync(id);
            if (typeBloc == null)
            {
                return NotFound();
            }

            db.TypeBloc.Remove(typeBloc);
            await db.SaveChangesAsync();

            return Ok(typeBloc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeBlocExists(int id)
        {
            return db.TypeBloc.Count(e => e.IdTypeBloc == id) > 0;
        }
    }
}