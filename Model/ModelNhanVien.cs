using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelNhanVien
    {
        public static List<SqlParameter> ThongTinNV(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenNhanVien,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,CONVERT(varchar, NgayTuyenDung, 103) as NgayTuyenDung,ChucVu," +
                "LuongCoBan,HeSoLuong,LuongThuong,Luong FROM NhanVien WHERE MaNhanVien = '" + id + "';");
            string tenNhanVien = null;
            Console.Write("\tTên nhân viên: ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                tenNhanVien = input;
            }

            DateTime? ngaySinh = null;
            Console.Write("\tNgày sinh (yyyy-MM-dd): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ngaySinh = DateTime.Parse(input);
            }

            string gioiTinh = null;
            Console.WriteLine("\tChọn giới tính: ".Green());
            Console.WriteLine("\t\t0. Nam".Red());
            Console.WriteLine("\t\t1. Nữ".Red());
            Console.Write("\tChon: ".Magenta());
            input= Console.ReadLine();
            if (input =="0") gioiTinh = "Nam";
            else if (input =="1") gioiTinh = "Nữ";
            string email = null;
            Console.Write("\tEmail: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                email = input;
            }

            string soDienThoai = null;
            Console.Write("\tSố điện thoại: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                soDienThoai = input;
            }

            DateTime? ngayTuyenDung = null;
            Console.Write("\tNgày tuyển dụng (yyyy-MM-dd): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ngayTuyenDung = DateTime.Parse(input);
            }

            string chucVu = null;
            Console.WriteLine("\tVui lòng chọn chức vụ:".Green());
            Console.WriteLine("\t\t1. Nhân viên thu ngân".Blue());
            Console.WriteLine("\t\t2. Nhân viên phục vụ".Blue());
            Console.Write("\tMời chọn: ".Magenta());
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        chucVu = "Nhân viên thu ngân";
                        break;
                    }
                   case "2":
                    {
                        chucVu = "Nhân viên phục vụ";
                        break;
                    }
            }
            int luongCoban = 0;
            Console.Write("\tLương cơ bản: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                luongCoban = int.Parse(input);
            }
            float heSoLuong = 0;
            Console.Write("\tHệ số lương: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                heSoLuong = float.Parse(input);
            }

            int luongThuong = 0;
            Console.Write("\tLương thưởng: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                luongThuong = int.Parse(input);
            }
            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(tenNhanVien))
            {
                query += "TenNhanVien = @tenNhanVien, ";
                parameters.Add(new SqlParameter("@tenNhanVien", tenNhanVien));
            }
            if (ngaySinh != null)
            {
                query += "NgaySinh = @ngaySinh, ";
                parameters.Add(new SqlParameter("@ngaySinh", ngaySinh));
            }
            if (!string.IsNullOrEmpty(gioiTinh))
            {
                query += "GioiTinh = @gioiTinh, ";
                parameters.Add(new SqlParameter("@gioiTinh", gioiTinh));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query += "Email = @email, ";
                parameters.Add(new SqlParameter("@email", email));
            }
            if (!string.IsNullOrEmpty(soDienThoai))
            {
                query += "SoDienThoai = @soDienThoai, ";
                parameters.Add(new SqlParameter("@soDienThoai", soDienThoai));
            }
            if (ngayTuyenDung != null)
            {
                query += "NgayTuyenDung = @ngayTuyenDung, ";
                parameters.Add(new SqlParameter("@ngayTuyenDung", ngayTuyenDung));
            }
            if (!string.IsNullOrEmpty(chucVu))
            {
                query += "ChucVu = @chucVu, ";
                parameters.Add(new SqlParameter("@chucVu", chucVu));
            }
            if (luongCoban > 0 )
            {
                query += "LuongCoBan = @LuongCoban, ";
                parameters.Add(new SqlParameter("@LuongCoBan",luongCoban));
            }
            if (heSoLuong > 0)
            {
                query += "HeSoLuong = @heSoLuong, ";
                parameters.Add(new SqlParameter("@heSoLuong", heSoLuong));
            }
            
            if (luongThuong > 0)
            {
                query += "LuongThuong = @luongThuong, ";
                parameters.Add(new SqlParameter("@luongThuong", luongThuong));
            }
            if (!string.IsNullOrEmpty(query))
            {
                query = query.TrimEnd(',', ' ');
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("query", query));
                return parameters;
            }
            else
            {
                return null;
            }
        }
        public static List<SqlParameter> ThongTinCapNhatNhanVien()
        {
            string maNhanVien = ControllerGenerate.Generate("NV", "NhanVien", "MaNhanVien");
            string tenNhanVien;
            Console.WriteLine("\t\t\t(*) Bắt buộc nhập!".Red());
            do
            {
                Console.Write("\tTên nhân viên(*): ".Magenta());
                tenNhanVien = Console.ReadLine();
            } while (string.IsNullOrEmpty(tenNhanVien));

            Console.Write("\tNgày sinh (dd-mm-yyyy): ".Magenta());
            string ngaySinhStr = Console.ReadLine();
            DateTime? ngaySinh = null;
            if (!string.IsNullOrEmpty(ngaySinhStr))
            {
                ngaySinh = DateTime.Parse(ngaySinhStr);
            }
            string gioiTinh = null;
            Console.WriteLine("\tGiới tính: ".Green());
            Console.WriteLine("\t\t0. Nam".Red());
            Console.WriteLine("\t\t1. Nữ".Red());
            Console.Write("\tChọn: ".Magenta());
            string chon = Console.ReadLine();
            if (chon == "0") gioiTinh = "Nam";
            else gioiTinh = "Nữ";

            Console.Write("\tEmail: ".Magenta());
            string email = Console.ReadLine();

            string soDienThoai;
            do
            {
                Console.Write("\tSố điện thoại(*): ".Magenta());
                soDienThoai = Console.ReadLine();
                if (soDienThoai.Length < 10 || soDienThoai.Length > 12 || !soDienThoai.All(char.IsDigit))
                {
                    Console.WriteLine("\t\tSố điện thoại không hợp lệ. Vui lòng nhập lại!".Red());
                    soDienThoai = null;
                }
            } while (string.IsNullOrEmpty(soDienThoai));

            Console.Write("\tNgày tuyển dụng (yyyy-MM-dd): ".Magenta());
            string ngayTuyenDungStr = Console.ReadLine();
            DateTime? ngayTuyenDung = DateTime.Now;
            if (!string.IsNullOrEmpty(ngayTuyenDungStr))
            {
                ngayTuyenDung = DateTime.Parse(ngayTuyenDungStr);
            }
            string chucVu = null;
            float heSoLuong = 0;
            int luongCoBan = 0,luongThuong = 0, luachon = 0;
            bool nhaplai = true;
            do
            {
                Console.WriteLine("\tVui lòng chọn chức vụ(*):".Yellow());
                Console.WriteLine("\t\t1. Nhân viên thu ngân".Blue());
                Console.WriteLine("\t\t2. Nhân viên phục vụ".Blue());
                Console.Write("\tMời chọn: ".Magenta());
                nhaplai = !Int32.TryParse(Console.ReadLine(), out luachon) || luachon < 1 || luachon > 2;
            } while (nhaplai);
            switch (luachon)
            {
                case 1:
                    {
                        chucVu = "Nhân viên thu ngân";
                        luongCoBan = 2500000;
                        heSoLuong = 2.0f;
                        luongThuong = 0;
                        break;
                    }
                case 2:
                    {
                        chucVu = "Nhân viên phục vụ";
                        luongCoBan = 2300000;
                        heSoLuong = 1.5f;
                        luongThuong = 0;
                        break;
                    }
            }
            string tenDangNhap = "";
            string matKhau = "";
            string xacNhan = "";

            while (string.IsNullOrWhiteSpace(tenDangNhap))
            {
                Console.Write("\tTên đăng nhập(*): ".Magenta());
                tenDangNhap = Console.ReadLine();
                string sql = "SELECT TenDangNhap FROM NhanVien WHERE TenDangNhap = '" + tenDangNhap + "'";
                if (!string.IsNullOrEmpty(ControllerExcution.ExcutionString(sql)))
                {
                    Console.WriteLine("\t\tTên đăng nhập đã bị trùng. Vui lòng nhập lại!".Red());
                    tenDangNhap = null;
                }
            }
            do
            {
                do
                {
                    Console.Write("\tMật khẩu(*): ".Magenta());
                    matKhau = ControllerMaHoaPW.ReadPassword();
                    if (string.IsNullOrWhiteSpace(matKhau))
                    {
                        Console.WriteLine("\t\tMật khẩu không được để trống!".Yellow());
                    }
                } while (string.IsNullOrWhiteSpace(matKhau));

                do
                {
                    Console.Write("\tXác nhận mật khẩu: ".Magenta());
                    xacNhan = ControllerMaHoaPW.ReadPassword();
                } while (string.IsNullOrWhiteSpace(xacNhan));
                if (matKhau != xacNhan)
                    Console.WriteLine("\t\tMật khẩu không khớp. Vui lòng nhập lại ! ".Yellow());
            } while (xacNhan != matKhau);
            matKhau = BCrypt.Net.BCrypt.HashPassword(matKhau, workFactor: 7);
            string query = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, NgaySinh, GioiTinh, Email," +
                " SoDienThoai, NgayTuyenDung, ChucVu, TenDangNhap, MatKhau,LuongCoBan," +
                " HeSoLuong, LuongThuong) " +
                             "VALUES (@MaNhanVien, @TenNhanVien, @NgaySinh, @GioiTinh, @Email, @SoDienThoai," +
                             " @NgayTuyenDung, @ChucVu, @TenDangNhap, @MatKhau,@LuongCoBan, @HeSoLuong," +
                             " @LuongThuong)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaNhanVien", maNhanVien));
            parameters.Add(new SqlParameter("@TenNhanVien", tenNhanVien));
            parameters.Add(new SqlParameter("@NgaySinh", ngaySinh.HasValue ? (object)ngaySinh.Value : DBNull.Value));
            parameters.Add(new SqlParameter("@GioiTinh", gioiTinh));
            parameters.Add(new SqlParameter("@Email", email));
            parameters.Add(new SqlParameter("@SoDienThoai", soDienThoai));
            parameters.Add(new SqlParameter("@NgayTuyenDung", ngayTuyenDung.HasValue ? (object)ngayTuyenDung.Value : DBNull.Value));
            parameters.Add(new SqlParameter("@ChucVu", chucVu));
            parameters.Add(new SqlParameter("@TenDangNhap", tenDangNhap));
            parameters.Add(new SqlParameter("@MatKhau", matKhau));
            parameters.Add(new SqlParameter("@LuongCoBan",luongCoBan));
            parameters.Add(new SqlParameter("@HeSoLuong", heSoLuong));
            parameters.Add(new SqlParameter("@LuongThuong", luongThuong));
            parameters.Add(new SqlParameter("query", query));
            return parameters;
        }
        public static string TinhLuong()
        {
            return "UPDATE NhanVien SET Luong = LuongCoBan * HeSoLuong + LuongThuong; " +
                "SELECT MaNhanVien, TenNhanVien, ChucVu, HeSoLuong, FORMAT(LuongThuong,'N0') AS LuongThuong, FORMAT(Luong,'N0') AS Luong FROM NhanVien WHERE MaNhanVien <> 'NV000';";
        }
        public static bool XoaNhanVien()
        {
            string query = "SELECT MaNhanVien, TenNhanVien, Email, SoDienThoai, ChucVu FROM NhanVien WHERE MaNhanVien <> 'NV000'; ";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\t\tKhông tìm thấy nhân viên nào.".Yellow());
                return false;
            }
            else
            {
                bool isMaNhanVienValid;
                string maNhanVien;
                do
                {
                    Console.Write("\tNhập mã nhân viên bạn muốn xóa: ".Magenta());
                    maNhanVien = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM NhanVien WHERE MaNhanVien = '" + maNhanVien + "';";
                    isMaNhanVienValid = ControllerExcution.Excution(query);
                    if (!isMaNhanVienValid)
                    {
                        Console.WriteLine("\t\tMã nhân viên bạn muốn xóa không tồn tại ! Vui lòng nhập lại !".Yellow());
                        if (ControllerXacNhan.IsXacNhan("Bạn muốn quay lại không ?"))
                            return false;
                    }
                } while (!isMaNhanVienValid);

                if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn muốn xóa nhân viên này?"))
                {
                    query = "DELETE FROM NhanVien WHERE MaNhanVien = '" + maNhanVien + "';";
                    ControllerExcution.Excution(query);

                    Console.WriteLine("\t\tĐã xóa nhân viên có mã {0}".Green(), maNhanVien);
                    return true;
                }
                else
                {
                    Console.WriteLine("\t\tKhông xóa nhân viên có mã {0}".Red(), maNhanVien);
                    return false;
                }
            }
        }
    }
}
