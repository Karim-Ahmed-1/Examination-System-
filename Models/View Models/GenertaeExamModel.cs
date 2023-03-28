using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace Examination_system.Models.View_Models
{
    public class GenertaeExamModel
    {
        public int CrsId { get; set; }

        [Required(ErrorMessage = "MCQ_number Is Required")]
        [Range(maximum: 15, minimum: 1, ErrorMessage = "MC Questions number Must be between 1 and 15")]
        public int MCQ_number { get; set; }

        [Range(maximum: 15, minimum: 1, ErrorMessage = "TF Questions number Must be between 1 and 15")]
        [Required(ErrorMessage = "TF_number Is Required")]
        public int TF_number { get; set; }


        public List<Course>? Courses{ get; set; }
    }
}
