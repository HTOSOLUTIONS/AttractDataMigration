using DataMigration.ParameterModels;
using DataMigration.ViewModels;
using Microsoft.EntityFrameworkCore;
using SourceDDContext.Data;
using TargetDDContext.Data;

namespace DataMigration.Services.KitchenSink
{
    public class KitchenSink : IKitchenSink
    {

        private readonly SourceDDDbContext _srcdbcontext;
        private readonly TargetDDDbContext _tgtdbcontext;


        public KitchenSink(SourceDDDbContext srcdb, TargetDDDbContext tgtdb)
        {

            _srcdbcontext = srcdb;
            _tgtdbcontext = tgtdb;

        }

        public async Task<MatchedColumnsViewModel> GetMatchPreviewVM(MatchColumnsParms parms)
        {
            var srcc = await _getSrcColumn(parms.SrcTableSchema, parms.SrcTableName, parms.SrcColumnName);
            if (srcc == null)
            {
                throw new Exception("Source Column Not Found." );
            }
            var tgtc = await _getTgtColumn(parms.TgtTableSchema, parms.TgtTableName, parms.TgtColumnName);
            if (tgtc == null)
            {
                throw new Exception("Target Column Not Found.");
            }

            var vm = new MatchedColumnsViewModel(new SourceColumnViewModel(srcc), new TargetColumnViewModel(tgtc)) ;
            vm.MatchColumnsParms = parms;

            return vm;

        }


        private async Task<SourceDDContext.Models.Column> _getSrcColumn(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _srcdbcontext.Columns
                .Include(m => m.Table)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

        private async Task<TargetDDContext.Models.Column> _getTgtColumn(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _tgtdbcontext.Columns
                .Include(m => m.Table)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }




    }
}
