using DataMigration.Constants;
using DataMigration.ModelFilters;
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
        private IIndexFilterManager _filtermanager;

        public SourceTablesController(SourceDDDbContext dbContext,
            IIndexFilterManager filtermanager) { 

            _dbcontext = dbContext;
            _filtermanager = filtermanager;
        }


        [ClearHistory(Order = 1)]
        [AddHistory("Source Tables", ClassNames.SourceTable, Order = 2)]
        public async Task<IActionResult> Index(int? page, SourceTablesFilter indexFilter)
        {
            indexFilter = _filtermanager.ProcFilter<SourceTablesFilter>(indexFilter, this);

            ViewData["Title"] = "Source Tables";
            ViewData["listaction"] = "indexlist";


            return View();


        }


        /// <summary>
        /// The third parameter in the AddHistory is useReferrerAction.  This flag makes sure that 
        /// return links created from this Action will go to Index, not indexlist.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="indexFilter"></param>
        /// <returns></returns>
        [ClearHistory(Order = 1)]
        [AddHistory("Source Tables", ClassNames.SourceTable, 1, Order = 2)]
        public async Task<IActionResult> indexlist(int? page, SourceTablesFilter indexFilter)
        {
            try
            {
                indexFilter = _filtermanager.ProcFilter<SourceTablesFilter>(indexFilter, this);
            }
            catch (Exception o)
            {
                var x = o;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist =  await _dbcontext.Tables
                .Include(c => c.Columns)
                .ThenInclude(cc => cc.ColumnTargets)
                .Where(indexFilter.ModelFilter)
                .OrderBy(c => c.TableSchema)
                .ThenBy(c => c.TableName)
                .ToPagedListAsync(pageSize, pageNumber);

            return PartialView("_indexlist", returnlist);

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



        private async Task<SourceDDContext.Models.Table?> _getRecord(string tableschema, string tablename)
        {
            var dbrecord = await _dbcontext.Tables
                .Include(m => m.Columns)
                .ThenInclude(c => c.ColumnTargets)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename);

            return dbrecord;

        }

    }
}
