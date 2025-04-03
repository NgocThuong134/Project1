using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerThongKe
    {
        public static void ThongKe(DateTime startTime, DateTime endTime)
        {
            string sql = "SELECT HoaDon.MaHoaDon, HoaDon.ThoiGianNhap, HoaDon.ThoiGianXuat, HoaDon.LoaiHoaDon, " +
                "BanAn.TenBanAn, KhachHang.TenKhachHang, NhanVien.TenNhanVien, " +
                "HoaDon.GiamGia, FORMAT(HoaDon.TongTien,'N0') AS TongTien, HoaDon.HinhThucThanhToan " +
                "FROM HoaDon LEFT JOIN BanAn ON HoaDon.MaBanAn = BanAn.MaBanAn " +
                "LEFT JOIN KhachHang ON HoaDon.MaKhachHang = KhachHang.MaKhachHang " +
                "LEFT JOIN NhanVien ON HoaDon.MaNhanVien = NhanVien.MaNhanVien " +
                "WHERE HoaDon.TrangThai = 1 AND HoaDon.ThoiGianNhap BETWEEN '"+startTime.ToString("yyyy-MM-dd")+"' AND '"+endTime.ToString("yyyy-MM-dd")+"';";
            if (ControllerView.Print(sql))
            {
                sql = "SELECT SUM(TongTien) FROM HoaDon WHERE TrangThai = 1 AND HinhThucThanhToan = 'Tien mat' AND " +
                    "ThoiGianNhap BETWEEN '" + startTime.ToString("yyyy-MM-dd") + "' AND '" + endTime.ToString("yyyy-MM-dd") + "';";
                Console.WriteLine("\t\tTổng tiền mặt: {0:N0} VNĐ".Green(), ControllerExcution.ExcutionFloat(sql));

                sql = "SELECT SUM(TongTien) FROM HoaDon WHERE TrangThai = 1 AND HinhThucThanhToan = 'Chuyen khoan' AND " +
                    "ThoiGianNhap BETWEEN '" + startTime.ToString("yyyy-MM-dd") + "' AND '" + endTime.ToString("yyyy-MM-dd") + "';";
                Console.WriteLine("\t\tTổng tiền chuyển khoản: {0:N0} VNĐ".Green(), ControllerExcution.ExcutionFloat(sql));

                sql = "SELECT SUM(GiamGia) FROM HoaDon " +
                   "WHERE TrangThai = 1 AND ThoiGianNhap BETWEEN '" + startTime.ToString("yyyy-MM-dd") + "' AND '" + endTime.ToString("yyyy-MM-dd") + "';";
                Console.WriteLine("\t\tTổng tiền giảm giá: {0:N0} VNĐ".Yellow(), ControllerExcution.ExcutionFloat(sql));

                sql = "SELECT SUM(TongTien) FROM HoaDon " +
                    "WHERE TrangThai = 1 AND ThoiGianNhap BETWEEN '" + startTime.ToString("yyyy-MM-dd") + "' AND '" + endTime.ToString("yyyy-MM-dd") + "';";
                Console.WriteLine("\t\tTổng tiền: {0:N0} VNĐ".Red(), ControllerExcution.ExcutionFloat(sql));
            }
            else
                Console.WriteLine("\t\tKhông tìm thấy ....".Red());
            Console.ReadKey();
        }
    }
}
