using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using Project.Model;
namespace Project.Controller
{
    internal class ControllerView
    {
        public static bool Print(string query)
        {
            try
            {
                query = query.Trim();
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, sql);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        ModelPrint.PrintData(dr);
                    }
                    else
                    {
                        Console.WriteLine("\t\t\tNULL");
                        return false;
                    } 
                    dr.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
