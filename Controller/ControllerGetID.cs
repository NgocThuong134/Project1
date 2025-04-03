using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerGetID
    {
        public static string GetID(string name, string table, string maTable)
        {
            using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT "+maTable+" FROM " + table + " WHERE TenDangNhap = @name";
                SqlCommand cmd = new SqlCommand(query, sql);
                cmd.Parameters.AddWithValue("@name", name);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader.GetString(0);
                    return id;
                }
                return null;
            }
        }
    }
}
