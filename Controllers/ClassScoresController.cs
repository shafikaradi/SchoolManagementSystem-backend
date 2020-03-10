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
    public class ClassScoresController : ControllerBase
    {
        private readonly SchoolContext _context;

        public ClassScoresController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/ClassScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassScore>>> GetClassScore()
        {
            return await _context.ClassScore.ToListAsync();
        }

        // GET: api/ClassScores/5
        [HttpPost("getScore")]
        public async Task<ActionResult<object>> GetClassScoreBy([FromHeader]int studentID, [FromHeader] string mobileNo)
        {
            
            var classScore = await (from scores in _context.ClassScore
                                    join student in _context.Student on scores.student equals student.id
                                    join classRow in _context.Classes on scores.classes equals classRow.id
                                    where scores.student == studentID && student.mobileNo == mobileNo && classRow.state == 1
                                    select new {classRow.class_name, classRow.state ,scores.first_semester_score ,scores.second_semester_score, scores.final_semester_score }).ToListAsync();

            var optionalClassScore = await (from scores in _context.ClassScore
                                    join student in _context.Student on scores.student equals student.id
                                    join classRow in _context.Classes on scores.classes equals classRow.id
                                    where scores.student == studentID && student.mobileNo == mobileNo && classRow.state == 0
                                    select new { classRow.class_name, classRow.state, scores.first_semester_score, scores.second_semester_score, scores.final_semester_score }).ToListAsync();

            var Results = await (from studentRow in _context.Student
                                         where studentRow.id == studentID && studentRow.mobileNo == mobileNo
                                         select new { studentName = String.Concat(studentRow.first_name, " ", studentRow.middle_name, " ", studentRow.last_name), classScore, optionalClassScore }).FirstOrDefaultAsync();


            if (Results == null)
            {
                return new {error = "error" };
            }
            

            return Results;
        }


        [HttpPost("RegisteredClasses")]
        public async Task<ActionResult<IEnumerable<object>>> GetRegistredClasses([FromBody] Student student)
        {
            var selectedClasses = await (from classesScores in _context.ClassScore
                                         join classes in _context.Classes on classesScores.classes equals classes.id
                                         where classesScores.student == student.id select new { classesScores.student, classesScores.classes, classes.class_name }).ToListAsync();
            return  selectedClasses;
        }

        [HttpPut("UpdateFirstSemesterScore")]
        public async Task<ActionResult<int>> UpdateFirstSemesterScore([FromBody] ClassScore classScore)
        {
            var selectedClass = await (from classes in _context.ClassScore where classes.student == classScore.student && classes.classes == classScore.classes select classes).FirstOrDefaultAsync();
            selectedClass.first_semester_score = classScore.first_semester_score;

            return await _context.SaveChangesAsync();
        }

        [HttpPut("UpdateSecondSemesterScore")]
        public async Task<ActionResult<int>> UpdateSecondSemesterScore([FromBody] ClassScore classScore)
        {
            var selectedClass = await (from classes in _context.ClassScore where classes.student == classScore.student && classes.classes == classScore.classes select classes).FirstOrDefaultAsync();
            selectedClass.second_semester_score = classScore.second_semester_score;

            return await _context.SaveChangesAsync();
        }


        [HttpPut("UpdateFinalSemesterScore")]
        public async Task<ActionResult<int>> UpdateFinalSemesterScore([FromBody] ClassScore classScore)
        {
            var selectedClass = await (from classes in _context.ClassScore where classes.student == classScore.student && classes.classes == classScore.classes select classes).FirstOrDefaultAsync();
            selectedClass.final_semester_score = classScore.final_semester_score;

            return await _context.SaveChangesAsync();
        }


        // PUT: api/ClassScores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassScore(int id, ClassScore classScore)
        {
            if (id != classScore.id)
            {
                return BadRequest();
            }

            _context.Entry(classScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassScoreExists(id))
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

        // POST: api/ClassScores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ClassScore>> PostClassScore(ClassScore classScore)
        {
            _context.ClassScore.Add(classScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassScore", new { id = classScore.id }, classScore);
        }

        // DELETE: api/ClassScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClassScore>> DeleteClassScore(int id)
        {
            var classScore = await _context.ClassScore.FindAsync(id);
            if (classScore == null)
            {
                return NotFound();
            }

            _context.ClassScore.Remove(classScore);
            await _context.SaveChangesAsync();

            return classScore;
        }

        private bool ClassScoreExists(int id)
        {
            return _context.ClassScore.Any(e => e.id == id);
        }
    }
}
