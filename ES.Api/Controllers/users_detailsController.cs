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
    builder.EntitySet<users_details>("users_details");
    builder.EntitySet<user>("users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class users_detailsController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/users_details
        [EnableQuery]
        public IQueryable<users_details> Getusers_details()
        {
            return db.users_details;
        }

        // GET: odata/users_details(5)
        [EnableQuery]
        public SingleResult<users_details> Getusers_details([FromODataUri] int key)
        {
            return SingleResult.Create(db.users_details.Where(users_details => users_details.users_details_id == key));
        }

        // PUT: odata/users_details(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<users_details> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            users_details users_details = db.users_details.Find(key);
            if (users_details == null)
            {
                return NotFound();
            }

            patch.Put(users_details);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!users_detailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(users_details);
        }

        // POST: odata/users_details
        public IHttpActionResult Post(users_details users_details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users_details.Add(users_details);
            db.SaveChanges();

            return Created(users_details);
        }

        // PATCH: odata/users_details(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<users_details> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            users_details users_details = db.users_details.Find(key);
            if (users_details == null)
            {
                return NotFound();
            }

            patch.Patch(users_details);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!users_detailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(users_details);
        }

        // DELETE: odata/users_details(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            users_details users_details = db.users_details.Find(key);
            if (users_details == null)
            {
                return NotFound();
            }

            db.users_details.Remove(users_details);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/users_details(5)/user
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.users_details.Where(m => m.users_details_id == key).Select(m => m.user));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool users_detailsExists(int key)
        {
            return db.users_details.Count(e => e.users_details_id == key) > 0;
        }
    }
}
