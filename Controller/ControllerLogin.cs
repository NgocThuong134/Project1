using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BCrypt.Net;
namespace Project.Controller
{
    internal class ControllerLogin
    {
        public static bool IsLogin(string username, string password, string table)
        {
            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT MatKhau FROM " + table + " WHERE TenDangNhap = @username;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    object passwordHash = command.ExecuteScalar();
                    if (passwordHash != null && passwordHash != DBNull.Value)
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, (string)passwordHash))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool PW(string id, string password, string table,string maTable)
        {
            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT MatKhau FROM " + table + " WHERE "+maTable+" = @id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    object passwordHash = command.ExecuteScalar();
                    if (passwordHash != null && passwordHash != DBNull.Value)
                    {
                        if (BCrypt.Net.BCrypt.Verify(password, (string)passwordHash))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
