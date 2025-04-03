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
    internal class ViewAdKH
    {
        public void MenuAdKH()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
    .AddRow("1", "Xem danh sách khách hàng")
    .AddRow("2", "Cập nhật thông tin khách hàng")
    .AddRow("3", "Xóa khách hàng khỏi danh sách")
    .AddRow("4", "Tìm kiếm thông tin khách hàng")
    .AddRow("0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("--------------QUẢN LÝ KHÁCH HÀNG---------------".Yellow());

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
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách khách hàng.".Green());
                        ControllerView.Print("SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang,CONVERT(varchar, NgayThamGia, 103) as NgayThamGia,DiemTichLuy FROM KhachHang;");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin khách hàng.".Green());
                            ControllerView.Print("SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang," +
                                                                        "CONVERT(varchar, NgayThamGia, 103) as NgayThamGia,DiemTichLuy FROM KhachHang");
                            Console.Write("\tNhập mã khách hàng cần cập nhật: ".Magenta());
                            string makh = Console.ReadLine();
                            if (ControllerCheckID.CheckID(makh.ToUpper(), "KhachHang", "MaKhachHang"))
                            {
                                Console.WriteLine("\t\tMã {0} tồn tại!".Green(), makh);
                                List<SqlParameter> sqlParameters = ModelKH.ThongTinKH(makh.ToUpper());
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\tKhông có thay đổi mới...".Yellow());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE KhachHang SET " + query + " WHERE MaKhachHang = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                                }
                            }
                            else Console.WriteLine("\t\tMã {0} không tồn tại!".Yellow(), makh);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa khách hàng khỏi danh sách.".Green());
                            if (ModelKH.XoaKhachHang())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!".Red());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin khách hàng.".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();

                            string query = "SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang,CONVERT(varchar, NgayThamGia, 103) as NgayThamGia,DiemTichLuy FROM KhachHang WHERE MaKhachHang LIKE '%"
                                + keyword + "%' OR TenKhachHang LIKE N'%"
                                + keyword + "%' OR NgaySinh LIKE '%"
                                + keyword + "%' OR GioiTinh LIKE N'%"
                                + keyword + "%' OR Email LIKE '%"
                                + keyword + "%' OR SoDienThoai LIKE '%"
                                + keyword + "%' OR DiaChi LIKE N'%"
                                + keyword + "%' OR HangKhachHang LIKE N'%"
                                + keyword + "%' OR NgayThamGia LIKE '%"
                                + keyword + "%' OR DiemTichLuy LIKE '%"
                                + keyword + "%';";
                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\t\tKhông tìm thấy khách hàng có thông tin '{0}'".Red(), keyword);
                            }
                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không?"))
                                Console.WriteLine("\t\tBạn đã chọn quay lại chương trình".Green());
                            else
                                luaChon = "";
                            break;
                        }
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại!");
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "0");

        }
    }
}
