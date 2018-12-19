using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureREST.Models;

namespace TemperatureREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        // really we would use a DB, but for dmeo purposes, a static list.
        public static List<Temperature> Data = new List<Temperature>
        {
            new Temperature
            {
                Id = 1,
                Time = DateTime.Now,
                Value = 36,
                Unit = TemperatureUnit.Celsius
            }
        };

        // GET: api/Temperature
        [HttpGet]
        // the return type can be just your type
        // or, ActionResult<YourType>, both will work, but the latter also allows you to
        // return error messages
        public IEnumerable<Temperature> Get()
        {
            return Data;
        }

        // GET: api/Temperature/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Temperature> Get(int id)
        {
            Temperature result = Data.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        // POST: api/Temperature
        // for inserting a new resource
        [HttpPost]
        public ActionResult Post([FromBody] Temperature value)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            Data.Add(value);
            return Ok();
        }

        // PUT: api/Temperature/5
        // replace an existing resource
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Temperature value)
        {
            var existing = Data.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound(); // if resource doesn't exist, i'll return an error
            }
            Data.Remove(existing);
            value.Id = id;
            Data.Add(value);
            return Ok(); // success = Ok()
        }

        // DELETE: api/Temperature/5
        // DELETE is for deleting resources
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existing = Data.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                return NotFound(); // if resource doesn't exist, i'll return an error
            }
            Data.Remove(existing);
            return Ok(); // success = Ok()
        }
    }
}
