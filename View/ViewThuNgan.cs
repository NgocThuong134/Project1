using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewThuNgan : ViewPhucVu
    {
        public override void NhapMon(string MaNV)
        {
            base.NhapMon(MaNV);
        }
        public override void ChinhSua(string MaNV)
        {
            base.ChinhSua(MaNV);
        }
        public virtual void ThanhToan(string MaNV)
        {
            string query = "SELECT HoaDon.MaBanAn,TenKhachHang, TenBanAn, TenNhanVien, GiamGia, TongTien " +
                "FROM HoaDon JOIN BanAn ON HoaDon.MaBanAn = BanAn.MaBanAn LEFT JOIN NhanVien ON HoaDon.MaNhanVien = NhanVien.MaNhanVien " +
                "LEFT JOIN KhachHang ON HoaDon.MaKhachHang = KhachHang.MaKhachHang " +
                "WHERE HoaDon.TrangThai = 0 AND HoaDon.MaBanAn IS NOT NULL ;";
            if (ControllerView.Print(query))
            {
                string maBanAn = null;
                do
                {
                    Console.Write("\tNhập mã bàn ăn cần thanh toán: ".Magenta());
                    maBanAn = Console.ReadLine();
                    query = "SELECT COUNT(*) FROM HoaDon WHERE TrangThai = 0 AND MaBanAn IS NOT NULL AND MaBanAn = '" + maBanAn + "';";
                } while (!ControllerExcution.Excution(query));
                query = "SELECT MaKhachHang FROM HoaDon WHERE MaHoaDon = '" + maBanAn + "';";
                string id = ControllerExcution.ExcutionString(query);
                query = "SELECT MaHoaDon FROM HoaDon WHERE MaBanAn = '" + maBanAn + "';";
                string maHD = ControllerExcution.ExcutionString(query);
                if (!string.IsNullOrEmpty(id))
                {
                    int option;
                    do
                    {
                        Console.WriteLine("\tMời bạn chọn hình thức giảm giá".Yellow());
                        Console.WriteLine("\t\t1. Mã ưu đãi".Blue());
                        Console.WriteLine("\t\t2. Điểm tích lũy".Blue());
                        Console.WriteLine("\t\t3. Không sử dụng giảm giá".Blue());
                        Console.Write("\tMời chọn: ".Magenta());
                    } while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3);
                    ModelUD.HinhThucUuDai(option, id, maHD);
                }
                ControllerHinhThucThanhToan.HinhThucThanhToan(maHD);
                if (ControllerXacNhan.IsXacNhan("Xác nhận thanh toán"))
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        ControllerUuDai.SetDiemTichLuy(id, maHD);
                        ControllerHangKH.SetHangKH(id);
                    }
                    ControllerExcution.Excution("UPDATE HoaDon SET ThoiGianXuat = GETDATE() WHERE MaHoaDon = '" + maHD + "';");
                    Console.WriteLine("\t\tBạn đã thanh toán thành công!".Green());
                    ControllerExcution.Excution("UPDATE HoaDon SET TrangThai = 1 WHERE MaHoaDon = '" + maHD + "';");
                    ControllerExcution.Excution("UPDATE BanAn SET TrangThai = 0, ChuThich = NULL WHERE MaBanAn = '" + maBanAn + "';");
                }
                else Console.WriteLine("\t\tBạn đã hủy thao tác thanh toán...".Yellow());
            }
            else Console.WriteLine("\t\t\tKhông có bàn ăn nào cần thanh toán...".Yellow());
        }
        public void MenuThuNgan(string MaNhanVien)
        {
            string luaChon;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("Chức năng", "Mã lựa chọn")
                                            .AddRow("Đặt món cho bàn ăn mới", "1")
                                            .AddRow("Chỉnh sửa thông tin bàn ăn", "2")
                                            .AddRow("Thanh toán hóa đơn", "3")
                                            .AddRow("Đổi mật khẩu", "4")
                                            .AddRow("Đăng xuất", "E");
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("------------------THU NGÂN------------------".Blue());

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
                            ThanhToan(MaNhanVien);
                            Console.ReadKey();
                            break;
                        }
                    case "4":
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
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn đăng xuất không"))
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