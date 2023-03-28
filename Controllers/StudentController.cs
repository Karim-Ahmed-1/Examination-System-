using Examination_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Examination_system.Controllers
{
    public class StudentController : Controller
    {

        IqexamContext DB = new IqexamContext();

        //display courses by student id
        

        public IActionResult GetCourseByStuId(int id)
        {

            List<Course> crs = DB.Courses.FromSql($"Sp_GetStuCoursesByID {id}").ToList();
            return View (crs);
        }

        public IActionResult GetExamByCrsId(int id)
        {
            var questions = DB.Questions.FromSql($"sp_get_exam_by_courseId {id}").ToList();

            return Json(questions);
        }
           public IActionResult Index()
        {
            return View();
        }
    }
}
