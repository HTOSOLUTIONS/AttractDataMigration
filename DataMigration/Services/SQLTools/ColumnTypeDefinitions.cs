using DataMigration.Services.HTOTools;
using IDataMigrations.Interfaces;
using TargetDDContext.Models;

namespace SQLTools
{
    public static class ColumnTypeDefinitions
    {

        public static string CreateType(this string text)
        {

            return text;

        }

        public static string CreateLine(this Column column, Table table)
        {
            string text = "[" + column.ColumnName.PascalToSnake() + "] "
                + "[" + column.DataType + "]";


            var key = table.PrimaryKeys.Where(k => k.ColumnName == column.ColumnName && k.IsIdentity == true).FirstOrDefault();

            if (key != null)
            {
                text = text + " IDENTITY(1,1)";
            }

            if (column.CharacterMaximumLength > 0)
            {
                text = text + "(" + column.CharacterMaximumLength + ")";
            }
            if (column.IsNullable == "NO")
            {
                text = text + " NOT";
            }
            text = text + " NULL,";

            return text;

        }

        public static string InsertIntoLine(this Column column, Table table, string? delim = null)
        {
            string text = delim + "[" + column.ColumnName.PascalToSnake() + "] ";

            return text;

        }
        public static string InsertFromLine(this Column column, Table table, string? delim = null)
        {
            string text = delim + "[" + column.ColumnName + "] ";

            return text;

        }


    }
}
