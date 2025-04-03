using CSharpExtensions.OpenSource.ConsoleColors;
using Microsoft.VisualBasic.FileIO;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelUD
    {
        public static List<SqlParameter> ThongTinUuDai(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenUuDai,ThoiGianBatDau,ThoiGianKetThuc," +
                "MoTa,DiemTichLuy,CachThucDangKy FROM UuDai WHERE MaUuDai = '" + id + "';");
            string tenUuDai = null;
            Console.Write("\tNhập tên ưu đãi: ".Magenta());
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                tenUuDai = input;
            }

            DateTime? thoiGianBatDau = null;
            Console.Write("\tNhập thời gian bắt đầu (yyyy-MM-dd HH:mm:ss): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                thoiGianBatDau = DateTime.Parse(input);
            }

            DateTime? thoiGianKetThuc = null;
            Console.Write("\tNhập thời gian kết thúc (yyyy-MM-dd HH:mm:ss): ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                thoiGianKetThuc = DateTime.Parse(input);
            }

            float giaTri = 0;
            Console.Write("\tNhập giá trị: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                giaTri = float.Parse(input);
            }

            string donViTinh = null;

            Console.WriteLine("\tChọn đơn vị tính:".Green());
            Console.WriteLine("\t\t1. %".Blue());
            Console.WriteLine("\t\t2. VNĐ".Blue());
            Console.Write("\tNhập lựa chọn của bạn: ".Magenta());
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    donViTinh = "%";
                    break;
                case "2":
                    donViTinh = "VNĐ";
                    break;
            }

            string moTa = null;
            Console.Write("\tNhập mô tả: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                moTa = input;
            }

            string dieuKien = null;
            Console.Write("\tNhập điều kiện áp dụng: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                dieuKien = input;
            }
            int DiemTichLuy = 0;
            Console.Write("\tNhập điểm tích lũy: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                DiemTichLuy = int.Parse(input);
            }
            string cachThucDangKy = null;
            Console.Write("\tNhập cách thức đăng ký: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                cachThucDangKy = input;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(tenUuDai))
            {
                query += "TenUuDai = @tenUuDai, ";
                parameters.Add(new SqlParameter("@tenUuDai", tenUuDai));
            }
            if (thoiGianBatDau != null)
            {
                query += "ThoiGianBatDau = @thoiGianBatDau, ";
                parameters.Add(new SqlParameter("@thoiGianBatDau", thoiGianBatDau));
            }
            if (thoiGianKetThuc != null)
            {
                query += "ThoiGianKetThuc = @thoiGianKetThuc, ";
                parameters.Add(new SqlParameter("@thoiGianKetThuc", thoiGianKetThuc));
            }
            if (giaTri > 0)
            {
                query += "GiaTri = @giaTri, ";
                parameters.Add(new SqlParameter("@giaTri", giaTri));
            }
            if (!string.IsNullOrEmpty(donViTinh))
            {
                query += "DonViTinh = @donViTinh, ";
                parameters.Add(new SqlParameter("@donViTinh", donViTinh));
            }
            if (!string.IsNullOrEmpty(moTa))
            {
                query += "MoTa = @moTa, ";
                parameters.Add(new SqlParameter("@moTa", moTa));
            }
            if (!string.IsNullOrEmpty(dieuKien))
            {
                query += "DieuKien = @dieuKien, ";
                parameters.Add(new SqlParameter("@dieuKien", dieuKien));
            }
            if (DiemTichLuy > 0)
            {
                query += "DiemTichLuy = @DiemTichLuy, ";
                parameters.Add(new SqlParameter("@DiemTichLuy", DiemTichLuy));
            }
            if (!string.IsNullOrEmpty(cachThucDangKy))
            {
                query += "CachThucDangKy = @cachThucDangKy, ";
                parameters.Add(new SqlParameter("@cachThucDangKy", cachThucDangKy));
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
        public static List<SqlParameter> ThongTinCapNhatUuDai()
        {
            string maUuDai = ControllerGenerate.Generate("UD", "UuDai", "MaUuDai");

            string tenUuDai;
            do
            {
                Console.Write("\tNhập tên ưu đãi: ".Magenta());
                tenUuDai = Console.ReadLine();
            } while (string.IsNullOrEmpty(tenUuDai));

            DateTime? thoiGianBatDau = null;
            Console.Write("\tNhập thời gian bắt đầu (dd-mm-yyyy hh:mm:ss): ".Magenta());
            string thoiGianBatDauStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(thoiGianBatDauStr))
            {
                thoiGianBatDau = DateTime.Parse(thoiGianBatDauStr);
            }

            DateTime? thoiGianKetThuc = null;
            Console.Write("\tNhập thời gian kết thúc (dd-mm-yyyy hh:mm:ss): ".Magenta());
            string thoiGianKetThucStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(thoiGianKetThucStr))
            {
                thoiGianKetThuc = DateTime.Parse(thoiGianKetThucStr);
            }

            float? giaTri = null;
            string giaTriStr;
            float temp;
            do
            {
                Console.Write("\tNhập giá trị: ".Magenta());
                giaTriStr = Console.ReadLine();
            } while (string.IsNullOrEmpty(giaTriStr) || !float.TryParse(giaTriStr, out temp));
            giaTri = temp;

            string donViTinh = "VNĐ";
            Console.WriteLine("\tChọn đơn vị tính:".Yellow());
            Console.WriteLine("\t\t1. %".Green());
            Console.WriteLine("\t\t2. VNĐ".Green());
            Console.Write("\tNhập lựa chọn của bạn: ".Magenta());
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    donViTinh = "%";
                    break;
                case "2":
                    donViTinh = "VNĐ";
                    break;
            }

            string moTa = null;
            Console.Write("\tNhập mô tả: ".Magenta());
            moTa = Console.ReadLine();

            string dieuKien = null;
            Console.Write("\tNhập điều kiện: ".Magenta());
            dieuKien = Console.ReadLine();

            int diemTichLuy = 0;
            string DTL = null;
            Console.Write("\tNhập điểm tích lũy: ".Magenta());
            DTL = Console.ReadLine();
            if (!string.IsNullOrEmpty(DTL))
            {
                diemTichLuy = int.Parse(DTL);
            }

            string cachThucDangKy = null;
            Console.Write("\tNhập cách thức đăng ký: ".Magenta());
            cachThucDangKy = Console.ReadLine();

            string query = "INSERT INTO UuDai (MaUuDai, TenUuDai, ThoiGianBatDau, ThoiGianKetThuc, GiaTri," +
                " DonViTinh, MoTa, DieuKien, DiemTichLuy, CachThucDangKy) " +
                             "VALUES (@MaUuDai, @TenUuDai, @ThoiGianBatDau, @ThoiGianKetThuc, @GiaTri, @DonViTinh," +
                             " @MoTa, @DieuKien, @DiemTichLuy, @CachThucDangKy)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaUuDai", maUuDai));
            parameters.Add(new SqlParameter("@TenUuDai", tenUuDai));
            parameters.Add(new SqlParameter("@ThoiGianBatDau", thoiGianBatDau.HasValue ? (object)thoiGianBatDau.Value : DBNull.Value));
            parameters.Add(new SqlParameter("@ThoiGianKetThuc", thoiGianKetThuc.HasValue ? (object)thoiGianKetThuc.Value : DBNull.Value));
            parameters.Add(new SqlParameter("@GiaTri", giaTri.HasValue ? (object)giaTri.Value : DBNull.Value));
            parameters.Add(new SqlParameter("@DonViTinh", donViTinh));
            parameters.Add(new SqlParameter("@MoTa", moTa));
            parameters.Add(new SqlParameter("@DieuKien", dieuKien));
            parameters.Add(new SqlParameter("@DiemTichLuy", diemTichLuy));
            parameters.Add(new SqlParameter("@CachThucDangKy", cachThucDangKy));
            parameters.Add(new SqlParameter("query", query));

            return parameters;
        }
        public static void HinhThucUuDai(int option, string id, string MaHD)
        {
            bool ok = true;
            bool kt = true;
            while (ok)
            {
                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("\t\tBạn đã chọn hình thức sử dụng mã ưu đãi.".Green());
                            string maUD;
                            maUD = ControllerUuDai.UuDaiMax(id, MaHD);
                            if (maUD == null)
                            {
                                Console.WriteLine("\tBạn không có mã ưu đãi phù hợp!".Yellow());
                                if (kt == true && ControllerXacNhan.IsXacNhan("Bạn có muốn sử dụng điểm tích lũy không?"))
                                {
                                    option = 2;
                                    kt = false;
                                }
                                else ok = false;
                            }
                            else if (ControllerUuDai.AddUuDai(maUD, MaHD, id))
                            {
                                Console.WriteLine("\t\tÁp dụng mã ưu đãi ".Green()+maUD.Red()+" thành công!".Green());
                                ControllerUuDai.SetUuDai(maUD, id);
                                ok = false;
                            }
                            else
                            {
                                Console.WriteLine("\t\tÁp dụng mã ưu đãi thất bại!".Red());
                                Console.WriteLine("\tHãy kiểm tra lại mã ưu đãi của bạn!".Yellow());
                                ok = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("\tBạn đã chọn hình thức sử dụng điểm tích lũy.".Green());
                            int diemTichLuy = ControllerDiemTichLuy.DiemTichLuy(id);
                            Console.WriteLine("\tĐiểm tích lũy của bạn là: {0}".Green(), diemTichLuy);
                            Console.WriteLine("\t\t------------ 100 điểm tích lũy = 1.000 đồng ------------".Red());
                            diemTichLuy = diemTichLuy / 100;
                            if (diemTichLuy > 0 && ControllerDiemTichLuy.AddDTL(MaHD, diemTichLuy * 1000))
                            {
                                Console.WriteLine("\tBạn đã được giảm giá {0} đồng trên tổng tiền hóa đơn.".Green(), diemTichLuy * 1000);
                                ControllerDiemTichLuy.SetDTL(diemTichLuy * 100, id);
                                ok = false;
                            }
                            else
                            {
                                Console.WriteLine("\t\tĐiểm tích lũy của bạn không đủ!".Yellow());
                                if (kt == true && ControllerXacNhan.IsXacNhan("Bạn có muốn sử dụng mã ưu đãi khác không?"))
                                {
                                    option = 1;
                                    kt = false;
                                }
                                else ok = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("\t\t\tBạn không sử dụng bất kỳ ưu đãi nào.".Yellow());
                            ok = false;
                            break;
                        }
                        Console.ReadKey();
                }
            }
        }
        public static void DoiDiemUuDai(string id)
        {
            do
            {
                Console.Clear();
                string query = "SELECT DiemTichLuy FROM KhachHang WHERE MaKhachHang = '" + id + "';";
                int DiemTichLuy = ControllerExcution.ExcutionInt(query);
                query = "SELECT MaUuDai, ThoiGianBatDau, ThoiGianKetThuc, MoTa, DiemTichLuy FROM UuDai WHERE DiemTichLuy > 0 AND DiemTichLuy <= " + DiemTichLuy + ";";
                if (ControllerView.Print(query))
                {
                    Console.WriteLine();
                    Console.WriteLine("\tĐiểm tích lũy của bạn là: {0}".Green(), DiemTichLuy.ToString().Yellow());
                    string maUuDai;
                    Console.Write("\tNhập mã ưu đãi bạn muốn đổi: ".Magenta());
                    maUuDai = Console.ReadLine();
                    query = "SELECT COUNT(*) FROM UuDai WHERE DiemTichLuy < " + DiemTichLuy + " AND MaUuDai = '" + maUuDai + "';";
                    if (ControllerExcution.Excution(query))
                    {
                        Console.WriteLine("\tĐổi mã ưu đãi thành công!".Green());
                        query = "MERGE INTO ChiTietKhachHangUuDai AS target "
                                + "USING(VALUES('" + id + "', '" + maUuDai + "', 1)) AS source(MaKhachHang, MaUuDai, SoLuong) "
                                + "ON target.MaKhachHang = source.MaKhachHang AND target.MaUuDai = source.MaUuDai "
                                + "WHEN MATCHED THEN "
                                + "UPDATE SET target.SoLuong = target.SoLuong + source.SoLuong "
                                + "WHEN NOT MATCHED THEN "
                                + "INSERT(MaKhachHang, MaUuDai, SoLuong) VALUES(source.MaKhachHang, source.MaUuDai, source.SoLuong);";
                        ControllerExcution.Excution(query);
                        query = "SELECT DiemTichLuy FROM UuDai WHERE MaUuDai = '" + maUuDai + "';";
                        int DTL = ControllerExcution.ExcutionInt(query);
                        query = "UPDATE KhachHang SET DiemTichLuy = DiemTichLuy - " + DTL + " WHERE MaKhachHang = '" + id + "';";
                        ControllerExcution.Excution(query);
                    }
                    else
                    {
                        Console.WriteLine("\tĐổi mã ưu đãi không thành công...".Red());
                    }
                }
                else Console.WriteLine("\t\tBạn không đủ điểm tích lũy để đổi ưu đãi...".Red());
            } while (ControllerXacNhan.IsXacNhan("Bạn có muốn tiếp tục không?"));
        }
        public static bool XoaUuDai()
        {
            string query = "SELECT MaUuDai, TenUuDai, ThoiGianBatDau, ThoiGianKetThuc, GiaTri, DonViTinh, DieuKien, CachThucDangKy, DiemTichLuy FROM UuDai;";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\t\tKhông tìm thấy ưu đãi nào.".Yellow());
                return false;
            }
            else
            {
                bool ok;
                string maUuDai;
                do
                {
                    Console.Write("\tNhập mã ưu đãi bạn muốn xóa: ".Magenta());
                    maUuDai = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM UuDai WHERE MaUuDai = '" + maUuDai + "';";
                    ok = ControllerExcution.Excution(query);
                } while (!ok);
                if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn muốn xóa ưu đãi này?"))
                {
                    query = "DELETE FROM ChiTietKhachHangUuDai WHERE MaUuDai = '" + maUuDai + "';";
                    ControllerExcution.Excution(query);
                    query = "DELETE FROM UuDai WHERE MaUuDai = '" + maUuDai + "';";
                    ControllerExcution.Excution(query);
                    Console.WriteLine("\t\t\tĐã xóa ưu đãi có mã {0}".Green(), maUuDai);
                    return true;
                }
                else
                {
                    Console.WriteLine("\t\t\tKhông xóa ưu đãi có mã {0}".Red(), maUuDai);
                    return false;
                }
            }
        }
    }
}