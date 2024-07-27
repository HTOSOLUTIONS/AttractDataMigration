using HTOTools;
using SourceDDContext.Models;
using System.ComponentModel.DataAnnotations;

namespace DataMigration.ViewModels
{
    public class SourceTableViewModel
    {

        protected readonly Table _sourceTable;

        protected string? _description;

        protected string? _destinationtable;

        protected string? _xstgtable;

        protected bool? _needsmigration;

        protected IDictionary<string, string> _routeValues;


        public SourceTableViewModel(Table sourceTable)
        {

            _sourceTable = sourceTable;
            _description = sourceTable.Description;
            _destinationtable = sourceTable.DestinationTable;
            _xstgtable = sourceTable.XStgTable;
            _needsmigration = sourceTable.NeedsMigration;
            _routeValues = new Dictionary<string, string>() { { "tableschema", _sourceTable.TableSchema }, { "tablename", _sourceTable.TableName } };


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
        public string TableSchema { get => _sourceTable?.TableSchema != null ? _sourceTable.TableSchema : ""; }


        [Display(Name = "Table Name")]
        public string TableName { get => _sourceTable?.TableName != null ? _sourceTable.TableName : ""; }


        [Display(Name = "Number of Records")]
        public int? RowCount { get => _sourceTable.RowCount; }


        [Display(Name = "Number of Columns")]
        public int? ColCount { get => _sourceTable.ColCount; }


        [Display(Name = "Destination Table")]
        public string? DestinationTable { get => _destinationtable; set => _destinationtable = value; }


        [Display(Name = "Staging Table")]
        public string? XStgTable { get => _xstgtable; set => _xstgtable = value; }


        public string? Description { get => _description; set => _description = value; }


        [Display(Name = "Needs Migration")]
        public bool? NeedsMigration { get => _needsmigration; set => _needsmigration = value; }


        public IDictionary<string, string> RouteValues { get => _routeValues; }



        public HTORowCtrlList RowCtrls { get; set; }


    }
}
