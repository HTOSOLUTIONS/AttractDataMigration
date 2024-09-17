using IDataMigrations.Interfaces;

namespace SourceDDContext.Models;

public partial class ColumnTarget : IColumnMapping
{
    public string? SourceSchema { get; set; }

    public string SourceTable { get; set; } = null!;

    public string SourceColumn { get; set; } = null!;

    public string? TargetSchema { get; set; }

    public string TargetTable { get; set; } = null!;

    public string TargetColumn { get; set; } = null!;

    public virtual Column? Column { get; set; }
}
