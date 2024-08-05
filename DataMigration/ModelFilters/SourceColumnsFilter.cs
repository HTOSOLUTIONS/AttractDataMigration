using HTOTools;
using System.Linq.Expressions;
using System.Security.Claims;
using SourceDDContext.Models;


namespace DataMigration.ModelFilters
{
    public class SourceColumnsFilter : BaseIndexFilters
    {

        public string? ColumnName { get; set; }

        public string? TableName { get; set; }

        public bool? NeedsMigration { get; set; }

        public bool? NeedsFollowUp { get; set; }

        public int? MinTargets { get; set; }

        public int? MaxTargets { get; set; }

        public int? MinDestinctValues { get; set; }

        public int? MaxDistinctValues { get; set; }

        public override void SetFilter(ClaimsPrincipal user)
        {

            ModelFilter = GetModelFilter(user);
        }

        public Expression<Func<Column, bool>> ModelFilter { get; set; }


        public Expression<Func<Column, bool>> GetModelFilter(ClaimsPrincipal user)
        {
            var selectfilter = PredicateBuilder.True<Column>();
            if (!string.IsNullOrEmpty(ColumnName))
            {
                selectfilter = selectfilter.And(c => c.ColumnName.ToUpper().Contains(ColumnName.ToUpper()));
                filterOn = true;
            }
            if (!string.IsNullOrEmpty(TableName))
            {
                selectfilter = selectfilter.And(c => c.TableName.ToUpper().Contains(TableName.ToUpper()));
                filterOn = true;
            }
            if (NeedsFollowUp != null)
            {
                if (NeedsFollowUp == true)
                {
                    selectfilter = selectfilter.And(c => c.NeedsFollowUp == true);
                }
                else {
                    selectfilter = selectfilter.And(c => c.NeedsFollowUp == false);
                }
                filterOn = true;
            }
            if (NeedsMigration != null)
            {
                if (NeedsMigration == true)
                {
                    selectfilter = selectfilter.And(c => c.NeedsMigration == true);
                }
                else
                {
                    selectfilter = selectfilter.And(c => c.NeedsMigration == false);
                }
                filterOn = true;
            }
            if (MinTargets != null)
            {
                selectfilter = selectfilter.And(c => c.ColumnTargets != null && c.ColumnTargets.Count >= MinTargets);
                filterOn = true;
            }
            if (MaxTargets != null)
            {
                selectfilter = selectfilter.And(c => c.ColumnTargets != null && c.ColumnTargets.Count <= MaxTargets);
                filterOn = true;
            }
            if (MinDestinctValues != null)
            {
                selectfilter = selectfilter.And(c => c.DistinctValues != null && c.DistinctValues >= MinDestinctValues);
                filterOn = true;
            }
            if (MaxDistinctValues != null)
            {
                selectfilter = selectfilter.And(c => c.DistinctValues != null && c.DistinctValues <= MaxDistinctValues);
                filterOn = true;
            }

            return selectfilter;

        }


    }
}
