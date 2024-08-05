using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class ColumnTargetViewModel
    {

        private readonly string _targetColumn;
        private readonly string _targetTable;
        private readonly string? _targetSchema;
        private readonly string _sourceColumn;
        private readonly string _sourceTable;
        private readonly string? _sourceSchema;

        public ColumnTargetViewModel(SourceDDContext.Models.ColumnTarget columnTarget)
        {
            _targetSchema = columnTarget.TargetSchema;
            _targetTable = columnTarget.TargetTable;
            _targetColumn = columnTarget.TargetColumn;

            _sourceColumn = columnTarget.SourceColumn;
            _sourceSchema = columnTarget.SourceSchema;
            _sourceTable = columnTarget.SourceTable;
        }


        public string? TargetSchema { get; set; }


        [Display(Name = "Target Table")]
        public string TargetTable => _targetTable;


        [Display(Name = "Target Column")]
        public string TargetColumn => _targetColumn;

        public string? SourceSchema => _sourceSchema;

        public string SourceTable => _sourceTable;

        public string SourceColumn => _sourceColumn;


    }
}
