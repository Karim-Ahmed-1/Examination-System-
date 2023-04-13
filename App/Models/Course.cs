using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Course
{
    public int CrsId { get; set; }

    public string CrsName { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();

    public virtual ICollection<Topic> Topics { get; } = new List<Topic>();

    public virtual ICollection<Instructor> Ins { get; } = new List<Instructor>();

    public virtual ICollection<Student> Stds { get; } = new List<Student>();
}
