using System.Data.Common;

namespace IDataMigrations.Interfaces
{
    public interface IDDTable : IInformationSchemaTable
    {

        int? RowCount { get; set; }

        int? ColCount { get; set; }

        
        public string? Description { get; set; }


        ICollection<IDDColumn> IDDColumns { get; set; }



    }
}
