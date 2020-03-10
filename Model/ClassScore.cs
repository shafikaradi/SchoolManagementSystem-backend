using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebServiceWebAPO.Model
{
    public class ClassScore
    {
        [Key]
        public int id { get; set; }
        public int student { get; set; }
        public int classes { get; set; }
        public int first_semester_score { get; set; }
        public int second_semester_score { get; set; }
        public int final_semester_score { get; set; }
    }
}
