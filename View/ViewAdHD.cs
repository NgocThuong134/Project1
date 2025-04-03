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
    internal class ViewAdHD
    {
        public void MenuAdHD()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
    .AddRow("1", "Xem danh sách các hóa đơn")
    .AddRow("2", "Cập nhật thông tin hóa đơn")
    .AddRow("3", "Thêm hóa đơn vào danh sách")
    .AddRow("4", "Xóa hóa đơn khỏi danh sách")
    .AddRow("5", "Tìm kiếm thông tin hóa đơn")
    .AddRow("0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("--------------QUẢN LÝ HÓA ĐƠN---------------".Yellow());

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
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách hóa đơn.".Green());
                        ControllerView.Print("SELECT MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, DanhGiaChatLuong," +
                            " DanhGiaDichVu, TrangThai, MaBanAn, MaKhachHang, MaNhanVien,FORMAT (GiamGia,'N0') AS GiamGia, " +
                            "FORMAT(TongTien,'N0') AS TongTien, HinhThucThanhToan FROM HoaDon;");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin hóa đơn.".Green());
                            ControllerView.Print("SELECT MaHoaDon,ThoiGianNhap,ThoiGianXuat,LoaiHoaDon,TrangThai,MaBanAn,MaKhachHang,MaNhanVien,FORMAT(GiamGia,'N0') AS GiamGia, FORMAT(TongTien,'N0') AS TongTien,HinhThucThanhToan " +
                                "FROM HoaDon");
                            Console.Write("\tNhập mã hóa đơn cần cập nhật: ".Magenta());
                            string maHoaDon = Console.ReadLine();
                            if (ControllerCheckID.CheckID(maHoaDon.ToUpper(), "HoaDon", "MaHoaDon"))
                            {
                                Console.WriteLine("\t\t\tMã {0} đã tồn tại!".Green(), maHoaDon);
                                Console.ReadKey();
                                List<SqlParameter> sqlParameters = ModelHD.ThongTinHoaDon(maHoaDon);
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\tKhông có thay đổi mới...".Yellow());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE HoaDon SET " + query + " WHERE MaHoaDon = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                                }
                            }
                            else Console.WriteLine("\t\t\tMã {0} không tồn tại!".Red(), maHoaDon);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thêm hóa đơn vào danh sách.".Green());
                            List<SqlParameter> sqlParameters = ModelHD.ThongTinCapNhatHoaDon();
                            SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                            string query = queryParameter.Value.ToString();
                            sqlParameters.RemoveAt(sqlParameters.Count - 1);
                            if (ControllerUpdate.CapNhat(query, sqlParameters))
                                Console.WriteLine("\t\t\tThêm hóa đơn thành công!".Green());
                            else Console.WriteLine("\t\t\tThêm hóa đơn thất bại!".Red());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa hóa đơn khỏi danh sách.".Green());
                            if (ModelHD.XoaHoaDon())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!".Red());
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin hóa đơn.".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();

                            string query = "SELECT MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, DanhGiaChatLuong, DanhGiaDichVu, TrangThai, MaBanAn, MaKhachHang, MaNhanVien,FORMAT(GiamGia,'N0') AS GiamGia, FORMAT(TongTien,'N0') AS TongTien,HinhThucThanhToan FROM HoaDon WHERE MaHoaDon LIKE '%"
                                + keyword + "%' OR ThoiGianNhap LIKE '%"
                                + keyword + "%' OR ThoiGianXuat LIKE '%" 
                                + keyword + "%' OR LoaiHoaDon LIKE N'%" 
                                + keyword + "%' OR DanhGiaChatLuong LIKE '%" 
                                + keyword + "%' OR DanhGiaDichVu LIKE '%"
                                + keyword + "%' OR NhanXet LIKE N'%"
                                + keyword + "%' OR TRY_CAST('"+keyword+"' AS BIT) = TrangThai " +
                                "OR MaBanAn LIKE '%" + keyword + "%' OR MaKhachHang LIKE '%" 
                                + keyword + "%' OR MaNhanVien LIKE '%" 
                                + keyword + "%' OR GiamGia LIKE '%" 
                                + keyword + "%' OR HinhThucThanhToan LIKE N'%" 
                                + keyword + "%' OR TongTien LIKE '%" 
                                + keyword + "%'";

                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\t\tKhông tìm thấy hóa đơn có thông tin '{0}'".Red(), keyword);
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
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng nhập lại.".Yellow().BlueBg());
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "0");
        }
    }
}
