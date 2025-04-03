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
    internal class ViewAdNV
    {
        public void MenuAdNV()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
     .AddRow("1", "Xem danh sách nhân viên")
     .AddRow("2", "Cập nhật thông tin nhân viên")
     .AddRow("3", "Thêm nhân viên vào danh sách")
     .AddRow("4", "Xóa nhân viên khỏi danh sách")
     .AddRow("5", "Tìm kiếm thông tin nhân viên")
     .AddRow("6", "Tính lương nhân viên")
     .AddRow("0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("---------------QUẢN LÝ NHÂN VIÊN--------------".Yellow());

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
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách nhân viên".Green());
                        ControllerView.Print("SELECT MaNhanVien, TenNhanVien, CONVERT(varchar, NgaySinh, 103) as NgaySinh, GioiTinh, Email, SoDienThoai, ChucVu, FORMAT(LuongCoBan,'N0') AS LuongCoBan, HeSoLuong, FORMAT(LuongThuong,'N0') AS LuongThuong, FORMAT(Luong,'N0') AS Luong FROM NhanVien WHERE MaNhanVien <> 'NV000';");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng sửa thông tin nhân viên".Green());
                            ControllerView.Print("SELECT MaNhanvien, TenNhanVien, CONVERT(VARCHAR, NgaySinh, 103) as NgaySinh, GioiTinh, Email, SoDienThoai, CONVERT(varchar, NgayTuyenDung,103) AS NgayTuyenDung, ChucVu, FORMAT(LuongCoBan,'N0') AS LuongCoBan, HeSoLuong, FORMAT(LuongThuong,'N0') AS LuongThuong, FORMAT(Luong,'N0') AS Luong FROM NhanVien WHERE MaNhanVien <> 'NV000'");
                            Console.Write("\tNhập mã nhân viên cần cập nhật: ".Magenta());
                            string maNhanVien = Console.ReadLine().ToUpper();
                            if ((maNhanVien != "NV000") && ControllerCheckID.CheckID(maNhanVien.ToUpper(), "NhanVien", "MaNhanVien"))
                            {
                                Console.WriteLine("\t\tMã {0} tồn tại!".Green(), maNhanVien);
                                Console.ReadKey();
                                List<SqlParameter> sqlParameters = ModelNhanVien.ThongTinNV(maNhanVien);
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\tKhông có thay đổi mới...".Yellow());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE NhanVien SET " + query + " WHERE MaNhanVien = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                                }
                            }
                            else Console.WriteLine("\t\tMã {0} không tồn tại!".Red(), maNhanVien);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thêm nhân viên vào danh sách".Green());
                            List<SqlParameter> sqlParameters = ModelNhanVien.ThongTinCapNhatNhanVien();
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
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa nhân viên ra khỏi danh sách".Green());
                            if (ModelNhanVien.XoaNhanVien())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!".Red());
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin nhân viên".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();

                            string query = "SELECT MaNhanVien, TenNhanVien, CONVERT(varchar, NgaySinh, 103) as NgaySinh, GioiTinh, Email, SoDienThoai, ChucVu, FORMAT(LuongCoBan,'N0') AS LuongCoBan, HeSoLuong, FORMAT(LuongThuong,'N0') AS LuongThuong, FORMAT(Luong,'N0') AS Luong FROM NhanVien WHERE (MaNhanVien LIKE '%"
                                + keyword + "%' OR TenNhanVien LIKE N'%"
                                + keyword + "%' OR NgaySinh LIKE '%"
                                + keyword + "%' OR GioiTinh LIKE N'%"
                                + keyword + "%' OR Email LIKE '%"
                                + keyword + "%' OR SoDienThoai LIKE '%"
                                + keyword + "%' OR NgayTuyenDung LIKE '%"
                                + keyword + "%' OR ChucVu LIKE N'%"
                                + keyword + "%' OR TenDangNhap LIKE '%"
                                + keyword + "%' OR HeSoLuong LIKE '%"
                                + keyword + "%' OR LuongCoBan LIKE '%"
                                + keyword + "%' OR LuongThuong LIKE '%" + keyword + "%') AND MaNhanVien <> 'NV000' ;";
                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\t\t\tKhông tìm thấy nhân viên có thông tin '".Yellow()+keyword.Red()+"'".Yellow());
                            }
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tính lương nhân viên".Green());
                            ControllerView.Print(ModelNhanVien.TinhLuong());
                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không)?"))
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
