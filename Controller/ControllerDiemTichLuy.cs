using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerDiemTichLuy
    {
        public static bool AddDTL(string maHD,int DiemTichLuy)
        {
            string query = "UPDATE HoaDon SET TongTien = TongTien - " + DiemTichLuy +
                "WHERE MaHoaDon = '" + maHD + "';";
            ControllerExcution.Excution(query);
            query = "UPDATE HoaDon SET GiamGia = GiamGia + CAST("+DiemTichLuy+" AS FLOAT) " +
                           "WHERE MaHoaDon = '" + maHD + "';";
            if (!ControllerExcution.Excution(query)) return false;
            return true;
        }
        public static void SetDTL(int DiemTichLuy,string maKH)
        {
            string query = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy - " + DiemTichLuy +
                           " WHERE MaKhachHang = '" + maKH + "';";
            ControllerExcution.Excution(query);
        }
        public static int DiemTichLuy(string maKH)
        {
            string sql = "SELECT DiemTichLuy " +
                         "FROM KhachHang " +
                         "WHERE MaKhachHang = '" + maKH + "';";
            return ControllerExcution.ExcutionInt(sql);
        }
        
    }
}
