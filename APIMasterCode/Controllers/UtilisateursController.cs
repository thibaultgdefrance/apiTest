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
using Newtonsoft.Json;

namespace APIMasterCode.Controllers
{
    public class UtilisateursController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();

        // GET: api/Utilisateurs
        public IQueryable<Utilisateur> GetUtilisateur()
        {
            return db.Utilisateur;
        }

        // GET: api/Utilisateurs/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> GetUtilisateur(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }


        // GET: api/Utilisateurs/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<Utilisateur> GetLogin(string Email, string MDP,bool clafouti)
        {
            Utilisateur utilisateur= (from u in db.Utilisateur where u.Email == Email && u.MotdePasse == MDP select u).First();
            
            if (utilisateur != null)
            {
                
                return await Task.FromResult(utilisateur);
            }
            else
            {
                return null;
            }
        }
        // PUT: api/Utilisateurs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest();
            }

            db.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // PUT: api/Utilisateurs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUtilisateur(int id,Utilisateur utilisateur,bool croquette)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.IdUtilisateur)
            {
                return BadRequest();
            }

            db.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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


        // POST: api/Utilisateurs
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Utilisateur.Add(utilisateur);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateur.IdUtilisateur }, utilisateur);
        }
        


        // DELETE: api/Utilisateurs/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> DeleteUtilisateur(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            db.Utilisateur.Remove(utilisateur);
            await db.SaveChangesAsync();

            return Ok(utilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UtilisateurExists(int id)
        {
            return db.Utilisateur.Count(e => e.IdUtilisateur == id) > 0;
        }
    }
}