using HTOTools;
//using Microsoft.CodeAnalysis.Elfie.Model;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Security.Claims;
using TargetDDContext.Models;

namespace DataMigration.ModelFilters
{
    public class TargetTablesFilter : BaseIndexFilters
    {

        public string? TableName { get; set; }


        public override void SetFilter(ClaimsPrincipal user)
        {

            ModelFilter = GetModelFilter(user);
        }

        public Expression<Func<Table, bool>> ModelFilter { get; set; }

        public Expression<Func<Table, bool>> GetModelFilter(ClaimsPrincipal user)
        {
            var selectfilter = PredicateBuilder.True<Table>();
            if (!string.IsNullOrEmpty(TableName))
            {
                selectfilter = selectfilter.And(c => c.TableName.ToUpper().Contains(TableName.ToUpper()));
                filterOn = true;
            }


            return selectfilter;

        }

    }
}
