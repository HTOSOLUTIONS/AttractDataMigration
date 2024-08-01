using TargetDDContext.Models;
using System.ComponentModel.DataAnnotations;


namespace DataMigration.ViewModels
{
    public class FamilyPathViewModel
    {

        protected readonly FamilyPath _familyPath;

        public FamilyPathViewModel(FamilyPath familyPath)
        {
            _familyPath = familyPath;
        }


        public string? Parentpath => _familyPath.Parentpath;

        public string PktableOwner => _familyPath.PktableOwner;

        public string PktableName => _familyPath.PktableName;

        public string FktableOwner => _familyPath.FktableOwner;

        public string FktableName => _familyPath.FktableName;

        public int? Children => _familyPath.Children;

        public string Fullpath => _familyPath.Fullpath == null ? string.Empty : _familyPath.Fullpath;


        public string ChildPath { get {

                var childPath = "";
                if (!string.IsNullOrEmpty(Fullpath))
                {
                    var path = Fullpath;
                    var ststring = PktableOwner + "." + PktableName + "~";
                    var len = ststring.Length;
                    var start = path.IndexOf(ststring);
                    childPath = path.Substring(start + len);
                } 
                return childPath;

            } 
        }

        //public virtual Table Table { get; set; }

        //public virtual Table TableNavigation { get; set; }


    }
}
