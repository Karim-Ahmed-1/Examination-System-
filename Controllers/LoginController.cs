using Examination_system.Models;
using Examination_system.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Examination_system.Controllers
{
    public class LoginController : Controller
    {
        IqexamContext context = new IqexamContext();

        public IActionResult Index()
        {

            return View();
        }

            [HttpPost]
        public IActionResult Index(LoginMV MV )
        {

			if (ModelState.IsValid == true)
			{
                if (MV.Type )
                {

                    List<Student> students = context.Students.FromSql($"Sp_StudentValidNamePassword {MV.UserName},{MV.Password} ").ToList();
                    if (students.Count() > 0 && students[0].StdName == MV.UserName && students[0].StdPass == MV.Password)
                    {
                        ViewData["valid"] = "true";
                         return RedirectToAction("GetCourseByStuId", "Student",new { id = students[0].StdId });

                    }
                    else
                        ViewData["valid"] = "false";
                }
                else
                {


                  List<Instructor> instructors = context.Instructors.FromSql($"Sp_InstructorValidNamePassword {MV.UserName},{MV.Password} ").ToList();
                    if (instructors.Count() > 0 && instructors[0].InsName == MV.UserName && instructors[0].InsPass ==MV.Password)
                    {
                        ViewData["valid"] = "true";
                        return RedirectToAction("Index","Instructor");
                    }
                    else
                        ViewData["valid"] = "false";
                }
			}


			return View();
        }
    }
}
