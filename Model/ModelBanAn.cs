using ConsoleGUI.Data;
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
    internal class ModelBanAn
    {
        public static List<SqlParameter> ThongTinBanAn(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenBanAn,SoChoNgoi,ChuThich,TrangThai FROM BanAn WHERE MaBanAn = '" + id + "';");
            string input;
            Console.Write("\tNhập tên bàn ăn: ".Magenta());
            string tenBanAn = Console.ReadLine();

            Console.Write("\tNhập số chỗ ngồi: ".Magenta());
            int soChoNgoi;
            int.TryParse(Console.ReadLine(), out soChoNgoi);
            string chuThich = null;
            Console.Write("\tNhập chú thích: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                chuThich = input;
            }

            bool trangThai = false;
            Console.Write("\tNhập trạng thái (1: đang dùng, 0: trống): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                trangThai = (input == "1");
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(tenBanAn))
            {
                query += "TenBanAn = @tenBanAn, ";
                parameters.Add(new SqlParameter("@tenBanAn", tenBanAn));
            }
            if (soChoNgoi > 0)
            {
                query += "SoChoNgoi = @soChoNgoi, ";
                parameters.Add(new SqlParameter("@soChoNgoi", soChoNgoi));
            }
            if (!string.IsNullOrEmpty(chuThich))
            {
                query += "ChuThich = @chuThich, ";
                parameters.Add(new SqlParameter("@chuThich", chuThich));
            }
            query += "TrangThai = @trangThai ";
            if (!string.IsNullOrEmpty(query))
            {
                parameters.Add(new SqlParameter("@trangThai", trangThai));
                parameters.Add(new SqlParameter("@id", id));
                parameters.Add(new SqlParameter("query", query));
                return parameters;
            }
            else return null;
        }
        public static List<SqlParameter> ThongTinCapNhatBanAn()
        {
            Console.WriteLine("* Bắt buộc nhập!".Yellow());
            string maBanAn = ControllerGenerate.Generate("BA", "BanAn", "MaBanAn");
            string tenBanAn;
            do
            {
                Console.Write("\tTên bàn ăn (*): ".Magenta());
                tenBanAn = Console.ReadLine();
            } while (string.IsNullOrEmpty(tenBanAn));

            int soChoNgoi;
            string input;
            do
            {
                Console.Write("\tSố chỗ ngồi (*): ".Magenta());
                input = Console.ReadLine();
            } while (!int.TryParse(input, out soChoNgoi));

            Console.Write("\tChú thích: ".Magenta());
            string chuThich = Console.ReadLine();

            bool trangThai = true;
            Console.Write("\tTrạng thái (0 - bàn trống, 1 - bàn đang dùng): ".Magenta());
            input = Console.ReadLine();
            if (input == "1") trangThai = false;

            string query = "INSERT INTO BanAn (MaBanAn, TenBanAn, SoChoNgoi, ChuThich, TrangThai)" +
                           "VALUES (@MaBanAn, @TenBanAn, @SoChoNgoi, @ChuThich, @TrangThai)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaBanAn", maBanAn));
            parameters.Add(new SqlParameter("@TenBanAn", tenBanAn));
            parameters.Add(new SqlParameter("@SoChoNgoi", soChoNgoi));
            parameters.Add(new SqlParameter("@ChuThich", string.IsNullOrEmpty(chuThich) ? (object)DBNull.Value : chuThich));
            parameters.Add(new SqlParameter("@TrangThai", trangThai));
            parameters.Add(new SqlParameter("query", query));
            return parameters;
        }
        public static bool DatBan(string id)
        {
            Console.WriteLine("\t\t\t(*) Bắt buộc nhập!".Red());
            DateTime time;
            do
            {
                Console.Write("\tNhập thời gian đặt bàn (dd/mm/yyyy hh:mm)(*): ".Magenta());
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out time))
                {
                    if (time <= DateTime.Now)
                    {
                        Console.WriteLine("\t\tThời gian đặt bàn phải lớn hơn thời gian hiện tại...".Yellow());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("\t\tBạn cần nhập đúng định dạng thời gian...".Yellow());
                }

            } while (true);

            int soLuong;
            do
            {
                Console.Write("\tNhập số lượng người (>1)(*): ".Magenta());
                string input = Console.ReadLine();
                if (int.TryParse(input, out soLuong))
                {
                    if (soLuong < 1)
                    {
                        Console.WriteLine("\t\tSố lượng người phải lớn hơn 1...".Yellow());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("\t\tBạn cần nhập số nguyên dương...".Yellow());
                }

            } while (true);
            string query = "SELECT MaBanAn, TenBanAn FROM BanAn WHERE TrangThai = 1 AND SoChoNgoi >= " + soLuong + " ;";
            if (!ControllerView.Print(query))
                return false;
            string maBanAn = null;
            bool ok;
            do
            {
                Console.Write("\tNhập mã bàn ăn bạn muốn đặt: ".Magenta());
                maBanAn = Console.ReadLine().ToUpper();
                query = "SELECT COUNT(*) FROM BanAn WHERE TrangThai = 1 AND MaBanAn = '" + maBanAn + "';";
                ok = ControllerExcution.Excution(query);
                if (!ok)
                    Console.WriteLine("\t\tMã bàn ăn không tồn tại. Vui lòng nhập lại...".Yellow());
            } while (!ok);
            string maHoaDon = ControllerGenerate.Generate("HD", "HoaDon", "MaHoaDon");
            ModelHD.KHTaoHoaDon(maHoaDon, id, maBanAn);
            query = "UPDATE BanAn SET TrangThai = 0 WHERE MaBanAn = '" + maBanAn + "';";
            ControllerExcution.Excution(query);
            Console.Write("\tNhập chú thích bàn ăn (nếu có): ".Magenta());
            string chuThich = Console.ReadLine();
            chuThich = time.ToString() + " " + soLuong.ToString() + " " + chuThich;
            query = "UPDATE BanAn SET ChuThich = '" + chuThich + "' WHERE MaBanAn = '" + maBanAn + "';";
            ControllerExcution.Excution(query);
            if (ControllerXacNhan.IsXacNhan("Bạn có muốn đặt món ăn trước hay không?"))
                ModelNhapMon.NhapMon(maHoaDon);
            return true;
        }
        public static bool HuyDatBan(string id)
        {
            string query = "SELECT BanAn.MaBanAn, TenBanAn, ChuThich " +
                           "FROM BanAn JOIN HoaDon ON BanAn.MaBanAn = HoaDon.MaBanAn " +
                           "WHERE BanAn.TrangThai = 0 AND MaKhachHang = '" + id + "'; ";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\t\t\tBạn chưa đặt bàn...".Yellow());
                return false;
            }
            string maBanAn = null;
            bool ok;
            do
            {
                Console.Write("\tNhập mã bàn ăn bạn muốn hủy: ".Magenta());
                maBanAn = Console.ReadLine().ToUpper();
                query = "SELECT COUNT(*) FROM BanAn WHERE TrangThai = 0 AND MaBanAn = '" + maBanAn + "';";
                ok = ControllerExcution.Excution(query);
                if (!ok)
                    Console.WriteLine("\t\tMã bàn ăn không tồn tại. Vui lòng nhập lại...".Yellow());
            } while (!ok);
            if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn hủy bàn " + maBanAn + " không?"))
            {
                query = "SELECT MaHoaDon FROM HoaDon JOIN BanAn ON HoaDon.MaBanAn = BanAn.MaBanAn " +
                       "WHERE MaKhachHang = '" + id + "' AND HoaDon.MaBanAn = '" + maBanAn + "' AND HoaDon.TrangThai = 0;";
                string maHoaDon = ControllerExcution.ExcutionString(query);
                query = "DELETE FROM ChiTietHoaDonMonAn WHERE MaHoaDon = '" + maHoaDon + "';";
                ControllerExcution.Excution(query);
                query = "DELETE FROM HoaDon WHERE MaHoaDon = '" + maHoaDon + "';";
                ControllerExcution.Excution(query);
                query = "UPDATE BanAn SET TrangThai = 1 WHERE MaBanAn = '" + maBanAn + "';";
                ControllerExcution.Excution(query);
                return true;
            }
            else
            {
                Console.WriteLine("\t\tBạn hủy bỏ thao tác...".Yellow());
                return false;
            }
        }
        public static bool XoaBanAn()
        {
            string query = "SELECT MaBanAn, TenBanAn, SoChoNgoi FROM BanAn;";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\t\t\tKhông tìm thấy bàn ăn nào.".Yellow());
                return false;
            }
            else
            {
                string maBanAn;
                bool ok;
                do
                {
                    Console.Write("\tNhập mã bàn ăn muốn xóa: ".Magenta());
                    maBanAn = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM BanAn WHERE MaBanAn = '" + maBanAn + "';";
                    ok = ControllerExcution.Excution(query);
                } while (!ok);

                if (ControllerXacNhan.IsXacNhan("Bạn có chắc muốn xóa bàn ăn này ?"))
                {
                    query = "DELETE FROM BanAn WHERE MaBanAn = '" + maBanAn + "';";
                    ControllerExcution.Excution(query);
                    Console.WriteLine("\tĐã xóa bàn ăn có mã {0}".Green(),maBanAn);
                    return true;
                }
                else
                {
                    Console.WriteLine("\tKhông xóa bàn ăn có mã {0}".Red(),maBanAn);
                    return false;
                }
            }
        }
    }
}
