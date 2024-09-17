namespace IDataMigrations.Interfaces
{
    public interface IInformationSchemaTable
    {
        string TableCatalog { get; set; }

        string TableSchema { get; set; }

        string TableName { get; set; }



    }
}
