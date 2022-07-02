using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Diagnostics;

namespace _17
{
    internal static class Connections
    {
        public static string connectionString1 =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=base1;Integrated Security=True;Pooling=False";
        public static SqlConnection sqlConnection;

        public static string connectionString2 =
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Stqslav\source\repos\10\17\bin\Debug\net6.0-windows\Database1.accdb";
        public static OleDbConnection oleDbConnection;

        public static void Activate()
        {
            sqlConnection = new SqlConnection(connectionString1);
            oleDbConnection = new OleDbConnection(connectionString2);
        }
        
        public static void Connect()
        {
            try
            {
                sqlConnection.Open();
                oleDbConnection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static void Disconnect()
        {
            sqlConnection.Close();
            oleDbConnection.Close();
        }
    }
}
