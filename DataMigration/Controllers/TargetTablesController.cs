﻿using DataMigration.Data;
using DataMigration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TargetDDContext.Data;
using TargetDDContext.Models;


namespace DataMigration.Controllers
{
    public class TargetTablesController : Controller
    {
        private readonly TargetDDDbContext _dbcontext;

        public TargetTablesController(TargetDDDbContext dbcontext) { 

            _dbcontext = dbcontext;
        }


        public async Task<IActionResult> Index()
        {

            var returnlist = await _dbcontext.Tables.ToListAsync();



            return View(returnlist);


        }

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

            return View(dbrecord);


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


            return View(dbrecord);
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
                    _dbcontext.Update(dbrecord);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }




        private async Task<TargetDDContext.Models.Table?> _getRecord(string tableschema, string tablename)
        {
            var dbrecord = await _dbcontext.Tables
                .Include(m => m.Columns)
                .Include(m => m.ChildPaths)
                .Include(m => m.ParentPaths)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename);

            return dbrecord;

        }


    }
}
