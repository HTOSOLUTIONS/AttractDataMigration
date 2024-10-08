﻿using DataMigration.Constants;
using DataMigration.ModelFilters;
using DataMigration.ViewModels;
using HTOTools;
using HTOTools.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sakura.AspNetCore;
using SourceDDContext.Data;

namespace DataMigration.Controllers
{
    public class SourceColumnsController : Controller
    {
        private SourceDDDbContext _dbcontext;
        private IIndexFilterManager _filtermanager;

        public SourceColumnsController(SourceDDDbContext dbContext,
            IIndexFilterManager filtermanager)
        {

            _dbcontext = dbContext;
            _filtermanager = filtermanager;

        }

        [ClearHistory(Order = 1)]
        [AddHistory("Source Columns", ClassNames.SourceColumn, Order = 2)]
        public async Task<IActionResult> Index(int? page, SourceColumnsFilter indexFilter)
        {
            indexFilter = _filtermanager.ProcFilter<SourceColumnsFilter>(indexFilter, this);

            ViewData["Title"] = "Source Columns";
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
        [AddHistory("Source Columns", ClassNames.SourceColumn, 1, Order = 2)]
        public async Task<IActionResult> indexlist(int? page, SourceColumnsFilter indexFilter)
        {
            try
            {
                indexFilter = _filtermanager.ProcFilter<SourceColumnsFilter>(indexFilter, this);
            }
            catch (Exception o)
            {
                var x = o;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var returnlist = await _dbcontext.Columns
                .Include(c => c.Table)
                .Include(cc => cc.ColumnTargets)
                .Where(indexFilter.ModelFilter)
                .OrderBy(c => c.ColumnName)
                .ThenBy(c => c.TableName)
                .ToPagedListAsync(pageSize, pageNumber);

            return PartialView("_indexlist", returnlist);

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

            ModelState.Remove<SourceDDContext.Models.Column>(c => c.Table);

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
                    //dbrecord.DestinationTable = viewmodel.DestinationTable;
                    //dbrecord.DestinationColumn = viewmodel.DestinationColumn;
                    dbrecord.NeedsFollowUp = viewmodel.NeedsFollowUp;
                    dbrecord.Notes = viewmodel.Notes;

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
                .Include(m => m.ColumnTargets)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

    }

}
