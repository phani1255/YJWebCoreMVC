using System.Data;

namespace YJWebCoreMVC.Helpers
{
    public static class DataTableExtensions
    {
        public static object ToJsonData(this DataTable table)
        {
            return table.AsEnumerable()
                .Select(row => table.Columns.Cast<DataColumn>()
                .ToDictionary(
                    col => col.ColumnName,
                    col => row[col]))
                .ToList();
        }

        public static object ToJsonData(this DataSet ds)
        {
            var result = new Dictionary<string, object>();

            foreach (DataTable table in ds.Tables)
            {
                string tableName =
                    string.IsNullOrEmpty(table.TableName)
                        ? $"Table{ds.Tables.IndexOf(table)}"
                        : table.TableName;

                result[tableName] = table.ToJsonData();
            }
            return result;
        }
    }
}
