using Examination_system.Models;
using Examination_system.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Examination_system.Controllers
{

    public class InstructorController : Controller
    {
        IqexamContext DB =new IqexamContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New(int id)
        {
            List<Course> crs = DB.Courses.FromSql($"Sp_GetInsCoursesByID {id}").ToList();
            return View("GenerateExam", new GenertaeExamModel() { Courses = crs ,InstId=id});
        }

        
        public IActionResult GenerateExam(GenertaeExamModel newExam)
        {
            List<Course> crs = DB.Courses.FromSql($"Sp_GetInsCoursesByID {newExam.InstId}").ToList();
            newExam.Courses = crs;


           // List<Course> crs = context.Courses.FromSql($"Sp_GetInsCoursesByID {id}").ToList(); 


            if (ModelState.IsValid == true)
            {

                using (var context = new IqexamContext())
                {
                    context.Database.ExecuteSqlInterpolated($"EXEC Sp_Exam_Generation {newExam.CrsId}, {newExam.MCQ_number}, {newExam.TF_number}");
                    context.SaveChanges();
                }

                return RedirectToAction("New", newExam.InstId);// Index
            }
            return View("GenerateExam", newExam); 
        }
    }
}
