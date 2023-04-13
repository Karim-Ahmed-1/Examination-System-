using Examination_system.Models;
using Examination_system.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Examination_system.Controllers
{
    public class StudentController : Controller
    {

        IqexamContext context = new IqexamContext();
        public IActionResult GetCourseByStuId(int id)
        {
            TempData["St_id"] = id;
            List<Course> crs = context.Courses.FromSql($"Sp_GetStuCoursesByID {id}").ToList();
            return View (crs);

        }

        public IActionResult GetExamByCrsId(int id, int st_id)
        {
            Exam Ex=context.Exams.Where(e=>e.CrsId==id).FirstOrDefault();
            TempData["E_id"]=Ex.ExamId;
            List<Question> questions = context.Questions.FromSql($"sp_get_questions_by_EId {Ex.ExamId}").ToList();
            List<Choice> test = new List<Choice>();
        
            foreach (Question question in questions)
            {
                test = context.Choices.FromSql($"sp_get_choices_by_quesId {question.QuesId}").ToList();
            }
            return View(questions);
        }
        [HttpPost]
        
        public IActionResult ExamAnswer()
        {
            List<string> answersList = new List<string>();
            foreach (var answer in Request.Form)
            {
                answersList.Add(answer.Value[0]);
            }
            var answers =String.Join(',', answersList);
            context.Database.ExecuteSql($"exec Sp_Exam_Answers {TempData["E_id"]},{TempData["St_id"]},{answers}");
            context.Database.ExecuteSql($"exec sp_Exam_Correction {TempData["St_id"]},{TempData["E_id"]}"); 
            int grade=context.StudentGrades.Where(g=>g.StdId== (int)TempData["St_id"] && g.ExamId==(int) TempData["E_id"]).Select(g=>g.Grade).FirstOrDefault();
            return View(grade);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
