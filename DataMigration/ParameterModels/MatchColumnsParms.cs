using IDataMigrations.Interfaces;

namespace DataMigration.ParameterModels
{
    public class MatchColumnsParms : IColumnMapping
    {
        private string? _SourceSchema;
        private string? _TargetSchema;
        private string _SourceTable;
        private string _TargetTable;
        private string _SourceColumn;
        private string _TargetColumn;

        public MatchColumnsParms() { 
            _SourceSchema = null;
            _TargetSchema = null;
            _SourceColumn = string.Empty; _TargetColumn = string.Empty;
            _SourceTable = string.Empty; _TargetTable = string.Empty;
        }

        public MatchColumnsParms(IColumnMapping columnMapping)
        {
            _SourceSchema = columnMapping.SourceSchema;
            _TargetSchema = columnMapping.TargetSchema;
            _SourceTable = columnMapping.SourceTable;
            _TargetTable = columnMapping.TargetTable;
            _SourceColumn = columnMapping.SourceColumn;
            _TargetColumn = columnMapping.TargetColumn;


        }

        //SrcTableSchema
        public string? SourceSchema { get => _SourceSchema; set => _SourceSchema = value; } 

        //SrcTableName
        public string SourceTable { get => _SourceTable; set => _SourceTable = value; }

        //SrcColumnName
        public string SourceColumn { get => _SourceColumn ; set => _SourceColumn = value; }

        //TgtTableSchema
        public string? TargetSchema { get => _TargetSchema; set => _TargetSchema = value; }

        //TgtTableName
        public string TargetTable { get => _TargetTable; set => _TargetTable = value; }

        //TgtColumnName
        public string TargetColumn { get => _TargetColumn ; set => _TargetColumn = value; }


        public bool AllThere => !string.IsNullOrEmpty(SourceSchema) && !string.IsNullOrEmpty(SourceTable) && !string.IsNullOrEmpty(SourceColumn)
            && !string.IsNullOrEmpty(TargetSchema) && !string.IsNullOrEmpty(TargetTable) && !string.IsNullOrEmpty(TargetColumn);


    }
}
