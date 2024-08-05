using DataMigration.ParameterModels;
using DataMigration.ViewModels;
using IDataMigrations.Interfaces;
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
            var srcc = await _getSrcColumn(parms.SourceSchema, parms.SourceTable, parms.SourceColumn);
            if (srcc == null)
            {
                throw new Exception("Source Column Not Found." );
            }
            var tgtc = await _getTgtColumn(parms.TargetSchema, parms.TargetTable, parms.TargetColumn);
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
            var srcc = await _getSrcColumn(parms.SourceSchema, parms.SourceTable, parms.SourceColumn);
            if (srcc == null)
            {
                throw new Exception("Source Column Not Found.");
            }
            var tgtc = await _getTgtColumn(parms.TargetSchema, parms.TargetTable, parms.TargetColumn);
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
            var colTgtExists = srcc.ColumnTargets.Any(x => x.SourceSchema == parms.SourceSchema 
                && x.SourceColumn == parms.SourceColumn
                && x.SourceTable == parms.SourceTable
                && x.TargetColumn == parms.TargetColumn
                && x.TargetTable == parms.TargetTable);


            if (!colTgtExists) {

                _srcdbcontext.ColumnTargets.Add(new SourceDDContext.Models.ColumnTarget()
                {
                    SourceSchema = parms.SourceSchema
                    , SourceColumn = parms.SourceColumn
                    , SourceTable = parms.SourceTable
                    , TargetSchema= parms.TargetSchema
                    , TargetColumn = parms.TargetColumn
                    , TargetTable = parms.TargetTable
                });

            }

            var colSrcExists = tgtc.ColumnSources.Any(x => x.TargetSchema == parms.TargetSchema
                && x.SourceColumn == parms.SourceColumn
                && x.SourceTable == parms.SourceTable
                && x.TargetColumn == parms.TargetColumn
                && x.TargetTable == parms.TargetTable);


            if (!colSrcExists)
            {

                _tgtdbcontext.ColumnSources.Add(new TargetDDContext.Models.ColumnSource()
                {
                    SourceSchema = parms.SourceSchema
                    ,SourceColumn = parms.SourceColumn
                    ,SourceTable = parms.SourceTable
                    ,TargetSchema = parms.TargetSchema
                    ,TargetColumn = parms.TargetColumn
                    ,TargetTable = parms.TargetTable
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

        public async Task<bool> UnlinkColumns(IColumnMapping parms)
        {
            bool succ = false;

            var srcc = await _srcdbcontext.ColumnTargets.
                Where(c => c.SourceTable == parms.SourceTable
                            && c.SourceColumn == parms.SourceColumn
                            && c.TargetTable == parms.TargetTable
                            && c.TargetColumn == parms.TargetColumn
            ).FirstOrDefaultAsync();

            if (srcc == null)
            {
                throw new Exception("Source Column Not Found.");
            }

            var tgtc = await _tgtdbcontext.ColumnSources.
                Where(c => c.SourceTable == parms.SourceTable
                            && c.SourceColumn == parms.SourceColumn
                            && c.TargetTable == parms.TargetTable
                            && c.TargetColumn == parms.TargetColumn
            ).FirstOrDefaultAsync();

            if (tgtc == null)
            {
                throw new Exception("Target Column Not Found.");
            }

            _srcdbcontext.Remove(srcc);
            _tgtdbcontext.Remove(tgtc);
            await _srcdbcontext.SaveChangesAsync();
            await _tgtdbcontext.SaveChangesAsync();

            succ = true;

            return succ;
        }


        private async Task<SourceDDContext.Models.Column?> _getSrcColumn(string? tableschema, string tablename, string columnname)
        {
            if (string.IsNullOrEmpty(tableschema))
            {
                throw new Exception("No Table Schema");
            }


            var dbrecord = await _srcdbcontext.Columns
                .Include(m => m.Table)
                .Include(m => m.ColumnTargets)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

        private async Task<TargetDDContext.Models.Column?> _getTgtColumn(string? tableschema, string tablename, string columnname)
        {
            if (string.IsNullOrEmpty(tableschema))
            {
                throw new Exception("No Table Schema");
            }
            var dbrecord = await _tgtdbcontext.Columns
                .Include(m => m.Table)
                .Include(m => m.ColumnSources)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }




    }
}
