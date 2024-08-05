using TargetDDContext.Models;

namespace DataMigration.ViewModels
{
    public class ParentPathViewModel
    {
        private readonly string _fktablowner;
        private readonly string _fktablename;
        private readonly string _childpath;
        private readonly FamilyPath _oParentPath;

        //, string fktablowner, string fktablename, string childpath

        public ParentPathViewModel(FamilyPath familypath)
        {
            _oParentPath = familypath;

        }

        public string PktableOwner => _oParentPath.PktableOwner;

        public string PktableName => _oParentPath.PktableName;

        public string Fullpath => _oParentPath.Fullpath;

        public int? Records => _oParentPath?.ParentTable != null ? _oParentPath.ParentTable.RowCount : null;

        public int? Columns => _oParentPath?.ParentTable != null ? _oParentPath.ParentTable.ColCount : null;


        //public string? ChildPath => _childpath;

        public string ParentPath
        {
            get
            {

                var parentPath = "";
                if (!string.IsNullOrEmpty(Fullpath))
                {
                    var path = Fullpath;
                    var ststring = _oParentPath.FktableOwner + "." + _oParentPath.FktableName + "~";
                    var len = ststring.Length;
                    var start = path.IndexOf(ststring);
                    parentPath = path.Substring(0, start);
                }
                return parentPath;

            }
        }
    }
}
