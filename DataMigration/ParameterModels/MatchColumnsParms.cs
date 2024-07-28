namespace DataMigration.ParameterModels
{
    public class MatchColumnsParms
    {
        public string SrcTableSchema { get; set; } = String.Empty;

        public string SrcTableName { get; set; } = String.Empty;

        public string SrcColumnName { get; set; } = String.Empty;

        public string TgtTableSchema { get; set; } = String.Empty;

        public string TgtTableName { get; set; } = String.Empty;

        public string TgtColumnName { get; set; } = String.Empty;


        public bool AllThere => !string.IsNullOrEmpty(SrcTableSchema) && !string.IsNullOrEmpty(SrcTableName) && !string.IsNullOrEmpty(SrcColumnName)
            && !string.IsNullOrEmpty(TgtTableSchema) && !string.IsNullOrEmpty(TgtTableName) && !string.IsNullOrEmpty(TgtColumnName);


    }
}
