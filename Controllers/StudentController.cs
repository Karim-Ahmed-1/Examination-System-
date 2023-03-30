using Examination_system.Models;
using Examination_system.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Examination_system.Controllers
{
    public class StudentController : Controller
    {

        IqexamContext context = new IqexamContext();
        

        public IActionResult GetCourseByStuId(int id)
        {

            List<Course> crs = context.Courses.FromSql($"Sp_GetStuCoursesByID {id}").ToList();
            return View (crs);
        }

        public IActionResult GetExamByCrsId(int id)
        {

            Exam Ex=context.Exams.Where(e=>e.CrsId==id).FirstOrDefault();
            List<Question> questions = context.Questions.FromSql($"sp_get_questions_by_EId {Ex.ExamId}").ToList();
            List<Choice> test = new List<Choice>();
         

            ViewExamModel viewExamModel = new ViewExamModel();
            foreach (Question question in questions)
            {
                test = context.Choices.FromSql($"sp_get_choices_by_quesId {question.QuesId}").ToList();
            }
           


            return View(questions);
        }
           public IActionResult Index()
        {
            return View();
        }
    }
}
