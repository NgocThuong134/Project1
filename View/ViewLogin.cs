using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using Project.Model;

namespace Project.View
{
    internal class ViewLogin
    {

        public void Login()
        {
            string option;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("Chức năng", "Mã lựa chọn")
                                            .AddRow("Đăng nhập", "     1")
                                            .AddRow("Đăng ký", "     2")
                                            .AddRow("Xem thực đơn","     3")
                                            .AddRow("Thoát", "     E");
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("----------TRANG CHỦ-----------".Red());

                table.Options.EnableCount = false;
                foreach (var row in table.ToMarkDownString().Split(Environment.NewLine))
                {
                    Console.SetCursorPosition(leftMargin, Console.CursorTop);
                    if (row.StartsWith("|"))
                    {
                        Console.Write(row.Substring(0, 1).Cyan());
                        Console.Write(row.Substring(1, row.Length - 2).Green());
                        Console.WriteLine(row.Substring(row.Length - 1, 1).Cyan());
                    }
                    else if (!string.IsNullOrEmpty(row))
                    {
                        Console.WriteLine(row.White());
                    }
                }
                Console.Write("Mời chọn: ".Magenta());
                option = Console.ReadLine().ToUpper();
                switch (option)
                {
                    case "1":
                        {
                            ModelDangNhap.DangNhap();
                            break;
                        }
                    case "2":
                        {
                            ModelDangKy.DangKy();
                            break;
                        }
                        case "3":
                        {
                            ModelFood.XemFooD();
                            break;
                        }
                    case "E":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn thoát không?"))
                            {
                                Console.WriteLine("\t\tHẹn gặp lại... Xin chào".Green().Yellow());
                            }
                            else option = "";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng nhập lại!".Yellow().BlueBg());
                            break;
                        }
                }
                Console.ReadKey();
            } while (option != "E");
        }
    }
}
