﻿using System;
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
using System.Web;
using Newtonsoft.Json;

namespace BookWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private BooksLEntities db = new BooksLEntities();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.CateID }, category);
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.CateID == id) > 0;
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
        public IHttpActionResult PagingCategory(int currentPage, int pageSize, string lookfor)
        {
            object pageInfo = null;
            int skip = (currentPage - 1) * pageSize;
            if (String.IsNullOrEmpty(lookfor))
            {
                pageInfo = new
                {

                    category = db.Categories.OrderBy(x => x.CateID).AsQueryable().Skip(skip).Take(pageSize).ToList(),
                    total = db.Categories.Count()
                };
            }
            else
            {
                pageInfo = new
                {

                    category = db.Categories.OrderBy(x => x.CateID).AsQueryable().Where(x => x.CateName.Contains(lookfor)).Skip(skip).Take(pageSize).ToList(),
                    total = db.Categories.Count()
                };
            }
            return Ok(pageInfo);

        }
        // GET: api/Categories (Name, ID)
        [Route("api/Categories/getID_Name")]
        public IHttpActionResult GetCategoriesIDName()
        {
            object obj = null;
            obj = new
            {
                cateInfo = db.Categories.Select(x => new
                {
                    x.CateID,
                    x.CateName
                })


            };
            return Ok(obj);
        }
    }
}