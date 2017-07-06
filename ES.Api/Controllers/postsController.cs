using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ES.Data;

namespace ES.Api.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ES.Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<post>("posts");
    builder.EntitySet<user>("users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class postsController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/posts
        [EnableQuery]
        public IQueryable<post> Getposts()
        {
            return db.posts;
        }

        // GET: odata/posts(5)
        [EnableQuery]
        public SingleResult<post> Getpost([FromODataUri] int key)
        {
            return SingleResult.Create(db.posts.Where(post => post.post_id == key));
        }

        // PUT: odata/posts(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<post> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post post = db.posts.Find(key);
            if (post == null)
            {
                return NotFound();
            }

            patch.Put(post);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(post);
        }

        // POST: odata/posts
        public IHttpActionResult Post(post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.posts.Add(post);
            db.SaveChanges();

            return Created(post);
        }

        // PATCH: odata/posts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<post> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post post = db.posts.Find(key);
            if (post == null)
            {
                return NotFound();
            }

            patch.Patch(post);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(post);
        }

        // DELETE: odata/posts(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            post post = db.posts.Find(key);
            if (post == null)
            {
                return NotFound();
            }

            db.posts.Remove(post);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/posts(5)/user
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.posts.Where(m => m.post_id == key).Select(m => m.user));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool postExists(int key)
        {
            return db.posts.Count(e => e.post_id == key) > 0;
        }
    }
}
