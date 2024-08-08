using HTOTools;
using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataMigration.ViewModels
{
    public class TargetTableWithColumnsViewModel : TargetTableViewModel
    {

        private readonly string _sqlcolumnswithdistinctvalues;

        private readonly string _sqlcolumnswithnonnulls;

        public TargetTableWithColumnsViewModel(Table targetTable) : base(targetTable)
        {

            Columns = new List<TargetColumnViewModel>();
            if (targetTable?.Columns != null && targetTable.Columns.Count > 0)
            {
                Columns = targetTable.Columns.Select(c => new TargetColumnViewModel(c)).ToList();

                _sqlcolumnswithdistinctvalues = getSelectStatement(Columns.Where(c => c.DistinctValues > 1).OrderBy(c => c.ColumnName).ToList());

                _sqlcolumnswithnonnulls = getSelectStatement(Columns.Where(c => c.NonNulls > 1).OrderBy(c => c.ColumnName).ToList());

            }

        }

        private string getSelectStatement(List<TargetColumnViewModel> selectColumns) {

            string sql = "SELECT ";
            var colssq = String.Join("," + Environment.NewLine + "<br>", selectColumns.Select(c => c.TSQLName));
            sql = sql + colssq + Environment.NewLine + "<br> FROM [" + TableSchema + "].[" + TableName + "]";
            return sql;

        }



        public virtual List<TargetColumnViewModel> Columns { get; set; } = new List<TargetColumnViewModel>();


        [Display(Name = "Non-Null Columns")]
        public string TSQLSelectNonNullColumns => _sqlcolumnswithnonnulls;


        [Display(Name = "SQL Distinct Columns")]
        public string TSQLSelectDistinctColumns => _sqlcolumnswithdistinctvalues;




    }
}
