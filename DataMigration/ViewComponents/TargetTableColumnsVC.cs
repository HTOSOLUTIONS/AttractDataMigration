using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataMigration.ViewModels;
using TargetDDContext.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DataMigration.ViewComponents
{
    public class TargetTableColumnsVC : ViewComponent
    {

        private readonly TargetDDDbContext _dbcontext;

        public TargetTableColumnsVC(TargetDDDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tablename)
        {

            var targetTable = await _dbcontext.Tables
                .Include(c => c.Columns)
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
