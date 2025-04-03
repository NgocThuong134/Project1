using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
namespace Project.Controller
{
    internal class ControllerAddCus
    {
        public static bool AddCus(string ten, DateTime ngaysinh, int gioitinh, string email, string diachi,
            string sdt,string tenDN, string pw)
        {
            try
            {
                using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
                {
                    string query = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang,TenDangNhap, MatKhau, NgaySinh," +
                        " GioiTinh, Email, SoDienThoai, DiaChi, NgayThamGia) " +
                        "VALUES (@MaKhachHang, @TenKhachHang,@TenDangNhap, @MatKhau, @NgaySinh, @GioiTinh," +
                        " @Email, @SoDienThoai, @DiaChi, @NgayThamGia)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string maKhachHang = ControllerGenerate.Generate("KH", "KhachHang", "MaKhachHang"); // hàm tạo mã khách hàng
                        string matKhau = BCrypt.Net.BCrypt.HashPassword(pw,workFactor:7); // mã hóa mật khẩu sử dụng thư viện BCrypt.Net
                        DateTime ngayThamGia = DateTime.Now;
                        string gender = "Nam";
                        if (gioitinh == 1)
                            gender = "Nu";
                        command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                        command.Parameters.AddWithValue("@TenKhachHang", ten);
                        command.Parameters.AddWithValue("@TenDangNhap", tenDN);
                        command.Parameters.AddWithValue("@MatKhau", matKhau);
                        command.Parameters.AddWithValue("@NgaySinh", ngaysinh);
                        command.Parameters.AddWithValue("@GioiTinh", gender);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@SoDienThoai", sdt);
                        command.Parameters.AddWithValue("@DiaChi", diachi);
                        command.Parameters.AddWithValue("@HangKhachHang", "Thành viên");
                        command.Parameters.AddWithValue("@NgayThamGia", ngayThamGia);
                        int result = command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            return true;
                        }
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
