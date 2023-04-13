using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Instructor
{
    public int InsId { get; set; }

    public string InsName { get; set; } = null!;

    public int DeptId { get; set; }

    public string? InsPass { get; set; }

    public virtual Department Dept { get; set; } = null!;

    public virtual ICollection<Course> Crs { get; } = new List<Course>();
}
