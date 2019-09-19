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
using Cryptage2;
using Newtonsoft.Json;

namespace APIMasterCode.Controllers
{
    public class MessagesController : ApiController
    {
        private MasterCodeEntities db = new MasterCodeEntities();
        ClefDeCryptage2 clef = new ClefDeCryptage2();
       
        // GET: api/Messages
        public IQueryable<Message> GetMessage()
        {

            return db.Message;
        }


        


        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }
        public string GetForumMessage(int idForum)
        {
            
            
            Forum forum = new Forum();
            string reponse = "[{";
            List<Message> messages = (from m in db.Message where m.IdForum == idForum select m).ToList();
            
            List<Message> listeMessages = new List<Message>();
            foreach (var item in messages)
            {

                
                Message message = new Message();
                message.IdMessage = item.IdMessage;
                message.IdForum = item.IdForum;
                message.IdAuteur = item.IdAuteur;
                message.IdMessageParent = item.IdMessageParent;
                message.Popularite = item.Popularite;
                message.Texte = item.Texte;
                message.DatePublication = item.DatePublication;
                message.IdStatut = item.IdStatut;
                //Utilisateur auteur =(Utilisateur)(from u in db.Utilisateur where u.IdUtilisateur == item.IdAuteur select u);
                string nbrMessagesAuteur = (from m in db.Message where m.IdAuteur == item.IdAuteur select m).Count().ToString();
                List<Utilisateur> auteur = (from u in db.Utilisateur where u.IdUtilisateur == item.IdAuteur select u).ToList();
                List<Statut> statut = (from s in db.Statut where s.IdStatut == item.IdStatut select s).ToList();
                //listeMessages.Add(message);
                reponse += "\"IdMessage:\""+ "\""+message.IdMessage+ "\""+",";
                reponse += "\"Text:\"" + "\""+ message.Texte+ "\""+",";
                reponse += "\"Auteur:\"" + "\""+ auteur[0].Nom+ "\""+ ",";
                reponse += "\"CheminAvatar:\"" + "\""+ "~/media/avatar1.jpg" + "\""+ ",";
                reponse += "\"nbrPostUtilisateur:\"" + "\""+ nbrMessagesAuteur + "\""+ ",";
                reponse += "\"DatePublication:\"" + "\""+ message.DatePublication + "\""+ ",";
                reponse += "\"email:\"" + "\""+ auteur[0].Email+ "\""+",";
                reponse += "\"Statut:\"" + "\""+statut[0].typeStatut + "\""+",";
                reponse += "\"Popularite:\"" + "\"" + message.Popularite + "\"";



                //reponse += "Auteur:" + auteur +",";
                //reponse += "nbrPostAuteur:" + nbMsgUtilisateur + ",";
                reponse += "},{";
            }

            
            reponse += "}]";
            string json = JsonConvert.SerializeObject(reponse);
            return reponse;

        }

        public List<MessageForum> GetMessagesForum(int IdForum,bool sandwich)
        {
            List<MessageForum> messagesForum = new List<MessageForum>();
            
            List<Message> messages = (from m in db.Message where m.IdForum == IdForum select m).ToList();

            foreach (var item in messages)
            {
                MessageForum message = new MessageForum();
                message.IdForum = IdForum.ToString();
                message.PseudoUtilisateur = item.Utilisateur.Pseudo;
                DateTime dt = (DateTime)item.DatePublication;
                message.DatePublication = dt.ToShortDateString();
                message.TexteMessage = item.Texte;
                message.TitreForum = item.Forum.Sujet;
                message.CheminAvatar = item.Utilisateur.CheminAvatar;
                messagesForum.Add(message);
            }

            return messagesForum;
        }




        /*public IQueryable<Message> GetMessage(int idForum)
        {
            List<Message> messages=(from m in db.Message where m.IdForum == idForum select m).ToList();
            List<Message> listeMessages = new List<Message>();
            foreach (var item in messages)
            {
                Message message = new Message();
                message.IdMessage = item.IdMessage;
                message.IdForum = item.IdForum;
                message.IdAuteur = item.IdAuteur;
                message.IdMessageParent = item.IdMessageParent;
                message.Popularite = item.Popularite;
                message.Texte = item.Texte;
                message.DatePublication = item.DatePublication;
                message.IdStatut = item.IdStatut;
                listeMessages.Add(message);
            }
            return listeMessages.AsQueryable();
        }*/




        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.IdMessage)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> PostMessage(Message message,string token,int IdForumSelectionne, int idAuteur,string texteMessage)
        {
            if (clef.verify(token)==true)
            {
                /*if (!ModelState.IsValid)
                {
                   // return BadRequest(ModelState);
                }*/
                message.DatePublication = DateTime.Now;
                message.IdAuteur = idAuteur;
                message.IdForum = IdForumSelectionne;
                message.IdStatut = 1;
                message.Popularite = 0;
                message.Texte = texteMessage;
                message.IdMessageParent = null;
                db.Message.Add(message);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest(ModelState);
                    
                }
                

                return CreatedAtRoute("DefaultApi", new { id = message.IdMessage }, message);
            }else
            {
                return null;
            }
           
        }



        public async Task<IHttpActionResult> PostMessages(Message message)
        {
            Message message2 = new Message
            {
                DatePublication = Convert.ToDateTime(message.DatePublication),
                IdForum = Convert.ToInt32(message.IdForum),
                IdMessageParent = null,
                IdStatut = Convert.ToInt32(message.IdStatut),
                Texte=message.Texte,
                Popularite=Convert.ToInt32(message.Popularite),
                IdAuteur=Convert.ToInt32(message.IdAuteur)
            };
                /*if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }*/
               
                db.Message.Add(message2);
                await db.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = message.IdMessage }, message);
            }
           

        

        








        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            Message message = await db.Message.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Message.Remove(message);
            await db.SaveChangesAsync();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Message.Count(e => e.IdMessage == id) > 0;
        }



        public IQueryable<Message> GetMessage3()
        {
            return db.Message;
        }
    }
}