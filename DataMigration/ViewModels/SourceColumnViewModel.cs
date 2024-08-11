using HTOTools;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using SourceDDContext.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataMigration.Services.HTOTools;

namespace DataMigration.ViewModels
{
    public class SourceColumnViewModel
    {
        private readonly Column _sourceColumn;

        private string? _description;

        private string? _destinationtable;

        private string? _destinationcolumn;

        private string? _xstgtable;

        private bool? _needsmigration;

        private bool? _needsfollowup;

        private string? _notes;

        private IDictionary<string, string> _routeValues;

        private ICollection<ColumnTargetViewModel> _columnTargets;

        public SourceColumnViewModel(Column sourceColumn)
        {

            _sourceColumn = sourceColumn;
            _description = sourceColumn.Description;
            _destinationtable = sourceColumn.DestinationTable;
            _destinationcolumn = sourceColumn.DestinationColumn;
            _needsmigration = sourceColumn.NeedsMigration;
            _needsfollowup = sourceColumn.NeedsFollowUp;
            _notes = sourceColumn.Notes;
            _routeValues = new Dictionary<string, string>() { { "tableschema", _sourceColumn.TableSchema }, { "tablename", _sourceColumn.TableName }, { "columnname", _sourceColumn.ColumnName } };

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
                SourceTable = new SourceTableViewModel(sourceColumn.Table);
            }

            _columnTargets = new List<ColumnTargetViewModel>();
            if (_sourceColumn?.ColumnTargets != null && _sourceColumn.ColumnTargets.Count > 0)
            {
                _columnTargets =  _sourceColumn.ColumnTargets.Select(c => new ColumnTargetViewModel(c)).ToList();
            };


        }


        [Display(Name = "Table Schema")]
        public string TableSchema { get => _sourceColumn?.TableSchema != null ? _sourceColumn.TableSchema : ""; }


        [Display(Name = "Table Name")]
        public string TableName { get => _sourceColumn?.TableName != null ? _sourceColumn.TableName : ""; }


        [Display(Name = "Column Name")]
        public string ColumnName { get => _sourceColumn?.ColumnName != null ? _sourceColumn.ColumnName : ""; }


        [Display(Name = "Data Type")]
        public string DataType { get => _sourceColumn?.DataType != null ? _sourceColumn.DataType : ""; }


        [Display(Name = "Non Null Records")]
        public int? NonNulls { get => _sourceColumn.NonNulls; }


        [Display(Name = "Distinct Values")]
        public int? DistinctValues { get => _sourceColumn.DistinctValues; }


        [Display(Name = "Destination Table")]
        public string? DestinationTable { get => _destinationtable; set => _destinationtable = value; }


        [Display(Name = "Destination Column")]
        public string? DestinationColumn { get => _destinationcolumn; set => _destinationcolumn = value; }


        public string? Description { get => _description; set => _description = value; }


        [Display(Name = "Needs Migration")]
        public bool? NeedsMigration { get => _needsmigration; set => _needsmigration = value; }


        [Display(Name = "Needs Follow Up")]
        public bool? NeedsFollowUp { get => _needsfollowup; set => _needsfollowup = value; }


        public string? Notes { get => _notes; set => _notes = value; }


        public virtual SourceTableViewModel? SourceTable { get; set; }


        [Display(Name = "Target Column(s)")]
        public ICollection<ColumnTargetViewModel> ColumnTargets => _columnTargets;


        [Display(Name = "Target Column(s)")]
        public string ColumnTargetsDisp
        {
            get
            {
                var disp = "";
                if (ColumnTargets != null && ColumnTargets.Count > 0)
                {
                    var list = ColumnTargets.Select(c => new { target = c.TargetTable + '.' + c.TargetColumn }).ToList();
                    disp = String.Join(",", list.Select(c => c.target));
                }


                return disp;

            }

        }


        public IDictionary<string,string> RouteValues { get => _routeValues; }


        public HTORowCtrlList RowCtrls { get; set; }


        public string TSQLName { get {
                return "[" + TableName + "].[" + ColumnName + "]";
            } 
        }


    }
}
