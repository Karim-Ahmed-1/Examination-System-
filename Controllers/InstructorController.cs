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

        public IActionResult New()
        {
            return View("GenerateExam", new GenertaeExamModel() { Courses = DB.Courses.ToList() });
        }

        
        public IActionResult GenerateExam(GenertaeExamModel newExam)
        {
            newExam.Courses = DB.Courses.ToList();


           // List<Course> crs = context.Courses.FromSql($"Sp_GetInsCoursesByID {id}").ToList(); 


            if (ModelState.IsValid == true)
            {

                using (var context = new IqexamContext())
                {
                    context.Database.ExecuteSqlInterpolated($"EXEC Sp_Exam_Generation {newExam.CrsId}, {newExam.MCQ_number}, {newExam.TF_number}");
                    context.SaveChanges();
                }

                return RedirectToAction("Index");// Index
            }
            return View("GenerateExam", newExam); 
        }
    }
}
