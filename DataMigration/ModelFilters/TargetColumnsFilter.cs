using HTOTools;
using System.Linq.Expressions;
using System.Security.Claims;
using TargetDDContext.Models;

namespace DataMigration.ModelFilters
{
    public class TargetColumnsFilter : BaseIndexFilters
    {

        public string? TableName { get; set; }


        public string? TableSchema { get; set; }

        public string? ColumnName { get; set; }

        public bool? xNonProductionData { get; set; }

        public bool? PrimaryKeys { get; set; }

        public bool? ForeignKeys { get; set; }

        public bool? CreateModifyTrackers { get; set; }


        public override void SetFilter(ClaimsPrincipal user)
        {

            ModelFilter = GetModelFilter(user);
        }

        public Expression<Func<Column, bool>> ModelFilter { get; set; }

        public Expression<Func<Column, bool>> GetModelFilter(ClaimsPrincipal user)
        {
            var selectfilter = PredicateBuilder.True<Column>();
            if (!string.IsNullOrEmpty(TableSchema))
            {
                selectfilter = selectfilter.And(c => c.TableSchema.ToUpper().Contains(TableSchema.ToUpper()));
                filterOn = true;
            }
            if (!string.IsNullOrEmpty(TableName))
            {
                selectfilter = selectfilter.And(c => c.TableName.ToUpper().Contains(TableName.ToUpper()));
                filterOn = true;
            }

            if (!string.IsNullOrEmpty(ColumnName))
            {
                selectfilter = selectfilter.And(c => c.ColumnName.ToUpper().Contains(ColumnName.ToUpper()));
                filterOn = true;
            }
            if (xNonProductionData != null)
            {
                var nonproductionschemas = new List<string>() { "XSTG", "UI", "REC", "PROCESS", "MSG", "EVT" };

                if (xNonProductionData == false)
                {
                    selectfilter = selectfilter.And(c => !nonproductionschemas.Contains(c.TableSchema.ToUpper()));
                }
                else
                {
                    selectfilter = selectfilter.And(c => nonproductionschemas.Contains(c.TableSchema.ToUpper()));

                }
                filterOn = true;

            }
            if (PrimaryKeys != null)
            {

                if (PrimaryKeys == false)
                {
                    selectfilter = selectfilter.And(c => c.UseType != "PrimaryKey");
                }
                else
                {
                    selectfilter = selectfilter.And(c => c.UseType == "PrimaryKey");

                }
                filterOn = true;

            }
            if (ForeignKeys != null)
            {

                if (ForeignKeys == false)
                {
                    selectfilter = selectfilter.And(c => c.UseType != "ForeignKey");
                }
                else
                {
                    selectfilter = selectfilter.And(c => c.UseType == "ForeignKey");

                }
                filterOn = true;

            }
            if (CreateModifyTrackers != null)
            {

                if (CreateModifyTrackers == false)
                {
                    selectfilter = selectfilter.And(c => c.UseType != "CreateModifyTracking");
                }
                else
                {
                    selectfilter = selectfilter.And(c => c.UseType == "CreateModifyTracking");

                }
                filterOn = true;

            }

            return selectfilter;

        }

    }
}
