using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebServiceWebAPO;

namespace SchoolWebServiceWebAPO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly SchoolContext _context;

        public UsersController(SchoolContext context)
        {
            _context = context;
        }

      
        [HttpGet]


        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] Users user)
        {
            var UserData = await (from u in _context.Users where u.UserName == user.UserName && u.PASSWORD == user.PASSWORD select new { u.ID, Name = String.Concat(u.FName," ",u.LName)}).FirstOrDefaultAsync();
            return new { UserData };
        }


        [HttpPost("signup")]
        public async Task<ActionResult<object>> Signup(Users user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.ID }, user);
        }

        // GET: api/Users/5
        [HttpPost("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.ID)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.ID }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
