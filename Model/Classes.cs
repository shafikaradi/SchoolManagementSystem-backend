using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebServiceWebAPO.Model
{
    public class Classes
    {
        [Key]
        public int id { get; set; }
        public string class_name { get; set; }
        public int grade { get; set; }
        public int state { get; set; }
    }
}
