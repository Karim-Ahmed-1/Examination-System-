using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Student
{
    public int StdId { get; set; }

    public string StdName { get; set; } = null!;

    public int StdAge { get; set; }

    public string? StdPass { get; set; }

    public int DeptId { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual ICollection<StudentAnswer> StudentAnswers { get; } = new List<StudentAnswer>();

    public virtual ICollection<StudentGrade> StudentGrades { get; } = new List<StudentGrade>();

    public virtual ICollection<Course> Crs { get; } = new List<Course>();
}
