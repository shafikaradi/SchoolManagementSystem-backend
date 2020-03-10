using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolWebServiceWebAPO.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebServiceWebAPO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]

        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            return _context.Student.ToList();
        }

       
        [HttpPost("getStudent")]
        public ActionResult<object> GetStudent([FromHeader] int id, [FromHeader] string mobileNo)
        {


            var student =  (from students in _context.Student
                                 join grade in _context.Grade on students.grade equals grade.id
                                 where students.id == id && students.mobileNo == mobileNo
                                 select new { students.first_name, students.middle_name, grade = grade.grade_name, students.last_name, students.mobileNo, students.birthDay }).FirstOrDefault();

            if (student == null)
            {

                return new { id = 0 };
            }



            return student;
        }

        [HttpPost("getStudentByID")]
        public  ActionResult<object> GetStudent([FromBody] Student student)
        {


            var selectedUser =  (from students in _context.Student
                                      join grade in _context.Grade on students.grade equals grade.id
                                      where students.id == student.id
                                      select students).SingleOrDefault();

            if (student == null)
            {

                return new { error = 0 };
            }



            return selectedUser;
        }



        [HttpPut("updateStudentMobileNo")]
        public ActionResult<int> UpdateMobileNo([FromBody] Student student)
        {
            var selectedStudent =  (from students in _context.Student where students.id == student.id select students).FirstOrDefault();

            selectedStudent.mobileNo = student.mobileNo;
            return  _context.SaveChanges();
        }

        [HttpPut("updateStudentGrade")]
        public ActionResult<int> UpdateGrade([FromBody] Student student)
        {
            var selectedStudent = (from students in _context.Student where students.id == student.id select students).FirstOrDefault();

            selectedStudent.grade = student.grade;

            var selectedClasses = (from classes in _context.ClassScore where classes.student == student.id select classes).ToList();
            _context.ClassScore.RemoveRange(selectedClasses);

            var updatedClasses = (from classes in _context.Classes where classes.grade == student.grade select new ClassScore{ classes = classes.id, student = student.id}).ToList();

            _context.ClassScore.AddRange(updatedClasses);

            return  _context.SaveChanges();
        }

        [HttpPut("updateFullName")]
        public ActionResult<int> UpdateFullName([FromBody] Student student)
        {
            var selectedStudent =  (from students in _context.Student where students.id == student.id select students).FirstOrDefault();


            selectedStudent.first_name = student.first_name;
            selectedStudent.middle_name = student.middle_name;
            selectedStudent.last_name = student.last_name;

            return  _context.SaveChanges();
        }

        [HttpPut("updateBirthDate")]
        public  ActionResult<int> UpdateBirthDate([FromBody] Student student)
        {
            var selectedStudent =  (from students in _context.Student where students.id == student.id select students).FirstOrDefault();

            selectedStudent.birthDay = student.birthDay;

            return  _context.SaveChanges();

        }

        [HttpPost("newStudent")]
        public ActionResult<int> CreatNewStudent([FromBody] Student student)
        {
             _context.Student.AddAsync(student);

             _context.SaveChanges();
            var selectedClasses =  (from classes in _context.Classes where classes.grade == student.grade select classes).ToList();
            selectedClasses.ForEach(async x =>  await _context.ClassScore.AddAsync(new ClassScore() { student = student.id, classes = x.id}));
             _context.SaveChanges();

            return student.id;

        }

    }

}