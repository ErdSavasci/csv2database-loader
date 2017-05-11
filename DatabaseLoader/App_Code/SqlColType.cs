using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLoader.App_Code
{
    class SqlColType
    {
        public static Type GetSqlColumnType(string colType)
        {
            switch (colType.ToLower())
            {
                case "bigint":
                    return typeof(long?);

                case "binary":
                case "image":
                case "timestamp":
                case "varbinary":
                    return typeof(byte[]);

                case "bit":
                    return typeof(bool?);

                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    return typeof(string);

                case "datetime":
                case "smalldatetime":
                case "date":
                case "time":
                case "datetime2":
                    return typeof(DateTime?);

                case "decimal":
                case "money":
                case "smallmoney":
                    return typeof(decimal?);

                case "float":
                    return typeof(double?);

                case "int":
                    return typeof(int?);

                case "real":
                    return typeof(float?);

                case "uniqueidentifier":
                    return typeof(Guid?);

                case "smallint":
                    return typeof(short?);

                case "tinyint":
                    return typeof(byte?);

                case "variant":
                case "udt":
                    return typeof(object);

                case "structured":
                    return typeof(DataTable);

                case "datetimeoffset":
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        } 
    }
}
