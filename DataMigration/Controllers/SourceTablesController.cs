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
    public class SourceTablesController : Controller
    {

        private SourceDDDbContext _dbcontext;

        public SourceTablesController(SourceDDDbContext dbContext) { 

            _dbcontext = dbContext;

        }


        [ClearHistory(Order = 1)]
        [AddHistory("Source Tables", ClassNames.SourceTable, Order = 2)]
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist = await _dbcontext.Tables.ToPagedListAsync(pageSize,pageNumber);

            //var pgList = returnlist.Select(c => new SourceTableViewModel(c)).ToDynamicPagedList(pageSize,pageNumber);

            return View(returnlist);


        }

        [AddHistory("Source Table Details", ClassNames.SourceTable)]
        public async Task<IActionResult> Details(string tableschema, string tablename)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename))
            {
                return NotFound();
            }

            var dbrecord = await _getRecord(tableschema, tablename);

            if (dbrecord == null)
            {
                return NotFound();
            }

            return View(new SourceTableWithColumnsViewModel(dbrecord));


        }


        public async Task<IActionResult> Edit(string tableschema, string tablename)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename))
            {
                return NotFound();
            }

            var dbrecord = await _getRecord(tableschema, tablename);

            if (dbrecord == null)
            {
                return NotFound();
            }


            return View(new SourceTableWithColumnsViewModel(dbrecord));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("MigrationHistoryId,Description,DateTime")]
        public async Task<IActionResult> Edit(string tableschema, string tablename, SourceDDContext.Models.Table viewmodel)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbrecord = await _getRecord(tableschema, tablename);

                if (dbrecord == null)
                {
                    return NotFound();
                }

                try
                {
                    dbrecord.Description = viewmodel.Description;
                    dbrecord.NeedsMigration = viewmodel.NeedsMigration;
                    dbrecord.DestinationTable = viewmodel.DestinationTable;
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



        private async Task<SourceDDContext.Models.Table> _getRecord(string tableschema, string tablename)
        {
            var dbrecord = await _dbcontext.Tables
                .Include(m => m.Columns)
                .ThenInclude(c => c.ColumnTargets)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename);

            return dbrecord;

        }

    }
}
