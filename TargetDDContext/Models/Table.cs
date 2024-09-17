using IDataMigrations.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace TargetDDContext.Models;

public partial class Table : IMigrationTable
{
    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public int? RowCount { get; set; }

    public int? ColCount { get; set; }

    public string? SourceTable { get; set; }

    public string? Description { get; set; }

    public bool? NeedsMigration { get; set; }

    public string TableCatalog { get; set; } = null!;

    public string? UseDomain { get; set; }

    public string? UseType { get; set; }

    public DateTime? LastPushDt { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();


    public virtual ICollection<FamilyPath> ParentPaths { get; set; } = new List<FamilyPath>();

    public virtual ICollection<FamilyPath> ChildPaths { get; set; } = new List<FamilyPath>();
  

    public virtual ICollection<PrimaryKey> PrimaryKeys { get; set; } = new List<PrimaryKey>();


    [NotMapped]
    public ICollection<IDDColumn> IDDColumns { get { return Columns.Cast<IDDColumn>().ToList(); } set { } }



}
