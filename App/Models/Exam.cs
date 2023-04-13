using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public int CrsId { get; set; }

    public virtual Course Crs { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; } = new List<StudentAnswer>();

    public virtual ICollection<StudentGrade> StudentGrades { get; } = new List<StudentGrade>();

    public virtual ICollection<Question> Qusts { get; } = new List<Question>();
}
