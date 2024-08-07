using HTOTools;
using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataMigration.ViewModels
{
    public class TargetTableWithColumnsViewModel : TargetTableViewModel
    {

        private readonly string _sqlcolumnswithdistinctvalues;


        public TargetTableWithColumnsViewModel(Table targetTable) : base(targetTable)
        {

            Columns = new List<TargetColumnViewModel>();
            if (targetTable?.Columns != null && targetTable.Columns.Count > 0)
            {
                Columns = targetTable.Columns.Select(c => new TargetColumnViewModel(c)).ToList();


                string sql = "SELECT ";
                var colssq = String.Join("," + Environment.NewLine + "<br>", Columns.Where(c => c.DistinctValues > 1).OrderBy(c => c.ColumnName).Select(c => c.TSQLName));
                sql = sql + colssq + Environment.NewLine + " FROM [" + TableSchema + "].[" + TableName + "]";
                _sqlcolumnswithdistinctvalues = sql;

            }

        }
        public virtual List<TargetColumnViewModel> Columns { get; set; } = new List<TargetColumnViewModel>();


        [Display(Name = "SQL Distinct Columns")]
        public string TSQLSelectDistinctColumns => _sqlcolumnswithdistinctvalues;




    }
}
