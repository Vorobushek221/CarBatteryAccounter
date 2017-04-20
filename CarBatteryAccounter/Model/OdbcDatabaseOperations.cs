using CarBatteryAccounter.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model
{
    public class OdbcDatabaseOperations : IDisposable
    {
        public OdbcConnection connection;

        private string tableName;

        public OdbcDatabaseOperations(string connectionString, string tableName)
        {
            connection = new OdbcConnection(connectionString);
            this.tableName = tableName;
        }

        public OdbcDatabaseOperations(string tableName) :
            this(Properties.Settings.Default.myConnectionString2, tableName)
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

        private void ExecuteSimpleQuery(string sql)
        {
            connection.Open();
            var comand = new OdbcCommand(sql, connection);
            comand.ExecuteNonQuery();
            connection.Close();
        }

        public List<Dictionary<string, object>> GetDistinctCarsQuery()
        {
            return GetQuery("SELECT * FROM spr_pds");
            //return GetQuery("SELECT distinct gnaid, gn, Nm, Llist FROM " + tableName);
            return GetQuery("SELECT DISTINCT gnaid, gn, spr_pds_.Nm, Llist FROM spr_pds_ LEFT JOIN akkum ON spr_pds_.Gn = Akkum.gnom");
        }

        public List<Dictionary<string, object>> GetBattariesQuery()
        {
            return GetQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM spr_pds_ RIGHT JOIN akkum ON spr_pds_.Gn = Akkum.gnom");
            //return GetQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM " + tableName);
        }

        public void UpdateSourceTable()
        {
            //ExecuteSimpleQuery("SELECT * FROM spr_pds.dbf");
            //ExecuteSimpleQuery(@"SELECT gnaid, Gn, spr_pds_.nm, Llist, Nomak, Nnomak, Tipa, Datpol, Datspis FROM spr_pds_ INNER JOIN akkum_ ON spr_pds_.Gn = Akkum_.gnom COPY TO acc_ FOX2X as 866");
            //ExecuteSimpleQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM " + tableName);

            //ExecuteSimpleQuery("COPY TO acc_ FOX2X as 866");

            


        }

        public void Dispose()
        {
            connection?.Close();
        }
    }
}
