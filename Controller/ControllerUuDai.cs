using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerUuDai
    {
        public static string UuDaiMax(string maKH,string maHD)
        {
            using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
            {
                string query = "SELECT TOP 1 ctkh.MaUuDai FROM HoaDon hd " +
                               "JOIN ChiTietKhachHangUuDai ctkh ON hd.MaKhachHang = ctkh.MaKhachHang " +
                               "JOIN UuDai ud ON ctkh.MaUuDai = ud.MaUuDai " +
                               "WHERE hd.MaKhachHang = '"+maKH+"' AND hd.MaHoaDon = '"+maHD+"' AND hd.TongTien >= ud.DieuKien " +
                               "GROUP BY ctkh.MaUuDai " +
                               "ORDER BY SUM(CASE WHEN ud.DonViTinh = '%' " +
                               "THEN hd.TongTien * (100 - ud.GiaTri) / 100 " +
                               "WHEN ud.DonViTinh = 'VNĐ' THEN hd.TongTien - ud.GiaTri " +
                               "ELSE hd.TongTien END) " +
                               "ASC";
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        return result.ToString();
                    else return null;
                }

            }
        }
        public static bool AddUuDai(string maUD,string maHD,string maKH)
        {
            string query = "UPDATE HoaDon SET TongTien = TongTien + GiamGia FROM HoaDon WHERE MaHoaDon = '" + maHD + "';";
            ControllerExcution.Excution(query);
            query = "UPDATE HoaDon SET GiamGia = GiamGia + " +
                               "CASE WHEN UuDai.DonViTinh = N'VNĐ' THEN UuDai.GiaTri " +
                               "WHEN UuDai.DonViTinh = '%' THEN TongTien * UuDai.GiaTri / 100 ELSE 0 END " +
                          "FROM HoaDon, UuDai " +
                          "WHERE MaUuDai = '" + maUD + "' AND MaHoaDon = '"+maHD+"';";
            if (!ControllerExcution.Excution(query)) return false;
            query = "UPDATE HoaDon SET TongTien = TongTien - GiamGia FROM HoaDon " +
                    "WHERE MaHoaDon = '"+maHD+"'";
            if (!ControllerExcution.Excution(query)) return false;
            return true;
        }
        public static void SetUuDai(string maUD, string maKH)
        {
            string query = "UPDATE ChiTietKhachHangUuDai SET SoLuong = SoLuong - 1" +
                    " WHERE MaKhachHang = '" + maKH + "' AND MaUuDai = '" + maUD + "'; " +
                    "DELETE FROM ChiTietKhachHangUuDai " +
                    "WHERE MaKhachHang = '" + maKH + "' AND MaUuDai = '" + maUD + "' AND SoLuong = 0";
            ControllerExcution.Excution(query);
        }
        public static void SetDiemTichLuy(string maKH,string maHD)
        {
            string sql = "UPDATE KhachHang " +
             "SET DiemTichLuy = DiemTichLuy + CONVERT(INT, (TongTien / 10000)) " +
             "FROM KhachHang " +
             "INNER JOIN HoaDon ON KhachHang.MaKhachHang = HoaDon.MaKhachHang " +
             "WHERE KhachHang.MaKhachHang = '"+maKH+"' AND HoaDon.MaHoaDon = '"+maHD+"';";
            ControllerExcution.Excution(sql);
            sql = "SELECT CONVERT(INT, TongTien/10000) FROM HoaDon WHERE MaHoaDon = '" + maHD + "';";
            Console.WriteLine("\tBan duoc cong them ".Yellow()+"{0}".Red(),ControllerExcution.ExcutionInt(sql)+" diem tich luy".Green());
            sql = "SELECT DiemTichLuy FROM KhachHang WHERE MaKhachHang = '" + maKH + "';";
            Console.WriteLine("\tDiem tich luy hien tai cua ban la:".Green()+" {0}".Red(), ControllerExcution.ExcutionInt(sql));
        }
    }
}
