using DataMigration.ParameterModels;
using DataMigration.ViewModels;
using IDataMigrations.Interfaces;

namespace DataMigration.Services.KitchenSink
{
    public interface IKitchenSink
    {
        Task<MatchedColumnsViewModel> GetMatchPreviewVM(MatchColumnsParms parms);

        Task<MatchedColumnsViewModel> LinkColumns(MatchColumnsParms parms);

        Task<bool> UnlinkColumns(IColumnMapping parms);


    }
}
