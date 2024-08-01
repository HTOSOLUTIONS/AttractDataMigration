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


        [Display(Name = "SQL Distinct Columns")]
        public string TSQLSelectDistinctColumns
        {
            get
            {
                string sql = "SELECT ";
                if (Columns != null && Columns.Count() > 0)
                {
                    var colssq = String.Join("," + Environment.NewLine + "<br>",Columns.Where(c => c.DistinctValues > 1).OrderBy(c => c.ColumnName).Select(c => c.TSQLName));
                    sql = sql + colssq + Environment.NewLine + " FROM [" + TableSchema + "].[" + TableName + "]";
                }
                return sql;
            }
        }


    }
}
