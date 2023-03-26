using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class StudentAnswer
{
    public int AnsId { get; set; }

    public int ExamId { get; set; }

    public int StdId { get; set; }

    public int AnsBody { get; set; }

    public int QustId { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Question Qust { get; set; } = null!;

    public virtual Student Std { get; set; } = null!;
}
