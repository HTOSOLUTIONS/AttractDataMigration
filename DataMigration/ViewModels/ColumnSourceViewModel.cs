using DataMigration.Models;
using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class ColumnSourceViewModel
    {

        private readonly string _sourceTable;
        private readonly string _sourceColumn;

        public ColumnSourceViewModel(TargetDDContext.Models.ColumnSource columnSource) {

            _sourceTable = columnSource.SourceTable;
            _sourceColumn = columnSource.SourceColumn;
        }


        [Display(Name = "Source Table")]
        public string SourceTable => _sourceTable;


        [Display(Name = "Source Column")]
        public string SourceColumn => _sourceColumn;



    }

}
