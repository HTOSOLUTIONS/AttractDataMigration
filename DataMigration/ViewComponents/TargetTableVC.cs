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

            var targetTable = await _dbcontext.Tables
                .Include(c => c.Columns)
                .ThenInclude(m => m.ColumnSources)
                .Include(c => c.ParentPaths)
                .Include(c => c.ChildPaths)
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
