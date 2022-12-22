using CourseScore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseScore.Context
{
    public class CourseScoreDb : DbContext
    {
        public CourseScoreDb(DbContextOptions<CourseScoreDb> options) : base(options)
        {

        }
        public DbSet<CourseCode> CourseCodes { get; set; }
    }
}
