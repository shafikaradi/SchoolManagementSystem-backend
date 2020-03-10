using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolWebServiceWebAPO.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        private readonly SchoolContext schoolContext;
        public HomeController(SchoolContext sc)
        {
            schoolContext = sc;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<Users>> Get()
        {
            return schoolContext.Users.Find(1);
            // schoolContext.Users.Add(new Users
            //{

            //    FName ="Chris",
            //    LName = "Evens",
            //    UserName = "CEvens",
            //    PASSWORD = "aaaaaa",
            //    MobileNo ="+1324859303"

            //});

            //return await schoolContext.SaveChangesAsync();


          
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return schoolContext.Users.Find(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
