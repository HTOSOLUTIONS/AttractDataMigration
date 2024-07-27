using HTOTools;
using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class TargetTableViewModel
    {
        protected readonly Table _targetTable;

        protected string? _description;

        protected string? _sourcetable;

        protected bool? _needsmigration;

        protected IDictionary<string, string> _routeValues;


        public TargetTableViewModel(Table targetTable)
        {

            _targetTable = targetTable;
            _description = targetTable.Description;
            _sourcetable = targetTable.SourceTable;
            _needsmigration = targetTable.NeedsMigration;
            _routeValues = new Dictionary<string, string>() { { "tableschema", _targetTable.TableSchema }, { "tablename", _targetTable.TableName } };


            RowCtrls = new HTORowCtrlList()
            {
                DefaultController = "SourceTables",
                RouteValues = _routeValues,
                Controls = new List<HTORowControl>()
                {
                }
            };


        }

        [Display(Name = "Table Schema")]
        public string TableSchema { get => _targetTable?.TableSchema != null ? _targetTable.TableSchema : ""; }


        [Display(Name = "Table Name")]
        public string TableName { get => _targetTable?.TableName != null ? _targetTable.TableName : ""; }


        [Display(Name = "Number of Records")]
        public int? RowCount { get => _targetTable.RowCount; }


        [Display(Name = "Number of Columns")]
        public int? ColCount { get => _targetTable.ColCount; }


        [Display(Name = "Source Table")]
        public string? SourceTable { get => _sourcetable; set => _sourcetable = value; }

        public string? Description { get => _description; set => _description = value; }


        [Display(Name = "Needs Migration")]
        public bool? NeedsMigration { get => _needsmigration; set => _needsmigration = value; }


        public IDictionary<string, string> RouteValues { get => _routeValues; }



        public HTORowCtrlList RowCtrls { get; set; }




    }
}
