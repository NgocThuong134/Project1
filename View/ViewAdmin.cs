using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project.View
{
    internal class ViewAdmin
    {

        public static void MenuAd()
        {
            string luaChon;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("        Chức năng", "Mã lựa chọn")
    .AddRow("Quản lý nhân viên", "     A")
    .AddRow("Quản lý khách hàng", "     B")
    .AddRow("Quản lý món ăn và thức uống", "     C")
    .AddRow("Quản lý bàn ăn", "     D")
    .AddRow("Quản lý ưu đãi", "     E")
    .AddRow("Quản lý hóa đơn", "     F")
    .AddRow("Xem thống kê doanh thu", "     G")
    .AddRow("Đổi mật khẩu", "     H")
    .AddRow("Đăng xuất", "     I");

                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("-------------------QUẢN LÝ-------------------".RedBg());

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
                Console.Write("Mời nhập lựa chọn của bạn: ".Magenta());
                luaChon = Console.ReadLine().ToUpper();

                switch (luaChon)
                {
                    case "A":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý nhân viên.".Green());
                        ViewAdNV viewAdNV = new ViewAdNV();
                        viewAdNV.MenuAdNV();
                        break;
                    case "B":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý khách hàng.".Green());
                        ViewAdKH viewAdKH = new ViewAdKH();
                        viewAdKH.MenuAdKH();
                        break;
                    case "C":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý món ăn và thức uống.".Green());
                        ViewAdFood viewAdFood = new ViewAdFood();
                        viewAdFood.MenuAdFood();
                        break;
                    case "D":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý bàn ăn.".Green());
                        ViewAdBA viewAdBA = new ViewAdBA();
                        viewAdBA.MenuAdBA();
                        break;
                    case "E":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý ưu đãi.".Green());
                        ViewAdUD viewAdUD = new ViewAdUD();
                        viewAdUD.MenuAdUD();
                        break;
                    case "F":
                        Console.WriteLine("\t\tBạn đã chọn chức năng quản lý hóa đơn.".Green());
                        ViewAdHD viewAdHD = new ViewAdHD();
                        viewAdHD.MenuAdHD();
                        break;
                    case "G":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xem thống kê doanh thu.".Green());
                            ViewThongKe viewThongKe = new ViewThongKe();
                            viewThongKe.MenuThongKe();
                            break;
                        }
                    case "H":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng đổi mật khẩu.".Green());
                            if (ControllerDoiMatKhau.DoiMatKhau("NV000", "NhanVien", "MaNhanVien"))
                            {
                                Console.WriteLine("\t\tĐổi mật khẩu thành công!".Green());
                            }
                            else Console.WriteLine("\t\tĐổi mật khẩu thất bại!".Red());
                            break;
                        }
                    case "I":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn đăng xuất không?"))
                            {
                                ControllerLuuPW.DangXuat("NV000", "NhanVien", "MaNhanVien");
                                Console.WriteLine("\t\tHẹn gặp lại... Xin chào".Cyan());
                            }
                            else luaChon = "";
                            break;
                        }
                    default:
                        Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!".Yellow().BlueBg());
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "I");
        }
    }
}