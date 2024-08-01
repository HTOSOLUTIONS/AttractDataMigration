using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class ColumnTargetViewModel
    {

        private readonly string _targetColumn;
        private readonly string _targetTable;

        public ColumnTargetViewModel(SourceDDContext.Models.ColumnTarget columnTarget)
        {
            _targetTable = columnTarget.TargetTable;
            _targetColumn = columnTarget.TargetColumn;
        }


        [Display(Name = "Target Table")]
        public string TargetTable => _targetTable;


        [Display(Name = "Target Column")]
        public string TargetColumn => _targetColumn;



    }
}
