using HTOTools;
using System.Linq.Expressions;
using System.Security.Claims;
using SourceDDContext.Models;

namespace DataMigration.ModelFilters
{
    public class SourceTablesFilter : BaseIndexFilters
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
