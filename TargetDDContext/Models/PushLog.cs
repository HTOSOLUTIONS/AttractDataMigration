using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class PushLog
{
    public int PushLogId { get; set; }

    public DateTime? PushDt { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<PushColumn> PushColumns { get; set; } = new List<PushColumn>();
}
