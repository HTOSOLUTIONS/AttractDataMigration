using HTOTools;
using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;


namespace DataMigration.ViewModels
{
    public class TargetColumnViewModel
    {
        private readonly Column _targetColumn;

        private string? _description;

        private string? _sourcetable;

        private string? _sourcecolumn;

        private string? _xstgtable;

        private bool? _needsmigration;

        private IDictionary<string, string> _routeValues;


        public TargetColumnViewModel(Column sourceColumn)
        {

            _targetColumn = sourceColumn;
            _description = sourceColumn.Description;
            _sourcetable = sourceColumn.SourceTable;
            _sourcecolumn = sourceColumn.SourceColumn;
            _needsmigration = sourceColumn.NeedsMigration;
            _routeValues = new Dictionary<string, string>() { { "tableschema", _targetColumn.TableSchema }, { "tablename", _targetColumn.TableName }, { "columnname", _targetColumn.ColumnName } };

            RowCtrls = new HTORowCtrlList()
            {
                DefaultController = "SourceColumns",
                RouteValues = _routeValues,
                Controls = new List<HTORowControl>()
                {
                }
            };


            if (sourceColumn.Table != null)
            {
                TargetTable = new TargetTableViewModel(sourceColumn.Table);
            }


        }


        [Display(Name = "Table Schema")]
        public string TableSchema { get => _targetColumn?.TableSchema != null ? _targetColumn.TableSchema : ""; }


        [Display(Name = "Table Name")]
        public string TableName { get => _targetColumn?.TableName != null ? _targetColumn.TableName : ""; }


        [Display(Name = "Column Name")]
        public string ColumnName { get => _targetColumn?.ColumnName != null ? _targetColumn.ColumnName : ""; }


        [Display(Name = "Data Type")]
        public string DataType { get => _targetColumn?.DataType != null ? _targetColumn.DataType : ""; }


        [Display(Name = "Non Null Records")]
        public int? NonNulls { get => _targetColumn.NonNulls; }


        [Display(Name = "Distinct Values")]
        public int? DistinctValues { get => _targetColumn.DistinctValues; }


        [Display(Name = "Source Table")]
        public string? SourceTable { get => _sourcetable; set => _sourcetable = value; }


        [Display(Name = "Source Column")]
        public string? SourceColumn { get => _sourcecolumn; set => _sourcecolumn = value; }


        public string? Description { get => _description; set => _description = value; }


        [Display(Name = "Needs Migration")]
        public bool? NeedsMigration { get => _needsmigration; set => _needsmigration = value; }


        public virtual TargetTableViewModel TargetTable { get; set; }


        public IDictionary<string, string> RouteValues { get => _routeValues; }


        [Display(Name = "Source Column(s)")]
        public ICollection<ColumnSourceViewModel> ColumnSources
        {

            get
            {
                if (_targetColumn?.ColumnSources != null && _targetColumn?.ColumnSources.Count > 0)
                {
                    return _targetColumn.ColumnSources.Select(c => new ColumnSourceViewModel(c)).ToList();
                }
                else
                {
                    return new List<ColumnSourceViewModel>();
                }
            }

        }


        [Display(Name = "Source Column(s)")]
        public string ColumnSourcesDisp
        {
            get
            {
                var disp = "";
                if (ColumnSources != null && ColumnSources.Count > 0)
                {
                    var list = ColumnSources.Select(c => new { target = c.SourceTable + '.' + c.SourceColumn }).ToList();
                    disp = String.Join(",", list.Select(c => c.target));
                }


                return disp;

            }

        }




        public HTORowCtrlList RowCtrls { get; set; }

        public string TSQLName
        {
            get
            {
                return "[" + TableName + "].[" + ColumnName + "]";
            }
        }



    }


}
