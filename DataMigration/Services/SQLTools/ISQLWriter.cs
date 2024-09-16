using SQLTools;
using TargetDDContext.Models;

namespace SQLTools
{
    public interface ISQLWriter
    {

        string CreateTable(Table table, WriteParms? writeParms = null);

        string InsertIntoTable(Table table, WriteParms? writeParms = null);


    }
}
