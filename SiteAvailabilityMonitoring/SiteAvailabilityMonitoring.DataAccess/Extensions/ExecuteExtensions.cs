using System;
using System.Linq;

namespace SiteAvailabilityMonitoring.DataAccess.Extensions
{
    public static class ExecuteExtensions
    {
        public static void CreateEnum<T>(this FluentMigrator.Builders.Execute.IExecuteExpressionRoot execute, string enumName = null) where T : Enum
        {
            var type = typeof(T);
            var enumFormatValues = Enum.GetValues(type).Cast<T>().Select(elem => $"'{elem.ToString()}'");

            var enumValues = string.Join(",", enumFormatValues);
            var enumTableName = enumName ?? type.Name;

            execute.Sql($@"CREATE TYPE {enumTableName} AS ENUM ({enumValues});");
        }
    }
}