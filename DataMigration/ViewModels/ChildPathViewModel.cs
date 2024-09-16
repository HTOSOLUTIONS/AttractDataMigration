using TargetDDContext.Models;

namespace DataMigration.ViewModels
{
    public class ChildPathViewModel
    {
        private readonly string _fktablowner;
        private readonly string _fktablename;
        private readonly string _childpath;
        private readonly FamilyPath _oChildPath;

        //, string fktablowner, string fktablename, string childpath

        public ChildPathViewModel(FamilyPath familypath)
        {
            _oChildPath = familypath;
            //_fktablowner = fktablowner;
            //_fktablename = fktablename;
            //_childpath = childpath;

        }

        public string FktableOwner => _oChildPath.FktableOwner;

        public string FktableName => _oChildPath.FktableName;

        public string Fullpath => _oChildPath.Fullpath;

        public string? ChildUseType => _oChildPath?.ChildTable != null ? _oChildPath.ChildTable.UseType : null;

        public int? Records => _oChildPath?.ChildTable != null ? _oChildPath.ChildTable.RowCount : null;

        public int? Columns => _oChildPath?.ChildTable != null ? _oChildPath.ChildTable.ColCount : null;

        public DateTime? LastPushDt => _oChildPath?.ChildTable != null ? _oChildPath.ChildTable.LastPushDt : null;

        //public string? ChildPath => _childpath;

        public string ChildPath
        {
            get
            {

                var childPath = "";
                if (!string.IsNullOrEmpty(Fullpath))
                {
                    var path = Fullpath;
                    var ststring = _oChildPath.PktableOwner + "." + _oChildPath.PktableName + "~";
                    var len = ststring.Length;
                    var start = path.IndexOf(ststring);
                    childPath = path.Substring(start + len);
                }
                return childPath;

            }
        }


    }
}
