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
    builder.EntitySet<certification>("certifications");
    builder.EntitySet<user>("users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class certificationsController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/certifications
        [EnableQuery]
        public IQueryable<certification> Getcertifications()
        {
            return db.certifications;
        }

        // GET: odata/certifications(5)
        [EnableQuery]
        public SingleResult<certification> Getcertification([FromODataUri] int key)
        {
            return SingleResult.Create(db.certifications.Where(certification => certification.certification_id == key));
        }

        // PUT: odata/certifications(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<certification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            certification certification = db.certifications.Find(key);
            if (certification == null)
            {
                return NotFound();
            }

            patch.Put(certification);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!certificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(certification);
        }

        // POST: odata/certifications
        public IHttpActionResult Post(certification certification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.certifications.Add(certification);
            db.SaveChanges();

            return Created(certification);
        }

        // PATCH: odata/certifications(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<certification> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            certification certification = db.certifications.Find(key);
            if (certification == null)
            {
                return NotFound();
            }

            patch.Patch(certification);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!certificationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(certification);
        }

        // DELETE: odata/certifications(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            certification certification = db.certifications.Find(key);
            if (certification == null)
            {
                return NotFound();
            }

            db.certifications.Remove(certification);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/certifications(5)/user
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.certifications.Where(m => m.certification_id == key).Select(m => m.user));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool certificationExists(int key)
        {
            return db.certifications.Count(e => e.certification_id == key) > 0;
        }
    }
}
