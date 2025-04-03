using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerCheckID
    {
        public static bool CheckID(string id, string table, string MaTable)
        {
            bool result = false;
            using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT COUNT(*) FROM " + table + " WHERE " + MaTable + " = @id";
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    object count = cmd.ExecuteScalar();
                    if (count != null && count != DBNull.Value && Convert.ToInt32(count) > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
