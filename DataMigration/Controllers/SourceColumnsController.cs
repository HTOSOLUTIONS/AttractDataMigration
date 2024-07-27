using DataMigration.Constants;
using DataMigration.ViewModels;
using HTOTools;
using HTOTools.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;
using SourceDDContext.Data;

namespace DataMigration.Controllers
{
    public class SourceColumnsController : Controller
    {
        private SourceDDDbContext _dbcontext;

        public SourceColumnsController(SourceDDDbContext dbContext)
        {

            _dbcontext = dbContext;

        }

        [ClearHistory(Order = 1)]
        [AddHistory("Source Columns", ClassNames.SourceColumn, Order = 2)]
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist = await _dbcontext.Columns.ToPagedListAsync(pageSize, pageNumber);

            //var pgList = returnlist.Select(c => new SourceTableViewModel(c)).ToDynamicPagedList(pageSize,pageNumber);

            return View(returnlist);


        }

        [AddHistory("Source Columns Details", ClassNames.SourceColumn)]
        public async Task<IActionResult> Details(string tableschema, string tablename, string columnname)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                return NotFound();
            }

            var dbrecord = await _getRecord(tableschema, tablename, columnname);

            if (dbrecord == null)
            {
                return NotFound();
            }

            return View(new SourceColumnViewModel(dbrecord));


        }


        public async Task<IActionResult> Edit(string tableschema, string tablename, string columnname)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                return NotFound();
            }

            var dbrecord = await _getRecord(tableschema, tablename, columnname);

            if (dbrecord == null)
            {
                return NotFound();
            }


            return View(new SourceColumnViewModel(dbrecord));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("MigrationHistoryId,Description,DateTime")]
        public async Task<IActionResult> Edit(string tableschema, string tablename, string columnname, SourceDDContext.Models.Column viewmodel)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbrecord = await _getRecord(tableschema, tablename, columnname);

                if (dbrecord == null)
                {
                    return NotFound();
                }

                try
                {
                    dbrecord.Description = viewmodel.Description;
                    dbrecord.NeedsMigration = viewmodel.NeedsMigration;
                    dbrecord.DestinationTable = viewmodel.DestinationTable;
                    dbrecord.DestinationColumn = viewmodel.DestinationColumn;
                    _dbcontext.Update(dbrecord);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                return ActionHistory.ReturntoPreviousAction(HttpContext);
                //return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }



        private async Task<SourceDDContext.Models.Column> _getRecord(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _dbcontext.Columns
                .Include(m => m.Table)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

    }

}
