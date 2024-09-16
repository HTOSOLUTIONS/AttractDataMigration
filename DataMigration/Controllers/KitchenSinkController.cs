using DataMigration.ParameterModels;
using DataMigration.Services.KitchenSink;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SourceDDContext.Data;
using SQLTools;
using TargetDDContext.Data;

namespace DataMigration.Controllers
{
    public class KitchenSinkController : Controller
    {
        private readonly SourceDDDbContext _srcdbcontext;
        private readonly TargetDDDbContext _tgtdbcontext;
        private readonly IKitchenSink _kitchensink;
        private readonly ISQLWriter _sQLWriter;


        public KitchenSinkController(SourceDDDbContext srcdb, TargetDDDbContext tgtdb, IKitchenSink kitchenSink, ISQLWriter sQLWriter)
        {

            _srcdbcontext = srcdb;
            _tgtdbcontext = tgtdb;
            _kitchensink = kitchenSink;
            _sQLWriter = sQLWriter;
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

        [HttpPost]
        public async Task<JsonResult> LinkColumns(MatchColumnsParms parms)
        {
            if (!parms.AllThere)
            {
                Response.StatusCode = 404;
                return new JsonResult(new { responseText = "Insufficient Parameters" });

            }
            try
            {
                var vm = await _kitchensink.LinkColumns(parms);
                return new JsonResult(vm);

            }
            catch (Exception oe)
            {
                Response.StatusCode = 404;
                return new JsonResult(new { responseText = oe.Message });
            }

        }


        [HttpPost]
        public async Task<JsonResult> UnlinkColumns(MatchColumnsParms parms)
        {
            try
            {
                var vm = await _kitchensink.UnlinkColumns(parms);
                return new JsonResult(vm);

            }
            catch (Exception oe)
            {
                Response.StatusCode = 404;
                return new JsonResult(new { responseText = oe.Message });
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
