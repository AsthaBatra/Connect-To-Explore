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
    builder.EntitySet<connection>("connections");
    builder.EntitySet<user>("users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class connectionsController : ODataController
    {
        private ESEntities db = new ESEntities();

        // GET: odata/connections
        [EnableQuery]
        public IQueryable<connection> Getconnections()
        {
            return db.connections;
        }

        // GET: odata/connections(5)
        [EnableQuery]
        public SingleResult<connection> Getconnection([FromODataUri] int key)
        {
            return SingleResult.Create(db.connections.Where(connection => connection.connections_id == key));
        }

        // PUT: odata/connections(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<connection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            connection connection = db.connections.Find(key);
            if (connection == null)
            {
                return NotFound();
            }

            patch.Put(connection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!connectionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(connection);
        }

        // POST: odata/connections
        public IHttpActionResult Post(connection connection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.connections.Add(connection);
            db.SaveChanges();

            return Created(connection);
        }

        // PATCH: odata/connections(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<connection> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            connection connection = db.connections.Find(key);
            if (connection == null)
            {
                return NotFound();
            }

            patch.Patch(connection);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!connectionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(connection);
        }

        // DELETE: odata/connections(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            connection connection = db.connections.Find(key);
            if (connection == null)
            {
                return NotFound();
            }

            db.connections.Remove(connection);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/connections(5)/user
        [EnableQuery]
        public SingleResult<user> Getuser([FromODataUri] int key)
        {
            return SingleResult.Create(db.connections.Where(m => m.connections_id == key).Select(m => m.user));
        }

        // GET: odata/connections(5)/user1
        [EnableQuery]
        public SingleResult<user> Getuser1([FromODataUri] int key)
        {
            return SingleResult.Create(db.connections.Where(m => m.connections_id == key).Select(m => m.user1));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool connectionExists(int key)
        {
            return db.connections.Count(e => e.connections_id == key) > 0;
        }
    }
}
