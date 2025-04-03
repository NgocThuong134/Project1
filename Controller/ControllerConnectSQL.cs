using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Project.Controller
{
    internal class ControllerConnectSQL
    {
        public static SqlConnection GetSqlConnection()
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Project_1;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sqlConnection;
        }
        public static void CloseSqlConnection(SqlConnection sqlConnection)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }
    }
}
