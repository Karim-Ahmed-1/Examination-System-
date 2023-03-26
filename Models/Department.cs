using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Department
{
    public int DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public virtual ICollection<Instructor> Instructors { get; } = new List<Instructor>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
