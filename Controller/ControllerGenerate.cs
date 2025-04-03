using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerGenerate
    {
        public static string Generate(string ma,string table,string maTable)
        {
            string prefix = ma;
            int suffixLength = 3;
            string suffix = "";
            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT MAX(RIGHT("+maTable+", 3)) FROM "+table+" WHERE LEFT("+maTable+", 2) = '"+ma+"'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        int maxSuffix = Convert.ToInt32(result);
                        suffix = (maxSuffix + 1).ToString().PadLeft(suffixLength, '0');
                    }
                    else
                    {
                        suffix = "001";
                    }
                }
            }
            return prefix + suffix;
        }
    }
}
