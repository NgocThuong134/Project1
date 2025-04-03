using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewPhucVu
    {
        public virtual void NhapMon(string MaNhanVien)
        {
            string MaHoaDon = ControllerGenerate.Generate("HD", "HoaDon", "MaHoaDon");
            string query = "SELECT MaBanAn, TenBanAn, SoChoNgoi FROM BanAn WHERE TrangThai = 1;";
            if (ControllerView.Print(query))
            {
                string MaBanAn = null;
                while (string.IsNullOrEmpty(MaBanAn))
                {
                    Console.Write("\tNhập mã bàn ăn: ".Magenta());
                    MaBanAn = Console.ReadLine().Trim().ToUpper();
                    query = "SELECT COUNT(*) FROM BanAn WHERE TrangThai = 1 AND MaBanAn = '" + MaBanAn + "';";
                    if (string.IsNullOrEmpty(MaBanAn))
                    {
                        Console.WriteLine("\t\tMã bàn ăn không được để trống. Vui lòng nhập lại.".Yellow());
                    }
                    else if (!ControllerExcution.Excution(query))
                    {
                        Console.WriteLine("\t\tMã bàn ăn này không hợp lệ. Vui lòng nhập lại.".Red());
                        MaBanAn = null;
                    }
                }

                ControllerView.Print("SELECT MaKhachHang,TenKhachHang,CONVERT(varchar, NgaySinh, 103) as NgaySinh,GioiTinh,Email,SoDienThoai,DiaChi,HangKhachHang FROM KhachHang;");
                string id = null;
                bool ok = true;
                do
                {
                    Console.Write("\tNhập mã khách hàng (nếu có): ".Magenta());
                    id = Console.ReadLine().ToUpper();
                    if (!string.IsNullOrEmpty(id))
                    {
                        query = "SELECT COUNT(*) FROM KhachHang WHERE MaKhachHang = '" + id + "';";
                        if (!ControllerExcution.Excution(query))
                            Console.WriteLine("\t\tMã khách hàng không tồn tại. Vui lòng nhập lại.".Red());
                        else ok = false;
                    }
                    else { ok = false; }
                } while (ok == true);
                ModelHD.NVTaoHoaDon(MaHoaDon, id, MaNhanVien, MaBanAn);
                query = "UPDATE BanAn SET TrangThai = 0 WHERE MaBanAn = '" + MaBanAn + "';";
                ControllerExcution.Excution(query);
                Console.Write("\tNhập chú thích bàn ăn (nếu có): ".Magenta());
                string chuthich = Console.ReadLine();
                if (!string.IsNullOrEmpty(chuthich))
                {
                    query = "UPDATE BanAn SET ChuThich = '" + chuthich + "' WHERE MaBanAn = '" + MaBanAn + "';";
                    ControllerExcution.Excution(query);
                }
                ModelNhapMon.NhapMon(MaHoaDon);
            }
            else Console.WriteLine("\t\tKhông có bàn trống...".Red());
        }
        public virtual void ChinhSua(string MaNV)
        {
            List<string> maBanAnList = new List<string>();

            string query = "SELECT BanAn.MaBanAn, BanAn.TenBanAn " +
                           "FROM BanAn INNER JOIN HoaDon ON BanAn.MaBanAn = HoaDon.MaBanAn " +
                           "WHERE BanAn.TrangThai = 0 AND HoaDon.TrangThai = 0 ;";

            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maBanAn = reader.GetString(0);
                            maBanAnList.Add(maBanAn);
                        }
                    }
                }
            }

            if (maBanAnList.Count > 0)
            {
                foreach (string maBanAn in maBanAnList)
                {
                    query = "SELECT MaBanAn,TenBanAn,ChuThich FROM BanAn WHERE MaBanAn = '" + maBanAn + "';";
                    ControllerView.Print(query);
                    query = "SELECT ma.MaMonAn, ma.TenMonAn, ct.SoLuong, ct.DonGia, ct.DonViTinh, ba.ChuThich,ct.GiamGia, ct.TongTien " +
                            "FROM BanAn ba " +
                            "INNER JOIN HoaDon hd ON ba.MaBanAn = hd.MaBanAn " +
                            "INNER JOIN ChiTietHoaDonMonAn ct ON hd.MaHoaDon = ct.MaHoaDon " +
                            "INNER JOIN MonAn ma ON ct.MaMonAn = ma.MaMonAn " +
                            "WHERE hd.TrangThai = 0 AND ba.MaBanAn = '" + maBanAn + "' " +
                            "ORDER BY ba.MaBanAn ASC";
                    int currentTop = Console.CursorTop;
                    Console.SetCursorPosition(0, currentTop - 2);
                    ControllerView.Print(query);
                    query = "SELECT MaHoaDon FROM HoaDon WHERE MaBanAn = '" + maBanAn + "';";
                    string maHoaDon = ControllerExcution.ExcutionString(query);
                    ViewGioHang.ThanhTien(maHoaDon);
                    Console.WriteLine("\t\t\t------------------------------------------------------------------------------------------------------------------------------------");
                }

                string maBA = null;
                do
                {
                    Console.Write("\t\tNhập mã bàn ăn bạn muốn chỉnh sữa: ".Magenta());
                    maBA = Console.ReadLine().ToUpper();
                } while (!maBanAnList.Contains(maBA));
                query = "SELECT MaHoaDon FROM HoaDon WHERE TrangThai = 0 AND MaBanAn = '" + maBA + "';";
                string MaHoaDon = ControllerExcution.ExcutionString(query);
                if (!string.IsNullOrEmpty(MaHoaDon))
                {
                    ModelNhapMon.NhapMon(ControllerExcution.ExcutionString(query));
                }
                else Console.WriteLine("\t\t\tKhông tìm thấy bàn ăn có số bàn {0}".Red(), maBA);
            }
            else Console.WriteLine("\t\tKhông có bàn ăn nào cần chỉnh sửa...".Yellow());
        }
        public void MenuPhucVu(string MaNhanVien)
        {
            string luaChon;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("Chức năng", "Mã lựa chọn")
                                            .AddRow("Đặt món cho bàn ăn mới", "1")
                                            .AddRow("Chỉnh sửa thông tin bàn ăn", "2")
                                            .AddRow("Đổi mật khẩu", "3")
                                            .AddRow("Đăng xuất", "E");
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("------------------PHỤC VỤ-------------------".Blue());

                table.Options.EnableCount = false;
                foreach (var row in table.ToMarkDownString().Split(Environment.NewLine))
                {
                    Console.SetCursorPosition(leftMargin, Console.CursorTop);
                    if (row.StartsWith("|"))
                    {
                        Console.Write(row.Substring(0, 1).Cyan());
                        Console.Write(row.Substring(1, row.Length - 2).Green());
                        Console.WriteLine(row.Substring(row.Length - 1, 1).Cyan());
                    }
                    else if (!string.IsNullOrEmpty(row))
                    {
                        Console.WriteLine(row.White());
                    }
                }
                Console.Write("\tMời nhập lựa chọn của bạn: ".Magenta());
                luaChon = Console.ReadLine().ToUpper();
                switch (luaChon.ToUpper())
                {
                    case "1":
                        {
                            NhapMon(MaNhanVien);
                            break;
                        }
                    case "2":
                        {
                            ChinhSua(MaNhanVien);
                            Console.ReadKey();
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng đổi mật khẩu".Green());
                            if (ControllerDoiMatKhau.DoiMatKhau(MaNhanVien, "NhanVien", "MaNhanVien"))
                                Console.WriteLine("\t\tĐổi mật khẩu thành công!".Green());
                            else
                            {
                                Console.WriteLine("\t\tĐổi mật khẩu thất bại!".Red());
                            }
                            break;
                        }
                    case "E":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn đăng xuất không?"))
                            {
                                ControllerLuuPW.DangXuat(MaNhanVien, "NhanVien", "MaNhanVien");
                                Console.WriteLine("\t\tHẹn gặp lại... Xin chào".Green());
                            }
                            else luaChon = "";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!".Yellow().BlueBg());
                            break;
                        }
                }
            } while (luaChon != "E");
        }
    }
}