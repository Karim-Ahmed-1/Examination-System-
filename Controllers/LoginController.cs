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

                    List<Student> instructors = context.Students.FromSql($"Sp_StudentValidNamePassword {MV.UserName},{MV.Password} ").ToList();
                    if (instructors.Count() > 0 && instructors[0].StdName == MV.UserName && instructors[0].StdPass == MV.Password)
                    {
                        ViewData["valid"] = "true";
                        //  return RedirectToAction("Index", "Instructor");
                    }
                    else
                    {
                        ViewData["valid"] = "false";
                        ModelState.AddModelError("Type", "UnValid UserName and Password ");
                    }

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
                    {
                        ViewData["valid"] = "false";
                        ModelState.AddModelError("Type", "UnValid UserName and Password  ");

                    }


                }
			}


			return View();
        }
    }
}
