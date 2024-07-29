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


        public async Task<MatchedColumnsViewModel> LinkColumns(MatchColumnsParms parms)
        {
            var srcc = await _getSrcColumn(parms.SrcTableSchema, parms.SrcTableName, parms.SrcColumnName);
            if (srcc == null)
            {
                throw new Exception("Source Column Not Found.");
            }
            var tgtc = await _getTgtColumn(parms.TgtTableSchema, parms.TgtTableName, parms.TgtColumnName);
            if (tgtc == null)
            {
                throw new Exception("Target Column Not Found.");
            }

            //Clear out any existing targets
            var existTargets = await _tgtdbcontext.Columns.Where(c => c.SourceTable == parms.SrcTableName && c.SourceColumn == parms.SrcColumnName).ToListAsync();
            foreach (var item in existTargets)
            {
                _tgtdbcontext.Update(item);
                item.SourceColumn = null;
                item.SourceTable = null;
            }

            //Update the Source Column
            _srcdbcontext.Update(srcc);
            srcc.DestinationTable = parms.TgtTableName;
            srcc.DestinationColumn = parms.TgtColumnName;

            _tgtdbcontext.Update(tgtc);
            tgtc.SourceTable = parms.SrcTableName;
            tgtc.SourceColumn = parms.SrcColumnName;

            await _srcdbcontext.SaveChangesAsync();
            await _tgtdbcontext.SaveChangesAsync();


            var vm = new MatchedColumnsViewModel(new SourceColumnViewModel(srcc), new TargetColumnViewModel(tgtc));
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
