namespace IDataMigrations.Interfaces
{
    public interface IDDColumn : IInformationSchemaColumn
    {

        int? NonNulls { get; set; }

        int? DistinctValues { get; set; }

        string? Description { get; set; }

        IDDTable IDDTable { get; set; }

    }
}
