using DataMigration.ParameterModels;
using DataMigration.Services.KitchenSink;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SourceDDContext.Data;
using TargetDDContext.Data;

namespace DataMigration.Controllers
{
    public class KitchenSinkController : Controller
    {
        private readonly SourceDDDbContext _srcdbcontext;
        private readonly TargetDDDbContext _tgtdbcontext;
        private readonly IKitchenSink _kitchensink;



        public KitchenSinkController(SourceDDDbContext srcdb, TargetDDDbContext tgtdb, IKitchenSink kitchenSink)
        {

            _srcdbcontext = srcdb;
            _tgtdbcontext = tgtdb;
            _kitchensink = kitchenSink;

        }


        public async Task<IActionResult> PreviewLink(MatchColumnsParms parms)
        {
            if (!parms.AllThere)
            {
                return RedirectToAction("AjaxNotFound", "Home", new { message = "Insufficient parameters." });

            }
            try
            {
                var vm = await _kitchensink.GetMatchPreviewVM(parms);

                return PartialView(vm);

            }
            catch (Exception oe)
            {
                return RedirectToAction("AjaxNotFound", "Home", new { message = oe.Message });
            }

        }

        private async Task<SourceDDContext.Models.Column> _getSrcColumn(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _srcdbcontext.Columns
                .Include(m => m.Table)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }

        private async Task<TargetDDContext.Models.Column> _getTgtColumn(string tableschema, string tablename, string columnname)
        {
            var dbrecord = await _tgtdbcontext.Columns
                .Include(m => m.Table)
                .FirstOrDefaultAsync(m => m.TableSchema == tableschema && m.TableName == tablename && m.ColumnName == columnname);

            return dbrecord;

        }




    }
}
