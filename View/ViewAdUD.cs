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
    internal class ViewAdUD
    {
        public void MenuAdUD()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
     .AddRow("1", "Xem danh sách các ưu đãi")
     .AddRow("2", "Cập nhật thông tin các ưu đãi")
     .AddRow("3", "Thêm ưu đãi vào danh sách")
     .AddRow("4", "Xóa ưu đãi ra khỏi danh sách")
     .AddRow("5", "Tìm kiếm thông tin ưu đãi")
     .AddRow("0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("---------------QUẢN LÝ ƯU ĐÃI------------------".Yellow());

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
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách ưu đãi".Green());
                        ControllerView.Print("SELECT MaUuDai,TenUuDai,ThoiGianBatDau,ThoiGianKetThuc,GiaTri,DonViTinh,DieuKien,DiemTichLuy,CachThucDangKy FROM UuDai;");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin ưu đãi".Green());
                            ControllerView.Print("SELECT MaUuDai,TenUuDai,ThoiGianBatDau,ThoiGianKetThuc,GiaTri,DonViTinh,DieuKien,DiemTichLuy,CachThucDangKy FROM UuDai");
                            Console.Write("\tNhập mã ưu đãi cần cập nhật: ".Magenta());
                            string maUD = Console.ReadLine();
                            if (ControllerCheckID.CheckID(maUD.ToUpper(), "UuDai", "MaUuDai"))
                            {
                                Console.WriteLine("\t\tMã {0} tồn tại!".Green(), maUD);
                                Console.ReadKey();
                                List<SqlParameter> sqlParameters = ModelUD.ThongTinUuDai(maUD);
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\tKhông có thay đổi mới...".Yellow());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE UuDai SET " + query + " WHERE MaUuDai = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                                }
                            }
                            else Console.WriteLine("\t\t\tMã {0} không tồn tại!".Red(), maUD);
                        }
                        break;
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thêm ưu đãi vào danh sách".Green());
                            List<SqlParameter> sqlParameters = ModelUD.ThongTinCapNhatUuDai();
                            SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                            string query = queryParameter.Value.ToString();
                            sqlParameters.RemoveAt(sqlParameters.Count - 1);
                            if (ControllerUpdate.CapNhat(query, sqlParameters))
                                Console.WriteLine("\t\t\tThêm thành công!".Green());
                            else Console.WriteLine("\t\t\tThêm thất bại!".Red());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa ưu đãi ra khỏi danh sách".Green());
                            if (ModelUD.XoaUuDai())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!".Red());
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin ưu đãi".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();
                            string query = "SELECT * FROM UuDai WHERE MaUuDai LIKE '%"
                                + keyword + "%' OR TenUuDai LIKE N'%"
                                + keyword + "%' OR ThoiGianBatDau LIKE '%"
                                + keyword + "%' OR ThoiGianKetThuc LIKE '%"
                                + keyword + "%' OR GiaTri LIKE '%"
                                + keyword + "%' OR DonViTinh LIKE N'%"
                                + keyword + "%' OR MoTa LIKE N'%"
                                + keyword + "%' OR DieuKien LIKE '%"
                                + keyword + "%' OR DiemTichLuy LIKE '%"
                                + keyword + "%' OR CachThucDangKy LIKE N'%"
                                + keyword + "%';";
                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\t\tKhông tìm thấy ưu đãi có thông tin '{0}'".Red(), keyword);
                            }
                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không ?"))
                                Console.WriteLine("\t\tBạn đã chọn quay lại chương trình".Green());
                            else
                                luaChon = "";
                            break;
                        }
                    default:
                        Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!".Yellow().BlueBg());
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "0");
        }
    }
}
