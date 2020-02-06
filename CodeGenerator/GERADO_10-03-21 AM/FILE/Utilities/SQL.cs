using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for SQL
/// </summary>
namespace Utilities
{
    public static class SQL
    {
        public static string[] GetColumnNames(string[] columnNames)
        {
            return Utilities.SQL.GetColumnNames(columnNames, null);
        }

        public static string[] GetColumnNames(string[] columnNames, string alias)
        {
            return Utilities.SQL.GetColumnNames(columnNames, alias, null);
        }

        public static string[] GetColumnNames(string[] columnNames, string alias, string prefix)
        {
            string[] newColumnNames = new string[columnNames.Length];

            for (int i = 0; i < columnNames.Length; i++)
            {
                newColumnNames[i] = Utilities.SQL.GetColumnName(columnNames[i], alias, prefix);
            }

            return newColumnNames;
        }

        public static string GetColumnName(string columnName, string alias, string prefix)
        {
            StringBuilder newColumnName = new StringBuilder();

            if (!String.IsNullOrEmpty(alias))
            {
                newColumnName.AppendFormat("[{0}].", alias);
            }

            newColumnName.AppendFormat("[{0}]", columnName);

            if (!String.IsNullOrEmpty(prefix))
            {
                newColumnName.AppendFormat(" AS [{0}{1}]", prefix, columnName);
            }

            return newColumnName.ToString();
        }
    }
}