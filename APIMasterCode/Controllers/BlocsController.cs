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
    public class BlocsController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Blocs
        public IQueryable<Bloc> GetBloc()
        {
            return db.Bloc;
        }

        // GET: api/Blocs/5
        [ResponseType(typeof(Bloc))]
        public async Task<IHttpActionResult> GetBloc(int id)
        {
            Bloc bloc = await db.Bloc.FindAsync(id);
            if (bloc == null)
            {
                return NotFound();
            }

            return Ok(bloc);
        }

        // PUT: api/Blocs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBloc(int id, Bloc bloc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloc.IdBloc)
            {
                return BadRequest();
            }

            db.Entry(bloc).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlocExists(id))
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

        // POST: api/Blocs
        [ResponseType(typeof(Bloc))]
        public async Task<IHttpActionResult> PostBloc(Bloc bloc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bloc.Add(bloc);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bloc.IdBloc }, bloc);
        }

        // DELETE: api/Blocs/5
        [ResponseType(typeof(Bloc))]
        public async Task<IHttpActionResult> DeleteBloc(int id)
        {
            Bloc bloc = await db.Bloc.FindAsync(id);
            if (bloc == null)
            {
                return NotFound();
            }

            db.Bloc.Remove(bloc);
            await db.SaveChangesAsync();

            return Ok(bloc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlocExists(int id)
        {
            return db.Bloc.Count(e => e.IdBloc == id) > 0;
        }
    }
}