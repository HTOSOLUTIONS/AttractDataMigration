using DataMigration.ParameterModels;
using DataMigration.ViewModels;

namespace DataMigration.Services.KitchenSink
{
    public interface IKitchenSink
    {
        Task<MatchedColumnsViewModel> GetMatchPreviewVM(MatchColumnsParms parms);

        Task<MatchedColumnsViewModel> LinkColumns(MatchColumnsParms parms);

    }
}
