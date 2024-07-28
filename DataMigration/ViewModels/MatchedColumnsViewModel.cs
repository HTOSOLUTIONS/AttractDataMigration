namespace DataMigration.ViewModels
{
    public class MatchedColumnsViewModel
    {
        private readonly SourceColumnViewModel _srcvm;
        private readonly TargetColumnViewModel _dstvm;

        public MatchedColumnsViewModel(SourceColumnViewModel srcvm, TargetColumnViewModel dstvm) { 
            _srcvm = srcvm;
            _dstvm = dstvm;
        }

        public SourceColumnViewModel SourceColumn => _srcvm;

        public TargetColumnViewModel TargetColumn => _dstvm;

        public string? Status { get; set; }

        public string? Description { get; set; }

        public bool? ReadyToLink { get; set; }





    }
}
