using System.Data;
using System.Reflection;

namespace LOLServerStatistics.Server.Application.Helpers
{
    public class DBHelpers
    {
        public static DataTable ConvertModelToDataTable<T>(T model) where T : class
        {
            DataTable convertedTable = new();
            PropertyInfo[] propertiesInfo = typeof(T).GetProperties(BindingFlags.Public);
            foreach (var property in propertiesInfo)
            {
                convertedTable.Columns.Add(property.Name);
            }
            var row = convertedTable.NewRow();
            _ = new object[propertiesInfo.Length];
            for (int i = 0; i < propertiesInfo.Length; i++)
            {
                row[i] = propertiesInfo[i].GetValue(model, null);
            }
            convertedTable.Rows.Add(row);
            return convertedTable;
        }

        public static DataTable ConvertListModelsToDataTable<T>(List<T> model) where T : class
        {
            DataTable convertedTable = new();

            if (model.Any())
            {
                PropertyInfo[] propertiesInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var property in propertiesInfo)
                {
                    convertedTable.Columns.Add(property.Name);
                }

                foreach (T item in model)
                {
                    var row = convertedTable.NewRow();
                    _ = new object[propertiesInfo.Length];

                    for (int i = 0; i < propertiesInfo.Length; i++)
                    {
                        row[i] = propertiesInfo[i].GetValue(item, null);
                    }

                    convertedTable.Rows.Add(row);
                }
            }

            return convertedTable;
        }
    }
}
