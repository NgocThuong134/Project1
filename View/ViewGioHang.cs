using ConsoleGUI.Controls;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewGioHang
    {
        public static void ThanhTien(string maHD)
        {
            string query = "SELECT GiamGia,TongTien FROM HoaDon WHERE MaHoaDon = '" + maHD + "';";
            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                double giamGia = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
                                double thanhTien = reader.IsDBNull(0) ? 0 : reader.GetDouble(1);
                                Console.WriteLine("\t\tGiảm giá: {0} VNĐ".Yellow(), giamGia.ToString("N0").Red());
                                Console.WriteLine("\t\tThành tiền: {0} VNĐ".Green(), thanhTien.ToString("N0").Red());
                            }
                        }
                    }
                }
            }
        }
        public static bool GioHang(string Id)
        {
                List<string> maHoaDonList = new List<string>();

                string query = "SELECT MaHoaDon " +
                               "FROM HoaDon " +
                               "WHERE TrangThai = 0 AND MaKhachHang = '" + Id + "';";

                using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string maHoaDon = reader.GetString(0);
                                maHoaDonList.Add(maHoaDon);
                            }
                        }
                    }
                }
            if (maHoaDonList.Count == 0)
            {
                Console.WriteLine("\t\tBạn chưa có đơn hàng nào...".Yellow());
                return false;
            }
            else
            {
                foreach (string MaHD in maHoaDonList)
                {
                    query = "SELECT MaHoaDon, ThoiGianNhap " +
                                "FROM HoaDon " +
                                "WHERE MaHoaDon = '" + MaHD + "';";
                    ControllerView.Print(query);
                    query = "SELECT MA.TenMonAn, MA.LoaiMonAn, CTHDMA.ChuThich, CTHDMA.SoLuong, CTHDMA.DonGia, CTHDMA.DonViTinh, CTHDMA.GiamGia, CTHDMA.TongTien " +
                            "FROM HoaDon HD " +
                                 "INNER JOIN ChiTietHoaDonMonAn CTHDMA ON HD.MaHoaDon = CTHDMA.MaHoaDon " +
                                 "INNER JOIN MonAn MA ON CTHDMA.MaMonAn = MA.MaMonAn " +
                                 "WHERE HD.MaHoaDon = '" + MaHD + "' " +
                                 "ORDER BY MA.TenMonAn ASC;";
                    int currentTop = Console.CursorTop;
                    Console.SetCursorPosition(0, currentTop - 2);
                    ControllerView.Print(query);
                    ThanhTien(MaHD);
                    Console.WriteLine("\t------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
                return true;
            }
        }
        public static void ViewDonHang(string maHD)
        {
            string query = "SELECT ma.TenMonAn, ma.LoaiMonAn, ma.DonGia, ma.DonViTinh, cthd.SoLuong, cthd.GiamGia, cthd.TongTien " +
                                          "FROM MonAn ma " +
                                          "INNER JOIN ChiTietHoaDonMonAn cthd ON ma.MaMonAn = cthd.MaMonAn " +
                                          "INNER JOIN HoaDon hd ON cthd.MaHoaDon = hd.MaHoaDon " +
                                          "WHERE cthd.MaHoaDon = '" + maHD + "';";
            ControllerView.Print(query);
            ThanhTien(maHD);
        }
        public static void LichSu(string Id)
        {
            List<string> maHoaDonList = new List<string>();

            string query = "SELECT MaHoaDon " +
                           "FROM HoaDon " +
                           "WHERE TrangThai = 1 AND MaKhachHang = '" + Id + "' AND  TongTien > 0;";

            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maHoaDon = reader.GetString(0);
                            maHoaDonList.Add(maHoaDon);
                        }
                    }
                }
            }
            if (maHoaDonList.Count == 0)
                Console.WriteLine("\t\tBạn chưa có đơn hàng nào...".Yellow());
            else
            {
                foreach (string MaHD in maHoaDonList)
                {
                    query = "SELECT HD.MaHoaDon, HD.ThoiGianNhap, HD.ThoiGianXuat, HD.LoaiHoaDon, BA.TenBanAn, KH.TenKhachHang, NV.TenNhanVien, HD.HinhThucThanhToan " +
                                "FROM HoaDon HD " +
                                "LEFT JOIN BanAn BA ON HD.MaBanAn = BA.MaBanAn " +
                                "LEFT JOIN NhanVien NV ON HD.MaNhanVien = NV.MaNhanVien " +
                                "LEFT JOIN KhachHang KH ON HD.MaKhachHang = KH.MaKhachHang " +
                                "WHERE HD.MaHoaDon = '"+MaHD+"';";
                    ControllerView.Print(query);
                    query = "SELECT MA.TenMonAn, MA.LoaiMonAn, CTHDMA.ChuThich, CTHDMA.SoLuong, CTHDMA.DonGia, CTHDMA.DonViTinh, CTHDMA.GiamGia, CTHDMA.TongTien " +
                            "FROM HoaDon HD " +
                                 "INNER JOIN ChiTietHoaDonMonAn CTHDMA ON HD.MaHoaDon = CTHDMA.MaHoaDon " +
                                 "INNER JOIN MonAn MA ON CTHDMA.MaMonAn = MA.MaMonAn " +
                                 "WHERE HD.MaHoaDon = '"+MaHD+"' " +
                                 "ORDER BY MA.TenMonAn ASC;";
                    int currentTop = Console.CursorTop;
                    Console.SetCursorPosition(0, currentTop - 2);
                    ControllerView.Print(query);
                    ThanhTien(MaHD);
                    Console.WriteLine("\t-------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
            }
        }
    }
}
