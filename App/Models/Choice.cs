using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Choice
{
    public int ChoiceId { get; set; }

    public string ChoiceBody { get; set; } = null!;

    public int QustId { get; set; }

    public virtual Question Qust { get; set; } = null!;
}
