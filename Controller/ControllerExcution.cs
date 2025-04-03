using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerExcution
    {
        public static bool Excution(string query)
        {
            try
            {
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            return true;
                        }
                        else if (cmd.ExecuteNonQuery() == 0 )
                            return false;
                        else
                            if ((int)cmd.ExecuteScalar() > 0)
                            return true;
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static int ExcutionInt(string query)
        {
            try
            {
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, sql);
                    int diemTichLuy = (int)cmd.ExecuteScalar();
                    return diemTichLuy;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine (e.Message);
                return -1;
            }
        }
        public static string ExcutionString(string query)
        {
            try
            {
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, sql);
                    object result = cmd.ExecuteScalar();
                    string xau = null;
                    if (result != null && !Convert.IsDBNull(result))
                    {
                        xau = (string)result;
                    }
                    return xau;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public static float ExcutionFloat(string query)
        {
            try
            {
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, sql);
                    object result = cmd.ExecuteScalar();
                    float value = 0;
                    if (result != null && result != DBNull.Value)
                    {
                        float.TryParse(result.ToString(), out value);
                    }
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
