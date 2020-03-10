using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebServiceWebAPO;
using SchoolWebServiceWebAPO.Model;

namespace SchoolWebServiceWebAPO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly SchoolContext _context;

        public GradeController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Grade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrade()
        {
            return await _context.Grade.ToListAsync();
        }

        // GET: api/Grade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
          

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }

        // PUT: api/Grade/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.id)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // POST: api/Grade
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            _context.Grade.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrade", new { id = grade.id }, grade);
        }

        // DELETE: api/Grade/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Grade>> DeleteGrade(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grade.Remove(grade);
            await _context.SaveChangesAsync();

            return grade;
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.id == id);
        }
    }
}
