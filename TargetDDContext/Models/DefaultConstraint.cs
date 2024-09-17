using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class DefaultConstraint
{
    public string ConstraintName { get; set; } = null!;

    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string? Definition { get; set; }

    public virtual Column Column { get; set; } = null!;
}
