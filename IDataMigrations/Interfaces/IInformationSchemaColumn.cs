namespace IDataMigrations.Interfaces
{
    public interface IInformationSchemaColumn
    {

         string TableCatalog { get; set; }

         string TableSchema { get; set; }

         string TableName { get; set; }

         string ColumnName { get; set; }

         int? OrdinalPosition { get; set; }

         string? ColumnDefault { get; set; }

         string? IsNullable { get; set; }

         string? DataType { get; set; }

         int? CharacterMaximumLength { get; set; }

         int? CharacterOctetLength { get; set; }

         byte? NumericPrecision { get; set; }

         short? NumericPrecisionRadix { get; set; }

         int? NumericScale { get; set; }

         short? DatetimePrecision { get; set; }

         string? CharacterSetCatalog { get; set; }

         string? CharacterSetSchema { get; set; }

         string? CharacterSetName { get; set; }

         string? CollationCatalog { get; set; }

         string? CollationSchema { get; set; }

         string? CollationName { get; set; }

         string? DomainCatalog { get; set; }

         string? DomainSchema { get; set; }

         string? DomainName { get; set; }



    }
}
