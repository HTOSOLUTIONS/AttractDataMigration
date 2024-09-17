using IDataMigrations.Interfaces;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;

namespace SourceDDContext.Models;

public partial class Table : IMigrationTable
{

    public string TableCatalog { get; set; }

    public string TableSchema { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public int? RowCount { get; set; }

    public int? ColCount { get; set; }

    public string? DestinationTable { get; set; }

    public string? XStgTable { get; set; }

    public string? Description { get; set; }

    public bool? NeedsMigration { get; set; }

    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();


    [NotMapped]
    public ICollection<IDDColumn> IDDColumns { get { return Columns.Cast<IDDColumn>().ToList();  } set { } }



}
