using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewAdBA
    {
        public void MenuAdBA()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
    .AddRow("     1", "Xem danh sách các bàn ăn")
    .AddRow("     2", "Cập nhật thông tin bàn ăn")
    .AddRow("     3", "Thêm bàn ăn vào danh sách")
    .AddRow("     4", "Xóa bàn ăn khỏi danh sách")
    .AddRow("     5", "Tìm kiếm thông tin của bàn ăn")
    .AddRow("     0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("----------------QUẢN LÝ BÀN ĂN-----------------".Yellow()) ;
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
                luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1":
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách bàn ăn\n".Green());
                        ControllerView.Print("SELECT * FROM BanAn;");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin bàn ăn\n".Green());
                            ControllerView.Print("SELECT * FROM BanAn;");
                            Console.Write("\tNhập mã bàn ăn cần cập nhật: ".Magenta());
                            string mabanan = Console.ReadLine();
                            if (ControllerCheckID.CheckID(mabanan.ToUpper(), "BanAn", "MaBanAn"))
                            {
                                Console.WriteLine("\t\tMã {0} tồn tại !\n".Green(), mabanan);
                                Console.ReadKey();
                                List<SqlParameter> sqlParameters = ModelBanAn.ThongTinBanAn(mabanan);
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\tKhông có thay đổi mới...\n".Yellow());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE BanAn SET " + query + " WHERE MaBanAn = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!\n".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!\n".Red());
                                }
                            }
                            else Console.WriteLine("Mã {0} không tồn tại !\n".Red(), mabanan);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thêm bàn ăn vào danh sách\n".Green());
                            List<SqlParameter> sqlParameters = ModelBanAn.ThongTinCapNhatBanAn();
                            SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                            string query = queryParameter.Value.ToString();
                            sqlParameters.RemoveAt(sqlParameters.Count - 1);
                            if (ControllerUpdate.CapNhat(query, sqlParameters))
                                Console.WriteLine("\t\t\tThêm thành công\n".Green());
                            else Console.WriteLine("\t\t\tThêm thất bại\n".Red());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa bàn ăn khỏi danh sách\n".Green());
                            if (ModelBanAn.XoaBanAn())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!\n".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!\n".Red());
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin bàn ăn\n".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();

                            string query = "SELECT * FROM BanAn WHERE MaBanAn LIKE '%"
                                + keyword + "%' OR TenBanAn LIKE N'%"
                                + keyword + "%' OR ChuThich LIKE N'%"
                                + keyword + "%' OR TRY_CAST('" + keyword + "' AS BIT) = TrangThai;";
                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\t\tKhông tìm thấy bàn ăn có thông tin '{0}'\n".Red(), keyword);
                            }

                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không?"))
                                Console.WriteLine("\t\tBạn đã chọn quay lại chương trình chính".Green());
                            else
                                luaChon = "";
                            break;
                        }
                    default:
                        Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!\n".Yellow().BlueBg());
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "0");
        }
    }
}