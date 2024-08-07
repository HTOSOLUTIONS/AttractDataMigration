using DataMigration.Constants;
using DataMigration.ModelFilters;
using DataMigration.ViewModels;
using HTOTools;
using HTOTools.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;
using TargetDDContext.Data;


namespace DataMigration.Controllers
{
    public class TargetTablesController : Controller
    {
        private readonly TargetDDDbContext _dbcontext;
        private IIndexFilterManager _filtermanager;

        public TargetTablesController(TargetDDDbContext dbcontext,
            IIndexFilterManager filtermanager) { 

            _dbcontext = dbcontext;
            _filtermanager = filtermanager;
        }


        [ClearHistory(Order = 1)]
        [AddHistory("Target Tables",ClassNames.TargetTable, Order = 2)]
        public IActionResult Index(int? page, TargetTablesFilter indexFilter)
        {
            indexFilter = _filtermanager.ProcFilter<TargetTablesFilter>(indexFilter, this);

            ViewData["Title"] = "Target Tables";
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
        [AddHistory("Target Tables", ClassNames.TargetTable, 1, Order = 2)]
        public async Task<IActionResult> indexlist(int? page, TargetTablesFilter indexFilter)
        {
            try
            {
                indexFilter = _filtermanager.ProcFilter<TargetTablesFilter>(indexFilter, this);
            }
            catch (Exception o)
            {
                var x = o;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist =  await _dbcontext.Tables.Include(c => c.Columns).Where(indexFilter.ModelFilter)
                .Where(indexFilter.ModelFilter)
                .OrderBy(c => c.TableSchema)
                .ThenBy(c => c.TableName)
                .ToPagedListAsync(pageSize, pageNumber);

            return PartialView("_indexlist", returnlist);

        }



        [AddHistory("Target Table Details", ClassNames.TargetTable)]
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

            return View(new TargetTableWithColumnsViewModel(dbrecord));


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


            return View(new TargetTableWithColumnsViewModel(dbrecord));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("MigrationHistoryId,Description,DateTime")]
        public async Task<IActionResult> Edit(string tableschema, string tablename, TargetDDContext.Models.Table viewmodel)
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
                    dbrecord.UseDomain = viewmodel.UseDomain;
                    dbrecord.UseType = viewmodel.UseType;
                    _dbcontext.Update(dbrecord);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                //return RedirectToAction(nameof(Index));
                return ActionHistory.ReturntoPreviousAction(HttpContext);

            }
            return View(viewmodel);
        }


        public IActionResult TargetTableColumnsVC(string tablename)
        {
            return ViewComponent(nameof(TargetTableColumnsVC), new { tablename });
        }


        public IActionResult TargetTableVC(string tablename)
        {
            return ViewComponent(nameof(TargetTableVC), new { tablename });
        }



        private async Task<TargetDDContext.Models.Table?> _getRecord(string tableschema, string tablename)
        {
            var dbrecord = await _dbcontext.Tables
                .Include(c => c.Columns)
                .ThenInclude(m => m.ColumnSources)
                .Include(c => c.ParentPaths)
                .ThenInclude(pp => pp.ParentTable)
                .Include(c => c.ParentPaths)
                .ThenInclude(pp => pp.ForeignKey)
                .Include(c => c.ChildPaths)
                .ThenInclude(cp => cp.ChildTable)
                .Where(c => c.TableName == tablename)
                .FirstOrDefaultAsync();

            return dbrecord;

        }


    }
}
