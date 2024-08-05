﻿using HTOTools;
using System.Linq.Expressions;
using System.Security.Claims;
using TargetDDContext.Models;

namespace DataMigration.ModelFilters
{
    public class TargetTablesFilter : BaseIndexFilters
    {

        public string? TableName { get; set; }

        public string? TableSchema { get; set; }

        public bool? xNonProductionData { get; set; }


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
            if (xNonProductionData != null)
            {
                var nonproductionschemas = new List<string>() { "XSTG", "UI", "REC", "PROCESS", "MSG", "EVT" };

                if (xNonProductionData == false)
                {
                    selectfilter = selectfilter.And(c => !nonproductionschemas.Contains( c.TableSchema.ToUpper()));
                } else
                {
                    selectfilter = selectfilter.And(c => nonproductionschemas.Contains(c.TableSchema.ToUpper()));

                }
                filterOn = true;

            }


            return selectfilter;

        }

    }
}
