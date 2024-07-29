using HTOTools;
using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataMigration.ViewModels
{
    public class TargetTableWithColumnsViewModel : TargetTableViewModel
    {

        public TargetTableWithColumnsViewModel(Table targetTable) : base(targetTable)
        {

            Columns = new List<TargetColumnViewModel>();
            if (targetTable?.Columns != null && targetTable.Columns.Count > 0)
            {
                Columns = targetTable.Columns.Select(c => new TargetColumnViewModel(c)).ToList();
            }

        }
        public virtual List<TargetColumnViewModel> Columns { get; set; } = new List<TargetColumnViewModel>();


    }
}
