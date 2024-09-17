using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IDataMigrations.Interfaces;


namespace TargetDDContext.Models;

public partial class Column : IMigrationColumn
{
    public string TableCatalog { get; set; } = null!;

    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public int? OrdinalPosition { get; set; }

    public string? ColumnDefault { get; set; }

    public string? IsNullable { get; set; }

    public string? DataType { get; set; }

    public int? CharacterMaximumLength { get; set; }

    public int? CharacterOctetLength { get; set; }

    public byte? NumericPrecision { get; set; }

    public short? NumericPrecisionRadix { get; set; }

    public int? NumericScale { get; set; }

    public short? DatetimePrecision { get; set; }

    public string? CharacterSetCatalog { get; set; }

    public string? CharacterSetSchema { get; set; }

    public string? CharacterSetName { get; set; }

    public string? CollationCatalog { get; set; }

    public string? CollationSchema { get; set; }

    public string? CollationName { get; set; }

    public string? DomainCatalog { get; set; }

    public string? DomainSchema { get; set; }

    public string? DomainName { get; set; }

    public string? SourceTable { get; set; }

    public string? SourceColumn { get; set; }

    public int? NonNulls { get; set; }

    public int? DistinctValues { get; set; }

    public string? Description { get; set; }

    public bool? NeedsMigration { get; set; }

    public string? UseType { get; set; }

    public string? ProductionName { get; set; }

    public virtual ICollection<ColumnSource> ColumnSources { get; set; } = new List<ColumnSource>();


    public virtual ICollection<DefaultConstraint> DefaultConstraints { get; set; } = new List<DefaultConstraint>();

    public virtual ICollection<ForeignKey> ForeignKeys { get; set; } = new List<ForeignKey>();

    public virtual ICollection<ForeignKey> PrimaryKeys { get; set; } = new List<ForeignKey>();

    public virtual ICollection<PushColumn> PushColumns { get; set; } = new List<PushColumn>();

    public virtual Table Table { get; set; } = null!;


    [NotMapped]
    public IDDTable IDDTable { get { return Table as IMigrationTable; } set { } }


}
