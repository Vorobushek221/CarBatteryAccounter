using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Samples
{
    public static class CodeSamples
    {
        public static void DbConnectionTest()
        {
            OdbcConnection connection = new OdbcConnection("Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=d:\\db;");
            //OdbcConnection connection = new OdbcConnection("Provider = Microsoft.ACE.OLEDB.12.0; extended properties =\"excel 8.0;hdr=no;IMEX=1\";data source=");

            
            String SQL = "SELECT * FROM spr_pds_.DBF";
            connection.Open();

            OdbcCommand MyCommand = new OdbcCommand(SQL, connection);
            OdbcDataReader dr = MyCommand.ExecuteReader();
            while (dr.Read())
            {
                var a = dr["Cust_id"].ToString();
                var b = dr.GetInt32(1);

            }
        }

        public static void TestOledbConnection()
        {
            OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.myConnectionString2);

            connection.Open();

            var sql = "SELECT * FROM akkum where Gnom = 'AP1122-7'";

            var command = new OleDbCommand(sql);

            command.Connection = connection;
            command.CommandText = sql;
            var dr = command.ExecuteReader();
            while (dr.Read())
            {
                var a = dr["Cust_id"].ToString();
                var b = dr.GetInt32(1);

            }
            connection.Close();

        }

    }
}
