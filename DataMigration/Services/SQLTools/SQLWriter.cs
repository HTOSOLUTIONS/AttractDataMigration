using DataMigration.Services.HTOTools;
using IDataMigrations.Interfaces;
using System.Text;
using TargetDDContext.Models;

namespace SQLTools
{
    public class SQLWriter : ISQLWriter
    {

        public string CreateTable(Table table, WriteParms? writeParms = null)
        {
            WriteParms _writeParms = writeParms ?? new WriteParms();

            var indenter = "     ";
            var ob = _writeParms.UseBrackets ? "[" : "";
            var cb = _writeParms.UseBrackets ? "]" : "";
            var sb = new StringBuilder();
            var fullTableName = ob + table.TableSchema + cb + "." + ob + table.TableName + cb;
            sb.Append("CREATE TABLE " + fullTableName + " (");



            foreach (var column in table.Columns.OrderBy(c => c.OrdinalPosition)) { 
                sb.AppendLine(indenter + column.CreateLine(table));
            }
            if (table.PrimaryKeys.Any())
            {
                sb.AppendLine(" CONSTRAINT " + ob + table.PrimaryKeys.First().ColumnName.ToSnakeCase2() + cb + " PRIMARY KEY CLUSTERED ");
                sb.AppendLine("(");
                var keys = table.PrimaryKeys.Select(k => "[" + k.ColumnName.ToSnakeCase2() + "] ASC").ToList();
                sb.AppendLine(indenter + string.Join(", ", keys));
                sb.AppendLine(") WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]");
                sb.AppendLine(") ON [PRIMARY]");
            }
            sb.AppendLine("GO");
            sb.AppendLine("");
            var defltConstraints = table.Columns.SelectMany(c => c.DefaultConstraints).ToList();
            if (defltConstraints.Any())
            {
                foreach (var constraint in defltConstraints)
                {
                    sb.AppendLine("ALTER TABLE " + fullTableName + " ADD CONSTRAINT " + ob + constraint.ConstraintName + cb + " DEFAULT " + constraint.Definition
                            + " FOR " + ob + constraint.ColumnName.ToSnakeCase2() + cb
                        );
                    sb.AppendLine("GO");
                    sb.AppendLine("");
                }
            }
            var parentKeys = table.ParentPaths.Select(c => c.ForeignKey).ToList();
            if (parentKeys.Any()) {
                foreach (var foreignKey in parentKeys)
                {
                    sb.AppendLine("ALTER TABLE " + fullTableName + " WITH CHECK ADD CONSTRAINT " + ob + foreignKey.FkName + cb
                        + " FOREIGN KEY(" + ob + foreignKey.ForeignKeyColumn.ColumnName.ToSnakeCase2() + cb + ")");
                    sb.AppendLine("REFERENCES " + ob + foreignKey.PktableOwner + cb + "." + ob + foreignKey.PktableName + cb + " (" + ob + foreignKey.PkcolumnName.ToSnakeCase2() + cb + ")");
                    sb.AppendLine("GO");
                    sb.AppendLine("");
                }

            }


            
            return sb.ToString();

        }


        



    }
}
