using DataMigration.Models;
using SourceDDContext.Models;
using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class ColumnSourceViewModel
    {

        private readonly string _targetColumn;
        private readonly string _targetTable;
        private readonly string? _targetSchema;
        private readonly string _sourceColumn;
        private readonly string _sourceTable;
        private readonly string? _sourceSchema;

        public ColumnSourceViewModel(TargetDDContext.Models.ColumnSource columnSource) {

            _targetSchema = columnSource.TargetSchema;
            _targetTable = columnSource.TargetTable;
            _targetColumn = columnSource.TargetColumn;

            _sourceColumn = columnSource.SourceColumn;
            _sourceSchema = columnSource.SourceSchema;
            _sourceTable = columnSource.SourceTable;
        }


        [Display(Name = "Target Schema")]
        public string? TargetSchema { get; set; }


        [Display(Name = "Target Table")]
        public string TargetTable => _targetTable;


        [Display(Name = "Target Column")]
        public string TargetColumn => _targetColumn;


        [Display(Name = "Source Schema")]
        public string? SourceSchema => _sourceSchema;


        [Display(Name = "Source Table")]
        public string SourceTable => _sourceTable;


        [Display(Name = "Source Column")]
        public string SourceColumn => _sourceColumn;




    }

}
