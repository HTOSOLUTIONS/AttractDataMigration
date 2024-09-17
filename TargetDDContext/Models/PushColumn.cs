using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class PushColumn
{
    public int PushLogId { get; set; }

    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string Note { get; set; } = null!;

    public virtual Column Column { get; set; } = null!;

    public virtual PushLog PushLog { get; set; } = null!;
}
