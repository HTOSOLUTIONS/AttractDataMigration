namespace IDataMigrations.Interfaces
{
    public interface IMigrationColumn : IDDColumn
    {

        bool? NeedsMigration { get; set; }

    }
}
