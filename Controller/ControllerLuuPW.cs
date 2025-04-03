using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerLuuPW
    {
        public static void LuuMatKhau(string id, string table,string maTable)
        {
            string query = "UPDATE "+table+" SET LuuMatKhau = 1 WHERE "+maTable+" = '"+id+"';";
            ControllerExcution.Excution(query);
        }
        public static void DangXuat(string id, string table,string maTable)
        {
            string query = "UPDATE " + table + " SET LuuMatKhau = 0 WHERE " + maTable + " = '" + id + "';";
            ControllerExcution.Excution(query);
            Console.WriteLine("\t\t\tBạn đã đăng xuất thành công!".Green());
        }
    }
}
