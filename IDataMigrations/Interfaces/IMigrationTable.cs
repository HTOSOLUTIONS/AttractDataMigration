namespace IDataMigrations.Interfaces
{
    public interface IMigrationTable : IDDTable
    {

        bool? NeedsMigration { get; set; }



    }
}
