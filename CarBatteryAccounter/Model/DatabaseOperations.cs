using CarBatteryAccounter.Model.Entities;
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

        private string tableName;

        public DatabaseOperations(string connectionString, string tableName)
        {
            connection = new OdbcConnection(connectionString);
            this.tableName = tableName;
        }

        public DatabaseOperations(string tableName) :
            this(Properties.Settings.Default.myConnectionString ,tableName)
        {
            this.tableName = tableName;
        }

        public List<Dictionary<string, object>> GetQuery(string sql)
        {
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

        public List<Dictionary<string, object>> GetDistinctCarsQuery()
        {
            return GetQuery("SELECT distinct gnaid, gn, Nm, Llist FROM " + tableName);
        }

        public List<Dictionary<string, object>> GetBattariesQuery()
        {
            return GetQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM " + tableName);
        }

        public void Dispose()
        {
            connection?.Close();
        }
    }
}
