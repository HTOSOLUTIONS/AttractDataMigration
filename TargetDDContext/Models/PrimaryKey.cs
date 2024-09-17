using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class PrimaryKey
{
    public string ConstraintName { get; set; } = null!;

    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public bool? IsIdentity { get; set; }

    public virtual Table Table { get; set; } = null!;
}
