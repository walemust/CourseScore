using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseScore.Models
{
    public class CourseCode
    {
       [Key]
        public string matricNo { get; set; }
        public string courseCode { get; set; }
        public int score { get; set; }
    }
}
