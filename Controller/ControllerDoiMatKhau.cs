using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerDoiMatKhau
    {
        public static bool DoiMatKhau(string id, string table, string maTable)
        {
            Console.Clear();
            string pw, repw;
            int dem = 3;
            bool ok;
            do
            {
                Console.Write("\tNhập mật khẩu cũ: ".Cyan());
                pw = ControllerMaHoaPW.ReadPassword();
                ok = ControllerLogin.PW(id, pw, table, maTable);
                if (!ok)
                {
                    Console.WriteLine("\t\tMật khẩu sai... Vui lòng nhập lại...".Red());
                    Console.WriteLine("\t\t\tBạn còn {0} lần nhập", dem);
                    if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không ?"))
                        return false;
                    dem--;
                    if (dem == 0)
                        return false;
                }
            } while (!ok);
            dem = 3;
            do
            {
                Console.Write("\tNhập mật khẩu mới: ".Green());
                pw = ControllerMaHoaPW.ReadPassword();
                Console.Write("\tNhập lại mật khẩu mới: ".Green());
                repw = ControllerMaHoaPW.ReadPassword();
                if (pw == repw)
                {
                    string query = "UPDATE " + table + " SET MatKhau = '" + BCrypt.Net.BCrypt.HashPassword(pw, workFactor: 7) + "' WHERE " + maTable + " = '" + id + "';";
                    if (ControllerExcution.Excution(query))
                        return true;
                    else
                        return false;
                }
                else
                {
                    Console.WriteLine("\t\tNhập sai mật khẩu... Vui lòng nhập lại...".Yellow());
                    Console.WriteLine("\t\t\tBạn còn {0} lần nhập", dem);
                    dem--;
                    if (dem == 0)
                        return false;
                }
            } while (pw != repw);
            return false;
        }
    }
}
