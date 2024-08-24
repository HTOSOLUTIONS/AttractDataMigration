using IDataMigrations.Interfaces;

namespace DataMigration.ViewModels
{
    public class SQLStatementViewModel
    {
        public string? SQLStatement { get; set; }

        public IDDTable? Table { get; set; }

    }
}
