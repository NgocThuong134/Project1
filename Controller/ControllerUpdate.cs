using Project.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerUpdate
    {
        public static bool CapNhat(string query, List<SqlParameter> sqlParameters)
        {

            try
            {
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        foreach (SqlParameter p in sqlParameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                        
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            return true;
                        }
                        else return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
