using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataMigration.Data;
using DataMigration.Models;

namespace DataMigration.Controllers
{
    public class MigrationHistoriesController : Controller
    {
        private readonly DataMigrationDbContext _dbcontext;

        public MigrationHistoriesController(DataMigrationDbContext context)
        {
            _dbcontext = context;
        }

        // GET: MigrationHistories
        public async Task<IActionResult> Index()
        {
            return View(await _dbcontext.MigrationHistory.ToListAsync());
        }

        // GET: MigrationHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var migrationHistory = await _dbcontext.MigrationHistory
                .FirstOrDefaultAsync(m => m.MigrationHistoryId == id);
            if (migrationHistory == null)
            {
                return NotFound();
            }

            return View(migrationHistory);
        }

        // GET: MigrationHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MigrationHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MigrationHistoryId,Description,DateTime")] MigrationHistory migrationHistory)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Add(migrationHistory);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(migrationHistory);
        }

        // GET: MigrationHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var migrationHistory = await _dbcontext.MigrationHistory.FindAsync(id);
            if (migrationHistory == null)
            {
                return NotFound();
            }
            return View(migrationHistory);
        }

        // POST: MigrationHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MigrationHistoryId,Description,DateTime")] MigrationHistory migrationHistory)
        {
            if (id != migrationHistory.MigrationHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(migrationHistory);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MigrationHistoryExists(migrationHistory.MigrationHistoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(migrationHistory);
        }

        // GET: MigrationHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var migrationHistory = await _dbcontext.MigrationHistory
                .FirstOrDefaultAsync(m => m.MigrationHistoryId == id);
            if (migrationHistory == null)
            {
                return NotFound();
            }

            return View(migrationHistory);
        }

        // POST: MigrationHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var migrationHistory = await _dbcontext.MigrationHistory.FindAsync(id);
            if (migrationHistory != null)
            {
                _dbcontext.MigrationHistory.Remove(migrationHistory);
            }

            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MigrationHistoryExists(int id)
        {
            return _dbcontext.MigrationHistory.Any(e => e.MigrationHistoryId == id);
        }
    }
}
