using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataMigration.ViewModels;
using TargetDDContext.Data;
using Microsoft.AspNetCore.Http.HttpResults;


namespace DataMigration.ViewComponents
{
    public class TargetTableVC : ViewComponent
    {

        private readonly TargetDDDbContext _dbcontext;

        public TargetTableVC(TargetDDDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync(string tablename)
        {

            //2024.08.01 - Include ChildPaths -> ChildTable to provide a count of records

            var targetTable = await _dbcontext.Tables
                .Include(c => c.Columns)
                .ThenInclude(m => m.ColumnSources)
                .Include(c => c.ParentPaths)
                .ThenInclude(pp => pp.ParentTable)
                .Include(c => c.ChildPaths)
                .ThenInclude(cp => cp.ChildTable)
                .Where(c => c.TableName == tablename)
                .FirstOrDefaultAsync();

            if (targetTable == null)
            {
                throw new Exception("Record not found");
            }

            return View(new TargetTableWithColumnsViewModel(targetTable));

        }



    }
}
