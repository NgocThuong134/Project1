using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerSum
    {
        public static void Sum(string maHoaDon, string maMonAn)
        {
            string query = "SELECT COUNT(*) FROM ChiTietHoaDonMonAn " +
                           "WHERE MaHoaDon = '" + maHoaDon + "' AND MaMonAn = '" + maMonAn + "';";
            if (ControllerExcution.Excution(query))
            {
                query = "UPDATE ChiTietHoaDonMonAn SET TongTien = SoLuong*DonGia-GiamGia WHERE MaHoaDon = '" + maHoaDon + "' AND MaMonAn = '" + maMonAn + "';";
                ControllerExcution.Excution(query);
            }
            query = "UPDATE HoaDon SET " +
                "GiamGia = (SELECT SUM(GiamGia) FROM ChiTietHoaDonMonAn WHERE MaHoaDon = '" + maHoaDon + "') " +
                "WHERE MaHoaDon = '"+maHoaDon+"';";
            ControllerExcution.Excution(query);
            query = "UPDATE HoaDon SET TongTien = ( " +
                                    " SELECT SUM(TongTien) " +
                                    " FROM ChiTietHoaDonMonAn " +
                                    " WHERE MaHoaDon = '" + maHoaDon + "') " +
                                "WHERE MaHoaDon = '" + maHoaDon + "';";
            ControllerExcution.Excution(query);
        }
    }
}
