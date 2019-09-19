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
    public class StylesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Styles
        public IQueryable<Style> GetStyle()
        {
            return db.Style;
        }

        // GET: api/Styles/5
        [ResponseType(typeof(Style))]
        public async Task<IHttpActionResult> GetStyle(int id)
        {
            Style style = await db.Style.FindAsync(id);
            if (style == null)
            {
                return NotFound();
            }

            return Ok(style);
        }

        // PUT: api/Styles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStyle(int id, Style style)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != style.IdStyle)
            {
                return BadRequest();
            }

            db.Entry(style).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StyleExists(id))
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

        // POST: api/Styles
        [ResponseType(typeof(Style))]
        public async Task<IHttpActionResult> PostStyle(Style style)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Style.Add(style);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = style.IdStyle }, style);
        }

        // DELETE: api/Styles/5
        [ResponseType(typeof(Style))]
        public async Task<IHttpActionResult> DeleteStyle(int id)
        {
            Style style = await db.Style.FindAsync(id);
            if (style == null)
            {
                return NotFound();
            }

            db.Style.Remove(style);
            await db.SaveChangesAsync();

            return Ok(style);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StyleExists(int id)
        {
            return db.Style.Count(e => e.IdStyle == id) > 0;
        }
    }
}