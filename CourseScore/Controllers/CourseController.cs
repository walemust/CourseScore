using CourseScore.Context;
using CourseScore.Models;
using CourseScore.Views;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseScore.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseScoreDb _courseContext;
        public CourseController(CourseScoreDb courseDbContext)
        {
            _courseContext = courseDbContext;
        }
        [HttpPost("createcourse")]
        public async Task<IActionResult> CreateCourse(CourseViewModel courseView)
        {
            try
            {

                //if(courseView == null)
                //{
                //    return BadRequest();
                //}
                //if user already exist in the database
                var user = new CourseCode
                {
                    matricNo = courseView.matricNo,
                    courseCode = courseView.courseCode,
                    score = courseView.score,

                };
                var mae = await _courseContext.CourseCodes.Where(x => x.matricNo == courseView.matricNo).AnyAsync();
                if(!mae)
                {
                    await _courseContext.CourseCodes.AddAsync(user);
                 await _courseContext.SaveChangesAsync();
                }
                int res = await _courseContext.SaveChangesAsync();


                if (res > 0)
                {
                    return Ok(new { 
                        message ="It was successful",
                        code = 0});
                }
                else
                {
                    return Ok(new
                    {
                        message = "an error occurred",
                        code = -1
                    });
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("getallcourses")]
        public async Task<IActionResult> AllCourses()
        {
            try
            {
                var user = await _courseContext.CourseCodes.Select(x => new CourseViewModel
                {
                    
                    matricNo = x.matricNo,
                    courseCode = x.courseCode,
                    score = x.score,
                }).ToListAsync();
                
                return Ok (user);
            }
            catch (Exception)
            {
                return Ok("an error occur");
            }
        }

        [HttpGet("getcoursebyid")]
        public async Task<CourseViewModel> GetUser(string id)
        {
            try
            {
                var user = await _courseContext.CourseCodes.Where(user => user.matricNo == id).Select(user => new CourseViewModel
                {
                    matricNo = user.matricNo,
                    courseCode = user.courseCode,
                    score = user.score
                    
                }).FirstOrDefaultAsync();
                if (user == null) throw new Exception("No user found");
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("updatecourse")]
        public async Task<CourseViewModel> UpdateUser(CourseViewModel model)
        {
            try
            {
                if (model == null)
                    throw new Exception("matricno is not provided.");
                //get the course
                var user = await _courseContext.CourseCodes.FindAsync(model.matricNo);
                //update the user
                if (user == null)
                    throw new Exception("This  cannot be retrieved at the moment.");
                user.matricNo = string.IsNullOrEmpty(model.matricNo) ? user.matricNo : model.matricNo;
                user.courseCode = string.IsNullOrEmpty(model.courseCode) ? user.courseCode : model.courseCode;
                user.score = string.IsNullOrEmpty(model.score.ToString()) ? user.score : model.score;
                _courseContext.Entry(user).State = EntityState.Modified;
                await _courseContext.SaveChangesAsync();
                return new CourseViewModel
                {
                    matricNo = user.matricNo,
                    courseCode = user.courseCode,
                    score = user.score,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("removecourse")]
        public async Task<string> DeleteCourse(int id)
        {
            try
            {
                var courseToBeDeleted = await _courseContext.CourseCodes.FindAsync(id);
                if (courseToBeDeleted == null)
                    throw new Exception("Cannot delete a user that doesn't exist");
                _courseContext.CourseCodes.Remove(courseToBeDeleted);
                await _courseContext.SaveChangesAsync();
                return "User deleted successfully";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
