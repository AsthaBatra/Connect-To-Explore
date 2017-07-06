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
    builder.EntitySet<education>("educations");
    builder.EntitySet<user>("users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class educationsController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/educations
        [EnableQuery]
        public IQueryable<education> Geteducations()
        {
            return db.educations;
        }

        // GET: odata/educations(5)
        [EnableQuery]
        public SingleResult<education> Geteducation([FromODataUri] int key)
        {
            return SingleResult.Create(db.educations.Where(education => education.education_id == key));
        }

        // PUT: odata/educations(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<education> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            education education = db.educations.Find(key);
            if (education == null)
            {
                return NotFound();
            }

            patch.Put(education);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!educationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(education);
        }

        // POST: odata/educations
        public IHttpActionResult Post(education education)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.educations.Add(education);
            db.SaveChanges();

            return Created(education);
        }

        // PATCH: odata/educations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<education> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            education education = db.educations.Find(key);
            if (education == null)
            {
                return NotFound();
            }

            patch.Patch(education);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!educationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(education);
        }

        // DELETE: odata/educations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            education education = db.educations.Find(key);
            if (education == null)
            {
                return NotFound();
            }

            db.educations.Remove(education);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/educations(5)/user
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.educations.Where(m => m.education_id == key).Select(m => m.user));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool educationExists(int key)
        {
            return db.educations.Count(e => e.education_id == key) > 0;
        }
    }
}
