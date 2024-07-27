using HTOTools;
using SourceDDContext.Models;
using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class SourceTableWithColumnsViewModel : SourceTableViewModel
    {

        public SourceTableWithColumnsViewModel(Table sourceTable) : base(sourceTable)
        {

            Columns = new List<SourceColumnViewModel>();
            if (sourceTable?.Columns != null && sourceTable.Columns.Count > 0)
            {
                Columns = sourceTable.Columns.Select(c => new SourceColumnViewModel(c)).ToList();
            }

        }


        public virtual List<SourceColumnViewModel> Columns { get; set; }




    }
}
