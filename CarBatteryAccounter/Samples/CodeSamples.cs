using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;

namespace CarBatteryAccounter.Samples
{
    public static class CodeSamples
    {
        public static void DbConnectionTest()
        {
            OdbcConnection connection = new OdbcConnection("Driver={Microsoft dBASE Driver (*.dbf)};DriverID=277;Dbq=d:\\db;");
            String SQL = "SELECT * FROM Sales.dbf";
            connection.Open();

            OdbcCommand MyCommand = new OdbcCommand(SQL, connection);
            OdbcDataReader dr = MyCommand.ExecuteReader();
            while (dr.Read())
            {
                var a = dr["Cust_id"].ToString();
                var b = dr.GetInt32(1);

            }
        }

    }
}
