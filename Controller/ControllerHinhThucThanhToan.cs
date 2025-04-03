using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerHinhThucThanhToan
    {
        public static void HinhThucThanhToan(string maHD)
        {
            int hinhThucThanhToan;
            do
            {
                Console.WriteLine("\t\tChọn hình thức thanh toán".Yellow());
                Console.WriteLine("\t\t1. Tiền mặt".Green());
                Console.WriteLine("\t\t2. Chuyển khoản".Green());
                Console.Write("\tNhập lựa chọn của bạn (1 hoặc 2): ".Magenta());
                string input = Console.ReadLine();
                if (!int.TryParse(input, out hinhThucThanhToan))
                {
                    Console.WriteLine("\t\t\tLựa chọn không hợp lệ. Vui lòng nhập lại.".Yellow());
                    hinhThucThanhToan = 0;
                }
            } while (hinhThucThanhToan != 1 && hinhThucThanhToan != 2);
            if (hinhThucThanhToan == 1)
            {
                string query = "UPDATE HoaDon SET HinhThucThanhToan = 'Tien mat' WHERE MaHoaDon = '" + maHD + "';";
                ControllerExcution.Excution(query);
            }
            else
            {
                string query = "UPDATE HoaDon SET HinhThucThanhToan = 'Chuyen khoan' WHERE MaHoaDon = '" + maHD + "';";
                ControllerExcution.Excution(query);
            }
        }
    }
}
