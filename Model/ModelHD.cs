using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelHD
    {
        public static List<SqlParameter> ThongTinHoaDon(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT ThoiGianNhap,ThoiGianXuat,LoaiHoaDon,TrangThai,MaBanAn,MaKhachHang,MaNhanVien,GiamGia,TongTien,HinhThucThanhToan " +
                "FROM HoaDon WHERE MaHoaDon = '" + id + "';");
            DateTime? thoiGianNhap = null;
            Console.Write("\tNhập thời gian nhập (yyyy-MM-dd HH:mm:ss): ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                thoiGianNhap = DateTime.Parse(input);
            }

            DateTime? thoiGianXuat = null;
            Console.Write("\tNhập thời gian xuất (yyyy-MM-dd HH:mm:ss): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                thoiGianXuat = DateTime.Parse(input);
            }

            string loaiHoaDon = null;
            Console.WriteLine("\tChọn loại hóa đơn".Blue());
            Console.WriteLine("\t\t1. Hóa đơn trực tiếp");
            Console.WriteLine("\t\t2. Hóa đơn online");
            Console.Write("\tNhập lựa chọn của bạn: ".Magenta());
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    loaiHoaDon = "Trực tiếp";
                    break;
                case "2":
                    loaiHoaDon = "Online";
                    break;
            }

            string maBanAn = null;
            bool ok;
            string sql;
            do
            {
                Console.Clear();
                ControllerView.Print("SELECT ThoiGianNhap,ThoiGianXuat,LoaiHoaDon,TrangThai,MaBanAn,MaKhachHang,MaNhanVien,GiamGia,TongTien,HinhThucThanhToan " +
               "FROM HoaDon WHERE MaHoaDon = '" + id + "';");
                ControllerView.Print("SELECT MaBanAn,TenBanAn,SoChoNgoi,ChuThich FROM BanAn;");
                Console.Write("\tNhập mã bàn ăn: ".Magenta());
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    sql = "SELECT COUNT(*) FROM BanAn WHERE TrangThai = 0 AND MaBanAn = '" + input + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tMã bàn ăn không tồn tại... Vui lòng nhập lại!".Yellow());
                    else maBanAn = input.ToUpper();
                }
                else ok = true;
            } while (!ok);

            string maKhachHang = null;
            do
            {
                Console.Clear();
                ControllerView.Print("SELECT ThoiGianNhap,ThoiGianXuat,LoaiHoaDon,TrangThai,MaBanAn,MaKhachHang,MaNhanVien,GiamGia,TongTien,HinhThucThanhToan " +
               "FROM HoaDon WHERE MaHoaDon = '" + id + "';");
                ControllerView.Print("SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang FROM KhachHang;");
                Console.Write("\tNhập mã khách hàng: ".Magenta());
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = '" + input + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tMã khách hàng không tồn tại... Vui lòng nhập lại!".Yellow());
                    else maKhachHang = input.ToUpper();
                }
                else ok = true;
            } while (!ok);

            string maNhanVien = null;
            do
            {
                Console.Clear();
                ControllerView.Print("SELECT ThoiGianNhap,ThoiGianXuat,LoaiHoaDon,TrangThai,MaBanAn,MaKhachHang,MaNhanVien,GiamGia,TongTien,HinhThucThanhToan " +
               "FROM HoaDon WHERE MaHoaDon = '" + id + "';");
                ControllerView.Print("SELECT MaNhanVien, TenNhanVien, CONVERT(varchar, NgaySinh, 103) as NgaySinh, GioiTinh, Email, SoDienThoai, ChucVu FROM NhanVien WHERE MaNhanVien <> 'NV000' ;");
                Console.Write("\tNhập mã nhân viên: ".Magenta());
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    sql = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = '" + input + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tMã nhân viên không tồn tại... Vui lòng nhập lại!".Yellow());
                    else maNhanVien = input.ToUpper();
                }
                else ok = true;
            } while (!ok);
            List < SqlParameter > parameters = new List<SqlParameter>();
            string query = null;
            if (thoiGianNhap != null)
            {
                query += "ThoiGianNhap = @thoiGianNhap, ";
                parameters.Add(new SqlParameter("@thoiGianNhap", thoiGianNhap));
            }
            if (thoiGianXuat != null)
            {
                query += "ThoiGianXuat = @thoiGianXuat, ";
                parameters.Add(new SqlParameter("@thoiGianXuat", thoiGianXuat));
            }
            if (!string.IsNullOrEmpty(loaiHoaDon))
            {
                query += "LoaiHoaDon = @loaiHoaDon, ";
                parameters.Add(new SqlParameter("@loaiHoaDon", loaiHoaDon));
            }

            if (!string.IsNullOrEmpty(maBanAn))
            {
                query += "MaBanAn = @maBanAn, ";
                parameters.Add(new SqlParameter("@maBanAn", maBanAn));
            }
            if (!string.IsNullOrEmpty(maKhachHang))
            {
                query += "MaKhachHang = @maKhachHang, ";
                parameters.Add(new SqlParameter("@maKhachHang", maKhachHang));
            }
            if (!string.IsNullOrEmpty(maNhanVien))
            {
                query += "MaNhanVien = @maNhanVien, ";
                parameters.Add(new SqlParameter("@maNhanVien", maNhanVien));
            }
            if (!string.IsNullOrEmpty(query))
            {
                query = query.TrimEnd(',', ' ');
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("query", query));
                return parameters;
            }
            else return null;
        }
        public static List<SqlParameter> ThongTinCapNhatHoaDon()
        {
            Console.WriteLine("* Bắt buộc nhập !".Red().YellowBg());
            string maHoaDon = ControllerGenerate.Generate("HD", "HoaDon", "MaHoaDon");

            DateTime thoiGianNhap = DateTime.Now;
            Console.Write("\tThời gian nhập (dd/MM/yyyy HH:mm:ss): ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                thoiGianNhap = DateTime.Parse(input);
            }
            string loaiHoaDon = "Trực tiếp";
            Console.WriteLine("\tVui lòng chọn loại hóa đơn: ".Magenta());
            Console.WriteLine("\t\t1. Trực tiếp".Green());
            Console.WriteLine("\t\t2. Online".Green());
            Console.Write("\tNhập lựa chọn của bạn: ".Magenta());
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    loaiHoaDon = "Trực tiếp";
                    break;
                case "2":
                    loaiHoaDon = "Online";
                    break;
            }

            string maBanAn = null;
            bool ok;
            string sql;
            do
            {
                Console.Clear();
                ControllerView.Print("SELECT MaBanAn,TenBanAn,SoChoNgoi,ChuThich FROM BanAn WHERE TrangThai = 0;");
                Console.Write("\tMã bàn ăn: ".Magenta());
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    sql = "SELECT COUNT(*) FROM BanAn WHERE TrangThai = 0 AND MaBanAn = '" + input + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tMã bàn ăn không tồn tại... Vui lòng nhập lại!".Yellow());
                    else maBanAn = input.ToUpper();
                }
                else ok = true;
            } while (!ok);

            string maKhachHang = null;
            do
            {
                Console.Clear();
                ControllerView.Print("SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang FROM KhachHang;");
                Console.Write("\tMã khách hàng: ".Magenta());
                input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    sql = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = '" + input + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tMã khách hàng không tồn tại... Vui lòng nhập lại!".Yellow());
                    else maKhachHang = input.ToUpper();
                }
                else ok = true;
            } while (!ok);

            string query = "INSERT INTO HoaDon (MaHoaDon, ThoiGianNhap, LoaiHoaDon, MaBanAn, MaKhachHang, MaNhanVien) " +
                            "VALUES (@MaHoaDon, @ThoiGianNhap, @LoaiHoaDon, @MaBanAn, @MaKhachHang, @MaNhanVien)";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@MaHoaDon", maHoaDon),
        new SqlParameter("@ThoiGianNhap", thoiGianNhap),
        new SqlParameter("@LoaiHoaDon", loaiHoaDon),
        new SqlParameter("@MaBanAn", string.IsNullOrEmpty(maBanAn) ? DBNull.Value : (object)maBanAn),
        new SqlParameter("@MaKhachHang", string.IsNullOrEmpty(maKhachHang) ? DBNull.Value : (object)maKhachHang),
        new SqlParameter("@MaNhanVien", "NV000"),
        new SqlParameter("@query", query),
    };

            return parameters;
        }
        public static void TaoHoaDon(string MaHoaDon, string id)
        {
            string query = "INSERT INTO HoaDon (MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, DanhGiaChatLuong, DanhGiaDichVu, NhanXet,TrangThai, MaBanAn, MaKhachHang, MaNhanVien, TongTien) " +
                   "VALUES (@MaHoaDon, @ThoiGianNhap, @ThoiGianXuat, @LoaiHoaDon, @DanhGiaChatLuong, @DanhGiaDichVu, @NhanXet, @TrangThai, @MaBanAn, @MaKhachHang, @MaNhanVien, @TongTien)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaHoaDon", MaHoaDon));
            parameters.Add(new SqlParameter("@ThoiGianNhap", DateTime.Now));
            parameters.Add(new SqlParameter("@ThoiGianXuat", DBNull.Value));
            parameters.Add(new SqlParameter("@LoaiHoaDon", "Online"));
            parameters.Add(new SqlParameter("@DanhGiaChatLuong", DBNull.Value));
            parameters.Add(new SqlParameter("@DanhGiaDichVu", DBNull.Value));
            parameters.Add(new SqlParameter("@NhanXet", DBNull.Value));
            parameters.Add(new SqlParameter("@TrangThai", false));
            parameters.Add(new SqlParameter("@MaBanAn", DBNull.Value));
            parameters.Add(new SqlParameter("@MaKhachHang", id));
            parameters.Add(new SqlParameter("@MaNhanVien", DBNull.Value));
            parameters.Add(new SqlParameter("@TongTien", DBNull.Value));
            ControllerUpdate.CapNhat(query, parameters);
        }
        public static void NVTaoHoaDon(string MaHoaDon, string id, string MaNV, string MaBA)
        {
            string query = "INSERT INTO HoaDon (MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, DanhGiaChatLuong, DanhGiaDichVu, NhanXet, TrangThai, MaBanAn, MaKhachHang, MaNhanVien, TongTien) " +
                   "VALUES (@MaHoaDon, @ThoiGianNhap, @ThoiGianXuat, @LoaiHoaDon, @DanhGiaChatLuong, @DanhGiaDichVu, @NhanXet, @TrangThai, @MaBanAn, @MaKhachHang, @MaNhanVien, @TongTien)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaHoaDon", MaHoaDon));
            parameters.Add(new SqlParameter("@ThoiGianNhap", DateTime.Now));
            parameters.Add(new SqlParameter("@ThoiGianXuat", DBNull.Value));
            parameters.Add(new SqlParameter("@LoaiHoaDon", "Trực tiếp"));
            parameters.Add(new SqlParameter("@DanhGiaChatLuong", DBNull.Value));
            parameters.Add(new SqlParameter("@DanhGiaDichVu", DBNull.Value));
            parameters.Add(new SqlParameter("@NhanXet", DBNull.Value));
            parameters.Add(new SqlParameter("@TrangThai", false));
            parameters.Add(new SqlParameter("@MaBanAn", MaBA));
            parameters.Add(new SqlParameter("@MaKhachHang", string.IsNullOrEmpty(id) ? DBNull.Value : (object)id));
            parameters.Add(new SqlParameter("@MaNhanVien", MaNV));
            parameters.Add(new SqlParameter("@TongTien", DBNull.Value));
            ControllerUpdate.CapNhat(query, parameters);
        }
        public static void KHTaoHoaDon(string MaHoaDon, string id, string MaBA)
        {
            string query = "INSERT INTO HoaDon (MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, DanhGiaChatLuong, DanhGiaDichVu, NhanXet, TrangThai, MaBanAn, MaKhachHang, MaNhanVien, TongTien) " +
                   "VALUES (@MaHoaDon, @ThoiGianNhap, @ThoiGianXuat, @LoaiHoaDon, @DanhGiaChatLuong, @DanhGiaDichVu, @NhanXet, @TrangThai, @MaBanAn, @MaKhachHang, @MaNhanVien, @TongTien)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaHoaDon", MaHoaDon));
            parameters.Add(new SqlParameter("@ThoiGianNhap", DateTime.Now));
            parameters.Add(new SqlParameter("@ThoiGianXuat", DBNull.Value));
            parameters.Add(new SqlParameter("@LoaiHoaDon", "Trực tiếp"));
            parameters.Add(new SqlParameter("@DanhGiaChatLuong", DBNull.Value));
            parameters.Add(new SqlParameter("@DanhGiaDichVu", DBNull.Value));
            parameters.Add(new SqlParameter("@NhanXet", DBNull.Value));
            parameters.Add(new SqlParameter("@TrangThai", false));
            parameters.Add(new SqlParameter("@MaBanAn", MaBA));
            parameters.Add(new SqlParameter("@MaKhachHang", id));
            parameters.Add(new SqlParameter("@MaNhanVien", DBNull.Value));
            parameters.Add(new SqlParameter("@TongTien", DBNull.Value));
            ControllerUpdate.CapNhat(query, parameters);
        }
        public static bool XoaHoaDon()
        {
            string query = "SELECT MaHoaDon, ThoiGianNhap, ThoiGianXuat, LoaiHoaDon, BA.TenBanAn, KH.TenKhachHang, NV.TenNhanVien, HinhThucThanhToan, GiamGia, TongTien " +
                           "FROM HoaDon " +
                           "LEFT JOIN BanAn BA ON HoaDon.MaBanAn = BA.MaBanAn " +
                           "LEFT JOIN KhachHang KH ON HoaDon.MaKhachHang = KH.MaKhachHang " +
                           "LEFT JOIN NhanVien NV ON HoaDon.MaNhanVien = NV.MaNhanVien;";

            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\tKhông tìm thấy hóa đơn nào.".Yellow());
                return false;
            }
            else
            {
                bool isMaHoaDonValid;
                string maHoaDon;
                do
                {
                    Console.Write("\tNhập mã hóa đơn bạn muốn xóa: ".Magenta());
                    maHoaDon = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = '" + maHoaDon + "';";
                    isMaHoaDonValid = ControllerExcution.Excution(query);
                } while (!isMaHoaDonValid);

                if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn muốn xóa hóa đơn này?"))
                {
                    query = "DELETE FROM ChiTietHoaDonMonAn WHERE MaHoaDon = '" + maHoaDon + "';";
                    ControllerExcution.Excution(query);

                    query = "DELETE FROM HoaDon WHERE MaHoaDon = '" + maHoaDon + "';";
                    ControllerExcution.Excution(query);

                    Console.WriteLine("\tĐã xóa hóa đơn có mã {0}".Green(), maHoaDon);
                    return true;
                }
                else
                {
                    Console.WriteLine("\tKhông xóa hóa đơn có mã {0}".Red(), maHoaDon);
                    return false;
                }
            }
        }
    }
}