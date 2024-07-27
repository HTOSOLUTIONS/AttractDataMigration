

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TargetDDContext.Data;

namespace DataMigration.ViewComponents
{
    public class TaregetTablesBranchesVC : ViewComponent
    {
        private readonly TargetDDDbContext _dbcontext;

        public TaregetTablesBranchesVC(TargetDDDbContext dbContext)
        {
            _dbcontext = dbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync(string tablename)
        {

            var tablewithbranches = await _dbcontext.Tables
                .Include(c => c.ChildPaths)
                .Include(c => c.ParentPaths)
                .Where(c => c.TableName == tablename)
                .FirstOrDefaultAsync();


            return View(tablewithbranches);

        }

    }
}
