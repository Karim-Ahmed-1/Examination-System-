using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Question
{
    public int QuesId { get; set; }

    public string QustBody { get; set; } = null!;

    public int QustMarks { get; set; }

    public int QustModelAnsId { get; set; }

    public int CrsId { get; set; }

    public int QustType { get; set; }

    public virtual ICollection<Choice> Choices { get; } = new List<Choice>();

    public virtual ICollection<StudentAnswer> StudentAnswers { get; } = new List<StudentAnswer>();

    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();
}
