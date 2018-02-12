using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpleRESTServer.Models;
using System.Collections;

namespace SimpleRESTServer.Controllers
{
    public class PersonController : ApiController
    {
        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        // GET: api/Person
        public ArrayList Get()
        {
            PersonPersistence pp = new PersonPersistence();
            return pp.GetPersons();
        }
        /// <summary>
        /// Get a specific person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/5
        public Person Get(int id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person person = pp.GetPerson(id);
            return person;
        }

        /// <summary>
        /// Create a person resource, returning ID
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            
            PersonPersistence pp = new PersonPersistence();
            long id;
            id = pp.SavePerson(value);
            value.ID = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("person/{0}", id));
            return response;
        }

        /// <summary>
        /// Alter an exisiting Person resource, addressed by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;

            recordExisted = pp.UpdatePerson(id, value);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;

        }

        /// <summary>
        /// Remove an addressed resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;

            recordExisted = pp.DeletePerson(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return response;
        }
    }
}
