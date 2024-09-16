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

            var colsToUse = table.Columns.Where(c => c.NeedsMigration == true);

            foreach (var column in colsToUse.OrderBy(c => c.OrdinalPosition)) { 
                sb.AppendLine(indenter + column.CreateLine(table));
            }
            if (table.PrimaryKeys.Any())
            {
                sb.AppendLine(" CONSTRAINT " + ob + table.PrimaryKeys.First().ConstraintName + cb + " PRIMARY KEY CLUSTERED ");
                sb.AppendLine("(");
                var keys = table.PrimaryKeys.Select(k => "[" + k.ColumnName.PascalToSnake() + "] ASC").ToList();
                sb.AppendLine(indenter + string.Join(", ", keys));
                sb.AppendLine(") WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]");
                sb.AppendLine(") ON [PRIMARY]");
            }
            sb.AppendLine("GO");
            sb.AppendLine("");
            var defltConstraints = colsToUse.SelectMany(c => c.DefaultConstraints).ToList();
            if (defltConstraints.Any())
            {
                foreach (var constraint in defltConstraints)
                {
                    sb.AppendLine("ALTER TABLE " + fullTableName + " ADD CONSTRAINT " + ob + constraint.ConstraintName + cb + " DEFAULT " + constraint.Definition
                            + " FOR " + ob + constraint.ColumnName.PascalToSnake() + cb
                        );
                    sb.AppendLine("GO");
                    sb.AppendLine("");
                }
            }

            var fkColNames = colsToUse.Select(c => c.ColumnName).ToList();
            var parentKeys = table.ParentPaths.Select(c => c.ForeignKey)
                .ToList();
            if (parentKeys.Any()) {
                foreach (var foreignKey in parentKeys.Where(fk => fkColNames.Contains(fk.ForeignKeyColumn.ColumnName)))
                {
                    sb.AppendLine("ALTER TABLE " + fullTableName + " WITH CHECK ADD CONSTRAINT " + ob + foreignKey.FkName + cb
                        + " FOREIGN KEY(" + ob + foreignKey.ForeignKeyColumn.ColumnName.PascalToSnake() + cb + ")");
                    sb.AppendLine("REFERENCES " + ob + foreignKey.PktableOwner + cb + "." + ob + foreignKey.PktableName + cb + " (" + ob + foreignKey.PkcolumnName.PascalToSnake() + cb + ")");
                    sb.AppendLine("GO");
                    sb.AppendLine("");
                }

            }


            
            return sb.ToString();

        }

        public string InsertIntoTable(Table table, WriteParms? writeParms = null)
        {
            WriteParms _writeParms = writeParms ?? new WriteParms();

            var indenter = "     ";
            var ob = _writeParms.UseBrackets ? "[" : "";
            var cb = _writeParms.UseBrackets ? "]" : "";
            var sb = new StringBuilder();
            var fullTableName = ob + table.TableSchema + cb + "." + ob + table.TableName + cb;

            sb.AppendLine("SET IDENTITY_INSERT " + fullTableName + " ON");
            sb.AppendLine("");

            sb.AppendLine("INSERT INTO " + fullTableName) ;
            sb.AppendLine(indenter + "(");
            var colsToUse = table.Columns.Where(c => c.NeedsMigration == true);

            var firstLine = true;
            foreach (var column in colsToUse.OrderBy(c => c.OrdinalPosition))
            {
                string? delim = null;
                if (firstLine)
                {
                    delim = null;
                    firstLine = false;
                } else
                {
                   delim = ",";
                };
                sb.AppendLine(indenter + column.InsertIntoLine(table, delim));
            }
            sb.Append(")");
            sb.Append("SELECT ");
            firstLine = true;
            foreach (var column in colsToUse.OrderBy(c => c.OrdinalPosition))
            {
                string? delim = null;
                if (firstLine)
                {
                    delim = null;
                    firstLine = false;
                }
                else
                {
                    delim = ",";
                };
                sb.AppendLine(indenter + column.InsertFromLine(table, delim));
            }
            sb.Append("FROM [greenfield_dvt_loaded_20240703184206]." + fullTableName);

            sb.AppendLine("");
            sb.AppendLine("SET IDENTITY_INSERT " + fullTableName + " OFF");

            return sb.ToString();

        }





    }
}
