using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerHangKH
    {
        public static void SetHangKH(string maKH)
        {
            string sql = "UPDATE KhachHang SET HangKhachHang = " +
            "CASE " +
            "WHEN (SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 5 AND (SELECT SUM(TongTien) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 1000000 THEN 'Thanh vien Bac' " +
            "WHEN (SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 10 AND (SELECT SUM(TongTien) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 3000000 THEN 'Thanh vien Vang' " +
            "WHEN (SELECT COUNT(*) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 20 AND (SELECT SUM(TongTien) FROM HoaDon WHERE MaKhachHang = @MaKhachHang AND TrangThai = 1) > 7000000 THEN 'Thanh vien Kim cuong' " +
            "ELSE N'Thanh vien' " +
            "END " +
            "WHERE MaKhachHang = @MaKhachHang";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaKhachHang",maKH));
            ControllerUpdate.CapNhat(sql, parameters);
        }
    }
}
