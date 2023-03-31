using System.ComponentModel.DataAnnotations;
using Examination_system.Models;
using Microsoft.AspNetCore.Mvc;


namespace Examination_system.ModelViews
{
    public class GenertaeExamModel
    {
        public int InstId { get; set; }
        public int CrsId { get; set; }

        [Required(ErrorMessage = "MCQ_number Is Required")]
        [Range(maximum: int.MaxValue, minimum: 1, ErrorMessage = "MC Questions number Must be greater than 1")]
        public int MCQ_number { get; set; }

        [Range(minimum: 1, maximum:int.MaxValue ,ErrorMessage= "TF Questions number Must greater than 1 ")]
        [Required(ErrorMessage = "TF_number Is Required")]
        public int TF_number { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
