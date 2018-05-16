using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookWebAPI.Models;
using System.Web.Http.Cors;

namespace BookWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PublishersController : ApiController
    {
        private BooksLEntities db = new BooksLEntities();

        // GET: api/Publishers
        public IQueryable<Publisher> GetPublishers()
        {
            return db.Publishers;
        }

        // GET: api/Publishers/5
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult GetPublisher(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // PUT: api/Publishers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPublisher(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != publisher.PubID)
            {
                return BadRequest();
            }

            db.Entry(publisher).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publishers
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult PostPublisher(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Publishers.Add(publisher);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = publisher.PubID }, publisher);
        }

        // DELETE: api/Publishers/5
        [ResponseType(typeof(Publisher))]
        public IHttpActionResult DeletePublisher(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return NotFound();
            }

            db.Publishers.Remove(publisher);
            db.SaveChanges();

            return Ok(publisher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PublisherExists(int id)
        {
            return db.Publishers.Count(e => e.PubID == id) > 0;
        }

        [HttpGet]
        //[Route("api/Categories/{currentPage}/{pageSize}/{lookfor}")]
        /// GET: api/Categories/1/10
        public IHttpActionResult PagingPublisher(int currentPage, int pageSize, string lookfor)
        {
            object pageInfo = null;
            int skip = (currentPage - 1) * pageSize;
            if (String.IsNullOrEmpty(lookfor))
            {
                pageInfo = new
                {

                    publisher = db.Publishers.OrderBy(x => x.PubID).AsQueryable().Skip(skip).Take(pageSize).ToList(),
                    total = db.Publishers.Count()
                };
            }
            else
            {
                pageInfo = new
                {

                    publisher = db.Publishers.OrderBy(x => x.PubID).AsQueryable().Where(x => x.Name.Contains(lookfor)).Skip(skip).Take(pageSize).ToList(),
                    total = db.Publishers.Count()
                };
            }
            return Ok(pageInfo);

        }
        // GET: api/Publishers/getID_Name (Name, ID)
        [Route("api/Publishers/getID_Name")]
        public IHttpActionResult GetPublishersIDName()
        {
            object obj = null;
            obj = new
            {
                publisherInfo = db.Publishers.Select(x => new
                {
                    x.PubID,
                    x.Name
                })
            };

            return Ok(obj);
        }
    }
}