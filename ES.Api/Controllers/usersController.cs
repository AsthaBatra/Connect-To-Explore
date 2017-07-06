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
    builder.EntitySet<user>("users");
    builder.EntitySet<certification>("certifications"); 
    builder.EntitySet<connection>("connections"); 
    builder.EntitySet<education>("educations"); 
    builder.EntitySet<post>("posts"); 
    builder.EntitySet<users_details>("users_details"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class usersController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/users
        [EnableQuery]
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        // GET: odata/users(5)
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.users.Where(user => user.id == key));
        }

        // PUT: odata/users(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<user> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user user = db.users.Find(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Put(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // POST: odata/users
        public IHttpActionResult Post(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            db.SaveChanges();

            return Created(user);
        }

        // PATCH: odata/users(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<user> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user user = db.users.Find(key);
            if (user == null)
            {
                return NotFound();
            }

            patch.Patch(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(user);
        }

        // DELETE: odata/users(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            user user = db.users.Find(key);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/users(5)/certifications
        [EnableQuery]
        public IQueryable<certification> Getcertifications([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.certifications);
        }

        // GET: odata/users(5)/connections
        [EnableQuery]
        public IQueryable<connection> Getconnections([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.connections);
        }

        // GET: odata/users(5)/connections1
        [EnableQuery]
        public IQueryable<connection> Getconnections1([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.connections1);
        }

        // GET: odata/users(5)/educations
        [EnableQuery]
        public IQueryable<education> Geteducations([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.educations);
        }

        // GET: odata/users(5)/posts
        [EnableQuery]
        public IQueryable<post> Getposts([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.posts);
        }

        // GET: odata/users(5)/users_details
        [EnableQuery]
        public IQueryable<users_details> Getusers_details([FromODataUri] int key)
        {
            return db.users.Where(m => m.id == key).SelectMany(m => m.users_details);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(int key)
        {
            return db.users.Count(e => e.id == key) > 0;
        }
    }
}
