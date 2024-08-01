using TargetDDContext.Models;

namespace DataMigration.ViewModels
{
    public class ChildPathViewModel
    {
        private readonly string _fktablowner;
        private readonly string _fktablename;
        private readonly string _childpath;


        public ChildPathViewModel(string fktablowner, string fktablename, string childpath)
        {
            _fktablowner = fktablowner;
            _fktablename = fktablename;
            _childpath = childpath;

        }

        public string FktableOwner => _fktablowner;

        public string FktableName => _fktablename;

        public string? ChildPath => _childpath;


    }
}
