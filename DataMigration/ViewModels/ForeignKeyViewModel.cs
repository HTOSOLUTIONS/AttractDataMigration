using TargetDDContext.Models;


namespace DataMigration.ViewModels
{
    public class ForeignKeyViewModel
    {

        private readonly ForeignKey _foreignKey;

        public ForeignKeyViewModel(ForeignKey foreignKey) { 
        
            _foreignKey = foreignKey;

        }

        public string SqlJoinParent { get {

                string sql = "";
                if (_foreignKey != null)
                {
                    sql = " INNER JOIN " + _fullParentTableName + "  ON " + _fullParentColumnKey + " = " + _fullChildColumnKey;
                }
                return sql;
            } 
        }

        public string SqlJoinChildren
        {
            get
            {

                string sql = "";
                if (_foreignKey != null)
                {
                    sql = " LEFT OUTER JOIN " + _fullChilTabledName + "  ON " + _fullChildColumnKey + " = " + _fullParentColumnKey;
                }
                return sql;
            }
        }


        private string _fullParentTableName => "[" + _foreignKey.PktableOwner + "].[" + _foreignKey.PktableName + "]";
        private string _fullChilTabledName => "[" + _foreignKey.FktableOwner + "].[" + _foreignKey.FktableName + "]";
        private string _fullParentColumnKey => "[" + _foreignKey.PktableName + "].[" + _foreignKey.PkcolumnName + "]";
        private string _fullChildColumnKey => "[" + _foreignKey.FktableName + "].[" + _foreignKey.FkcolumnName + "]";


    }
}
