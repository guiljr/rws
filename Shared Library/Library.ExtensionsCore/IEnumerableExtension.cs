using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Library.ExtensionsCore
{
    public static class IEnumerableExtension
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
              TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable ToKVPDataTable<T>(this T data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            table.Columns.Add("Key");
            table.Columns.Add("Value");
            foreach (PropertyDescriptor prop in properties)
            {
                DataRow row = table.NewRow();
                row["Key"] = prop.Name;
                row["Value"] = prop.GetValue(data) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            //table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            //foreach (T item in data)
            //{

            //    foreach (PropertyDescriptor prop in properties)
            //        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            //    table.Rows.Add(row);
            //}
            return table;
        }
    }
}