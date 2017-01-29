using System.Collections.Generic;
using DatabaseSchemaReader.DataSchema;

namespace CGen.Extensions
{
    public static class ColumnExtensions
    {
        private static readonly HashSet<string> NullableTypes = new HashSet<string>
        {
            "int",
            "short",
            "long",
            "double",
            "decimal",
            "float",
            "bool",
            "DateTime"
        };

        public static string GetFieldName(this DatabaseColumn column,bool normal= true)
        {
            if (column.IsForeignKey && normal == false)
                return "_obj" + column.Name.FirstCharLower();
            return "_" + column.Name.FirstCharLower();
        }

        public static string GetDataType(this DatabaseColumn column, bool normal = true)
        {
            if (column.IsForeignKey && normal == false)
            {
                return column.ForeignKeyTable.Name;
            }
            
            return column.DataType.NetDataTypeCSharpName +
                    ((column.Nullable && NullableTypes.Contains(column.DataType.NetDataTypeCSharpName)) ? "?" : "");
           
        }
    }
}
