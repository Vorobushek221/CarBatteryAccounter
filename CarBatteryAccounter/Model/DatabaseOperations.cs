using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model
{
    public class DatabaseOperations : IDisposable
    {
        public OdbcConnection connection;

        public DatabaseOperations(string connectionString)
        {
            connection = new OdbcConnection(connectionString);
        }

        public DatabaseOperations():
            this(Properties.Settings.Default.myConnectionString)
        {
        }

        public List<Dictionary<string, object>> SelectAll(string tableName)
        {
            string sql = "SELECT * FROM " + tableName;

            connection.Open();
            var comand = new OdbcCommand(sql, connection);
            var dataReader = comand.ExecuteReader();

            var resultList = new List<Dictionary<string, object>>();

            while (dataReader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    row.Add(dataReader.GetName(i), dataReader.GetValue(i));
                }

                resultList.Add(row);
            }
            connection.Close();

            return resultList;
        }


        public void Dispose()
        {
            connection?.Close();
        }
    }
}
