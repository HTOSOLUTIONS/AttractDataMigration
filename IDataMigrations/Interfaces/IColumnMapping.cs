namespace IDataMigrations.Interfaces
{
    public interface IColumnMapping
    {

         string? SourceSchema { get; set; }

         string SourceTable { get; set; }

         string SourceColumn { get; set; }

         string? TargetSchema { get; set; }

         string TargetTable { get; set; }

         string TargetColumn { get; set; }


    }
}
