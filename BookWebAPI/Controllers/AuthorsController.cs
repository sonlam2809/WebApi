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
    public class AuthorsController : ApiController
    {
        private BooksLEntities db = new BooksLEntities();

        // GET: api/Authors
        public IQueryable<Author> GetAuthors()
        {
            return db.Authors;
        }

        // GET: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(author).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = author.AuthorID }, author);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.AuthorID == id) > 0;
        }

        /// <summary>
        /// get categories by page and size
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("api/Categories/{currentPage}/{pageSize}/{lookfor}")]
        /// GET: api/Categories/1/10
        public IHttpActionResult PagingAuthor(int currentPage, int pageSize, string lookfor)
        {
            object pageInfo = null;
            int skip = (currentPage - 1) * pageSize;
            if (String.IsNullOrEmpty(lookfor))
            {
                pageInfo = new
                {

                    author = db.Authors.OrderBy(x => x.AuthorID).AsQueryable().Skip(skip).Take(pageSize).ToList(),
                    total = db.Authors.Count()
                };
            }
            else
            {
                pageInfo = new
                {

                    category = db.Authors.OrderBy(x => x.AuthorID).AsQueryable().Where(x => x.AuthorName.Contains(lookfor)).Skip(skip).Take(pageSize).ToList(),
                    total = db.Authors.Count()
                };
            }
            return Ok(pageInfo);

        }
        // GET: api/Authors (Name, ID)
        [Route("api/Authors/getID_Name")]
        public IHttpActionResult GetAuthorsIDName()
        {
            object obj = null;

            obj = new
            {
                authorInfo = db.Authors.Select(x => new
                {
                    x.AuthorID,
                    x.AuthorName
                })
            };
            
            return Ok(obj);
    }
}
}