using DataMigration.Constants;
using DataMigration.ModelFilters;
using DataMigration.ViewModels;
using HTOTools;
using HTOTools.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;
using TargetDDContext.Data;



namespace DataMigration.Controllers
{
    public class TargetColumnsController : Controller
    {
        private readonly TargetDDDbContext _dbcontext;
        private IIndexFilterManager _filtermanager;

        public TargetColumnsController(TargetDDDbContext dbcontext,
            IIndexFilterManager filtermanager)
        {

            _dbcontext = dbcontext;
            _filtermanager = filtermanager;
        }


        [ClearHistory(Order = 1)]
        [AddHistory("Target Columns", ClassNames.TargetColumn, Order = 2)]
        public async Task<IActionResult> Index(int? page, TargetColumnsFilter indexFilter)
        {
            indexFilter = _filtermanager.ProcFilter<TargetColumnsFilter>(indexFilter, this);

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
        [AddHistory("Target Columns", ClassNames.TargetColumn, 1, Order = 2)]
        public async Task<IActionResult> indexlist(int? page, TargetColumnsFilter indexFilter)
        {
            try
            {
                indexFilter = _filtermanager.ProcFilter<TargetColumnsFilter>(indexFilter, this);
            }
            catch (Exception o)
            {
                var x = o;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist = await _dbcontext.Columns
                .Include(c => c.Table)
                .Where(indexFilter.ModelFilter)
                .OrderBy(c => c.ColumnName)
                .ThenBy(c => c.TableName)
                .ToPagedListAsync(pageSize, pageNumber);

            return PartialView("_indexlist", returnlist);

        }



        [AddHistory("Target Column Details", ClassNames.TargetColumn, 1, Order = 2)]
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

            return View(new TargetColumnViewModel(dbrecord));


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


            return View(new TargetColumnViewModel(dbrecord));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("MigrationHistoryId,Description,DateTime")]
        public async Task<IActionResult> Edit(string tableschema, string tablename, string columnname, TargetDDContext.Models.Column viewmodel)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                return NotFound();
            }

            ModelState.Remove<TargetDDContext.Models.Column>(c => c.Table);

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


        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Bind("MigrationHistoryId,Description,DateTime")]
        public async Task<JsonResult> EditModal(string tableschema, string tablename, string columnname, TargetDDContext.Models.Column viewmodel)
        {
            if (string.IsNullOrEmpty(tableschema) || string.IsNullOrEmpty(tablename) || string.IsNullOrEmpty(columnname))
            {
                Response.StatusCode = 404;
                return new JsonResult(new { responseText = "Insufficient Parameters" });
            }

            ModelState.Remove<TargetDDContext.Models.Column>(c => c.Table);
            ModelState.Remove<TargetDDContext.Models.Column>(c => c.TableCatalog);
            ModelState.Remove<TargetDDContext.Models.Column>(c => c.IDDTable);

            if (ModelState.IsValid)
            {
                var dbrecord = await _getRecord(tableschema, tablename, columnname);

                if (dbrecord == null)
                {
                    Response.StatusCode = 404;
                    return new JsonResult(new { responseText = "Insufficient Parameters" });
                }

                try
                {
                    dbrecord.NeedsMigration = viewmodel.NeedsMigration;
                    _dbcontext.Update(dbrecord);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Response.StatusCode = 404;
                    return new JsonResult(new { responseText = "Concurrency exception" });
                }
                //return RedirectToAction(nameof(Index));
                return new JsonResult(dbrecord);
            }
            else
            {
                Response.StatusCode = 404;
                return new JsonResult(new { responseText = "Invalid model." });

            }
        }


        private async Task<TargetDDContext.Models.Column?> _getRecord(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _dbcontext.Columns
                .Include(m => m.Table)
                .ThenInclude(m => m.ChildPaths)
                .Include(m => m.Table)
                .ThenInclude(m => m.ParentPaths)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

    }
}
