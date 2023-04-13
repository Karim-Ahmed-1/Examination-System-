using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class StudentGrade
{
    public int GradeId { get; set; }

    public int ExamId { get; set; }

    public int StdId { get; set; }

    public int Grade { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Std { get; set; } = null!;
}
