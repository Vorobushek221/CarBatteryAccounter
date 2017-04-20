using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Model
{
    public class OleDatabaseOperations : IDisposable
    {
        OleDbConnection connection;

        public OleDatabaseOperations(string connectionString)
        {
            connection = new OleDbConnection(connectionString);
        }

        public OleDatabaseOperations() :
            this(Properties.Settings.Default.myConnectionString2)
        {
        }

        public List<Dictionary<string, object>> GetQuery(string sql)
        {
            connection.Open();

            var comand = new OleDbCommand(sql, connection);

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
            var comand = new OleDbCommand(sql, connection);
            comand.ExecuteNonQuery();
            connection.Close();
        }

        public List<Dictionary<string, object>> GetDistinctCarsQuery()
        {
            //return GetQuery("SELECT * FROM spr_pds_");
            //return GetQuery("SELECT distinct gnaid, gn, Nm, Llist FROM " + tableName);
            return GetQuery("SELECT DISTINCT gnaid, gn, spr_pds.Nm, Llist FROM spr_pds JOIN akkum ON spr_pds.Gn = Akkum.gnom");
        }

        public List<Dictionary<string, object>> GetBattariesQuery()
        {
            return GetQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM spr_pds JOIN akkum ON spr_pds.Gn = Akkum.gnom");
            //return GetQuery("SELECT distinct gnaid, Llist, Tipa, Nomak, Nnomak, Datpol, Datspis FROM " + tableName);
        }

        public void Dispose()
        {
            connection?.Close();
        }
    }
}
