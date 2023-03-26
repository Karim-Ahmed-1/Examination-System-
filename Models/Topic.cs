using System;
using System.Collections.Generic;

namespace Examination_system.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public string TopicName { get; set; } = null!;

    public int CrsId { get; set; }

    public virtual Course Crs { get; set; } = null!;
}
