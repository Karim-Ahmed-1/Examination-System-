using Examination_system.Models;
using Examination_system.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            Exam Ex=DB.Exams.Where(e=>e.CrsId==id).FirstOrDefault();

            List<Question> questions = DB.Questions.FromSql($"sp_get_questions_by_EId {Ex.ExamId}").ToList();

            List<Choice> test = new List<Choice>();
            foreach (Question question in questions)
            {
                test = DB.Choices.FromSql($"sp_get_choices_by_quesId {question.QuesId}").ToList();
            }
            return Json(questions);
        }
           public IActionResult Index()
        {
            return View();
        }
    }
}
