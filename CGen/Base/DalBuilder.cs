using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using CGen.Extensions;
using CGen.Models;
using DatabaseSchemaReader.DataSchema;

namespace CGen.Base
{
    public class DalBuilder
    {
        private Templates _templates;
        private Template _dalTemplate;

        public DalBuilder(Templates templates,NGenSettings settings)
        {
            _templates = templates;
            _dalTemplate = templates["DAL"];
        }

        //public DalBuilder BOs(List<DatabaseTable> tables)
        //{
        //    var boclassesBuilder = new StringBuilder("");
        //    var boTemplate = _templates["BO"];
        //    var fieldTemplate = _templates["FIELD"];
        //    var propertyTemplate = _templates["PROPERTY"];

        //    foreach (var table in tables)
        //    {
        //        var fieldsBuilder = new StringBuilder("");
        //        var propertiesBuilde = new StringBuilder("");

        //        foreach (var column in table.Columns)
        //        {
        //            if (column.IsForeignKey)
        //            {
        //                var dataType = column.ForeignKeyTableName + "Bo";
        //                var fieldName = "_obj" + column.Name.FirstCharLower();

        //                fieldTemplate
        //                    .Set("dataType", dataType)
        //                    .Set("name", fieldName);

        //                propertyTemplate.Set("dataType", dataType)
        //                    .Set("name", "Obj" + column.Name)
        //                    .Set("fieldName",fieldName)
        //                    .Set("primary", column.IsPrimaryKey ? "PrimaryKey = value;" : "");

        //                fieldsBuilder.AppendLine(fieldTemplate.Build());
        //                propertiesBuilde.AppendLine(propertyTemplate.Build());

        //                fieldTemplate = _templates["FIELD"];
        //                propertyTemplate = _templates["PROPERTY"];

        //                fieldName = "_" + column.Name.FirstCharLower();

        //                fieldTemplate
        //                    .Set("dataType", dataType)
        //                    .Set("name", fieldName);

        //                propertyTemplate.Set("dataType", dataType)
        //                    .Set("name", column.Name)
        //                    .Set("fieldName", fieldName)
        //                    .Set("primary", column.IsPrimaryKey ? "PrimaryKey = value;" : "");

        //                fieldsBuilder.AppendLine(fieldTemplate.Build());
        //                propertiesBuilde.AppendLine(propertyTemplate.Build());
        //            }
        //            else
        //            {
        //                var fieldName = "_" + column.Name.FirstCharLower();
        //                fieldTemplate
        //                   .Set("dataType", column.DataType.NetDataTypeCSharpName)
        //                   .Set("name", "_"+column.Name.FirstCharLower());

        //                propertyTemplate.Set("dataType", column.DataType.NetDataTypeCSharpName)
        //                    .Set("name", "Obj" + column.Name)
        //                    .Set("fieldName", fieldName)
        //                    .Set("primary", column.IsPrimaryKey ? "PrimaryKey = value;" : "");
        //            }
                        

        //            fieldTemplate.Set("name", "_"+column.Name.FirstCharLower());
        //            fieldsBuilder.AppendLine(fieldTemplate.Build());
        //        }
        //    }

        //}
    }
}
