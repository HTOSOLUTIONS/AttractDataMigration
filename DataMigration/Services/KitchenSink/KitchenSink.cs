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
            //No longer needed...a SourceColumn can have multiple targets
            //var existTargets = await _tgtdbcontext.Columns.Where(c => c.SourceTable == parms.SrcTableName && c.SourceColumn == parms.SrcColumnName).ToListAsync();
            //foreach (var item in existTargets)
            //{
            //    _tgtdbcontext.Update(item);
            //    item.SourceColumn = null;
            //    item.SourceTable = null;
            //}

            //Update the Source Column
            var colTgtExists = srcc.ColumnTargets.Any(x => x.SourceSchema == parms.SrcTableSchema 
                && x.SourceColumn == parms.SrcColumnName
                && x.SourceTable == parms.SrcTableName
                && x.TargetColumn == parms.TgtColumnName
                && x.TargetTable == parms.TgtTableName);


            if (!colTgtExists) {

                _srcdbcontext.ColumnTargets.Add(new SourceDDContext.Models.ColumnTarget()
                {
                    SourceSchema = parms.SrcTableSchema
                    , SourceColumn = parms.SrcColumnName
                    , SourceTable = parms.SrcTableName
                    , TargetSchema= parms.TgtTableSchema
                    , TargetColumn = parms.TgtColumnName
                    , TargetTable = parms.TgtTableName
                });

            }

            var colSrcExists = tgtc.ColumnSources.Any(x => x.TargetSchema == parms.TgtTableSchema
                && x.SourceColumn == parms.SrcColumnName
                && x.SourceTable == parms.SrcTableName
                && x.TargetColumn == parms.TgtColumnName
                && x.TargetTable == parms.TgtTableName);


            if (!colSrcExists)
            {

                _tgtdbcontext.ColumnSources.Add(new TargetDDContext.Models.ColumnSource()
                {
                    SourceSchema = parms.SrcTableSchema
                    ,SourceColumn = parms.SrcColumnName
                    ,SourceTable = parms.SrcTableName
                    ,TargetSchema = parms.TgtTableSchema
                    ,TargetColumn = parms.TgtColumnName
                    ,TargetTable = parms.TgtTableName
                });

            }


            //Update the Target Column
            //_tgtdbcontext.Update(tgtc);
            //tgtc.SourceTable = parms.SrcTableName;
            //tgtc.SourceColumn = parms.SrcColumnName;

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
                .Include(m => m.ColumnTargets)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

        private async Task<TargetDDContext.Models.Column> _getTgtColumn(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _tgtdbcontext.Columns
                .Include(m => m.Table)
                .Include(m => m.ColumnSources)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }




    }
}
