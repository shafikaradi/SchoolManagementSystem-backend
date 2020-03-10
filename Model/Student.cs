using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebServiceWebAPO.Model
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string mobileNo { get; set; }
        [Column(TypeName = "Date")]
        public DateTime birthDay { get; set; }

        //public Grade grade { get; set; }
        //[ForeignKey("Grade")]
        public int grade { get; set; }
       

    }
}
