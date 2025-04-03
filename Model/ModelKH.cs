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
    internal class ModelKH
    {
        public static List<SqlParameter> ThongTinKH(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang," +
                "DiemTichLuy FROM KhachHang WHERE MaKhachHang = '" + id + "';");
            string tenKhachHang = null;
            Console.Write("\tNhập tên khách hàng: ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                tenKhachHang = input;
            }

            DateTime? ngaySinh = null;
            Console.Write("\tNhập ngày sinh (yyyy-MM-dd): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ngaySinh = DateTime.Parse(input);
            }

            string gioiTinh = null;
            Console.Write("\tNhập giới tính: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                gioiTinh = input;
            }

            string email = null;
            Console.Write("\tNhập email: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                email = input;
            }

            string soDienThoai = null;
            Console.Write("\tNhập số điện thoại: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                soDienThoai = input;
            }

            string diaChi = null;
            Console.Write("\tNhập địa chỉ: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                diaChi = input;
            }

            int? diemTichLuy = null;
            Console.Write("\tNhập điểm tích lũy: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                diemTichLuy = int.Parse(input);
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(tenKhachHang))
            {
                query += "TenKhachHang = @tenKhachHang, ";
                parameters.Add(new SqlParameter("@tenKhachHang", tenKhachHang));
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
            if (!string.IsNullOrEmpty(diaChi))
            {
                query += "DiaChi = @diaChi, ";
                parameters.Add(new SqlParameter("@diaChi", diaChi));
            }
            
            if (diemTichLuy != null)
            {
                query += "DiemTichLuy = @diemTichLuy, ";
                parameters.Add(new SqlParameter("@diemTichLuy", diemTichLuy));
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
        public static List<SqlParameter> CapNhatKH(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi " +
                "FROM KhachHang WHERE MaKhachHang = '" + id + "';");
            string tenKhachHang = null;
            Console.Write("\tNhập tên: ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                tenKhachHang = input;
            }

            DateTime? ngaySinh = null;
            Console.Write("\tNhập ngày sinh (yyyy-MM-dd): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                ngaySinh = DateTime.Parse(input);
            }

            string gioiTinh = null;
            Console.WriteLine("\tGiới tính: ".Green());
            Console.WriteLine("\t\t0. Nam".Red());
            Console.WriteLine("\t\t1. Nữ".Yellow());
            Console.Write("\tChon: ".Magenta());
            string chon = Console.ReadLine();
            if (chon == "0") gioiTinh = "Nam";
            else if (chon == "1") gioiTinh = "Nữ";

            string email = null;
            Console.Write("\tNhập email: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                email = input;
            }

            string soDienThoai = null;
            Console.Write("\tNhập số điện thoại: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                soDienThoai = input;
            }

            string diaChi = null;
            Console.Write("\tNhập địa chỉ: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                diaChi = input;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(tenKhachHang))
            {
                query += "TenKhachHang = @tenKhachHang, ";
                parameters.Add(new SqlParameter("@tenKhachHang", tenKhachHang));
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
            if (!string.IsNullOrEmpty(diaChi))
            {
                query += "DiaChi = @diaChi, ";
                parameters.Add(new SqlParameter("@diaChi", diaChi));
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
        public static bool XoaKhachHang()
        {
            string query = "SELECT MaKhachHang, TenKhachHang, Email, SoDienThoai, DiaChi, HangKhachHang FROM KhachHang;";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\tKhông tìm thấy khách hàng nào.".Yellow());
                return false;
            }
            else
            {
                bool isMaKhachHangValid;
                string maKhachHang;
                do
                {
                    Console.Write("\tNhập mã khách hàng bạn muốn xóa: ".Magenta());
                    maKhachHang = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = '" + maKhachHang + "';";
                    isMaKhachHangValid = ControllerExcution.Excution(query);
                } while (!isMaKhachHangValid);

                if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn muốn xóa khách hàng này?"))
                {
                    query = "DELETE FROM ChiTietKhachHangUuDai WHERE MaKhachHang = '" + maKhachHang + "';";
                    ControllerExcution.Excution(query);

                    query = "DELETE FROM KhachHang WHERE MaKhachHang = '" + maKhachHang + "';";
                    ControllerExcution.Excution(query);

                    Console.WriteLine("\tĐã xóa khách hàng có mã {0}".Green(), maKhachHang);
                    return true;
                }
                else
                {
                    Console.WriteLine("\tKhông xóa khách hàng có mã {0}".Red(), maKhachHang);
                    return false;
                }
            }
        }
    }
}
