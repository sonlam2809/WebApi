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
    public class BooksController : ApiController
    {
        private BooksLEntities db = new BooksLEntities();

        // GET: api/Books
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        // GET: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookID)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.BookID }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookID == id) > 0;
        }

        [HttpGet]
        //[Route("api/Authors/{currentPage}/{pageSize}/{lookfor}")]
        /// GET: api/Authors/1/10
        public IHttpActionResult PagingBook(int currentPage, int pageSize, string lookfor)
        {
            object pageInfo = null;
            int skip = (currentPage - 1) * pageSize;
            if (String.IsNullOrEmpty(lookfor))
            {
                pageInfo = new
                {

                    book = db.Books.OrderBy(x => x.BookID).AsQueryable().Skip(skip).Take(pageSize).ToList(),
                    total = db.Books.Count()
                };
            }
            else
            {
                pageInfo = new
                {

                    book = db.Books.OrderBy(x => x.CateID).AsQueryable().Where(x => x.Title.Contains(lookfor)).Skip(skip).Take(pageSize).ToList(),
                    total = db.Books.Count()
                };
            }
            return Ok(pageInfo);

        }
        // GET: api/Books/getID_NameBookStatus (Name, ID)
        [Route("api/Books/BookStatus")]
        public IHttpActionResult GetBookStatus()
        {
            object obj = null;
            obj = new
            {
                BookStatusInfo = db.StatusBooks.Select(x => new
                {
                    x.BookStatusID,
                    x.BookStatus
                })


            };
            return Ok(obj);
        }

    }
}