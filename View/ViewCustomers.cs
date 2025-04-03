using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Microsoft.VisualBasic;
using Project.Controller;
using Project.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewCustomers
    {
        public static void MenuCustomers(string id)
        {
            string input = null;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("Chức năng", "Mã lựa chọn")
    .AddRow("Mua hàng", "     1")
    .AddRow("Xem giỏ hàng", "     2")
    .AddRow("Thanh toán đơn hàng", "     3")
    .AddRow("Xem lịch sử mua hàng", "     4")
    .AddRow("Xem các ưu đãi", "     5")
    .AddRow("Đánh giá đơn hàng", "     6")
    .AddRow("Đổi mật khẩu", "     7")
    .AddRow("Cập nhật thông tin cá nhân", "     8")
    .AddRow("Đổi mã ưu đãi", "     9")
    .AddRow("Đặt bàn", "     10")
    .AddRow("Hủy đặt bàn", "     11")
    .AddRow("Đăng xuất", "     E");

                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;
                int consoleWidth = Console.WindowWidth;
                int numRows = (int)Math.Ceiling((double)tableWidth / consoleWidth) - 2;
                int topMargin = Console.CursorTop - numRows;
                Console.SetCursorPosition(leftMargin, topMargin);
                Console.WriteLine("-----------------KHÁCH HÀNG-----------------".Red());

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

                Console.Write("Mời nhập lựa chọn của bạn: ".Magenta());
                input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "1":
                        {
                            string MaHoaDon = ControllerGenerate.Generate("HD", "HoaDon", "MaHoaDon");
                            ModelHD.TaoHoaDon(MaHoaDon, id);
                            ModelNhapMon.NhapMon(MaHoaDon);
                            break;
                        }
                    case "2":
                        {
                            if (ViewGioHang.GioHang(id))
                            {
                                string query = null;
                                bool ok;
                                if (ControllerXacNhan.IsXacNhan("Bạn có muốn mua tiếp không?"))
                                {
                                    string MaHD = null;
                                    do
                                    {
                                        Console.Write("\tNhập mã hóa đơn bạn muốn chỉnh sửa: ".Magenta());
                                        MaHD = Console.ReadLine().ToUpper();
                                        query = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = '" + MaHD + "' AND MaKhachHang = '" + id + "';";
                                        ok = ControllerExcution.Excution(query);
                                        if (!ok)
                                            Console.WriteLine("\tMã hóa đơn không tồn tại... Vui lòng nhập lại!".Yellow());
                                    } while (!ok);
                                    ModelNhapMon.NhapMon(MaHD);
                                }
                            }
                            else Console.WriteLine("\t\tBạn chưa có đơn hàng...".Yellow());
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thanh toán đơn hàng.".Green());
                            if (ViewGioHang.GioHang(id))
                            {
                                string MaHD = null;
                                bool ok = true;
                                do
                                {
                                    Console.Write("\tNhập mã hóa đơn bạn muốn thanh toán: ".Magenta());
                                    MaHD = Console.ReadLine().ToUpper();
                                    string query = "SELECT COUNT(*) FROM HoaDon WHERE MaHoaDon = '" + MaHD + "' AND MaKhachHang = '" + id + "';";
                                    ok = ControllerExcution.Excution(query);
                                    if (!ok)
                                        Console.WriteLine("\tMã hóa đơn không tồn tại... Vui lòng nhập lại!".Yellow());
                                } while (!ok);
                                Console.Clear();
                                ViewGioHang.ViewDonHang(MaHD);
                                int option;
                                do
                                {
                                    Console.WriteLine("\tMời bạn chọn hình thức giảm giá:".Blue());
                                    Console.WriteLine("\t\t1. Mã ưu đãi".Green());
                                    Console.WriteLine("\t\t2. Điểm tích lũy".Green());
                                    Console.WriteLine("\t\t3. Không sử dụng giảm giá".Yellow());
                                    Console.Write("\tMời chọn: ".Magenta());
                                } while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 3);
                                ModelUD.HinhThucUuDai(option, id, MaHD);
                                ViewGioHang.ViewDonHang(MaHD);
                                ControllerHinhThucThanhToan.HinhThucThanhToan(MaHD);
                                if (ControllerXacNhan.IsXacNhan("Xác nhận thanh toán?"))
                                {
                                    ControllerUuDai.SetDiemTichLuy(id, MaHD);
                                    ControllerHangKH.SetHangKH(id);
                                    ControllerExcution.Excution("UPDATE HoaDon SET ThoiGianXuat = GETDATE() WHERE MaHoaDon = '" + MaHD + "';");
                                    Console.WriteLine("\t\tBạn đã thanh toán thành công!".Green());
                                    ControllerExcution.Excution("UPDATE HoaDon SET TrangThai = 1 WHERE MaHoaDon = '" + MaHD + "';");
                                }
                                else Console.WriteLine("\t\tBạn đã hủy thao tác thanh toán...".Yellow());
                            }
                            else Console.WriteLine("\tBạn không có đơn hàng nào cần thanh toán...".Yellow());
                            break;
                        }
                    case "4":
                        {
                            ViewGioHang.LichSu(id);
                            break;
                        }
                    case "5":
                        {
                            string query = "SELECT ud.MaUuDai,ctkh.SoLuong, ud.TenUuDai, ud.ThoiGianBatDau, ud.ThoiGianKetThuc, " +
                                            "ud.GiaTri, ud.DonViTinh, ud.MoTa, ud.DieuKien, ud.CachThucDangKy " +
                                            "FROM ChiTietKhachHangUuDai ctkh " +
                                            "JOIN UuDai ud ON ctkh.MaUuDai = ud.MaUuDai " +
                                            "WHERE ctkh.MaKhachHang = '" + id + "';";
                            if (!ControllerView.Print(query))
                            {
                                Console.WriteLine("\tBạn không có ưu đãi nào...".Yellow());
                            }
                            break;
                        }
                    case "6":
                        {
                            ControllerDanhGia.DanhGia(id);
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng đổi mật khẩu.".Green());
                            if (ControllerDoiMatKhau.DoiMatKhau(id, "KhachHang", "MaKhachHang"))
                                Console.WriteLine("\t\tĐổi mật khẩu thành công!".Green());
                            else
                            {
                                Console.WriteLine("\t\tĐổi mật khẩu thất bại!".Red());
                            }
                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin.".Green());
                            List<SqlParameter> sqlParameters = ModelKH.CapNhatKH(id);
                            if (sqlParameters == null)
                            {
                                Console.WriteLine("\t\tKhông có thay đổi mới...".Yellow());
                            }
                            else
                            {
                                SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                string query = queryParameter.Value.ToString();
                                query = "UPDATE KhachHang SET " + query + " WHERE MaKhachHang = '" + id + "';";
                                sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                if (ControllerUpdate.CapNhat(query, sqlParameters))
                                    Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                            }
                            break;
                        }
                    case "9":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng đổi điểm ưu đãi.".Green());
                            ModelUD.DoiDiemUuDai(id);
                            break;
                        }
                    case "10":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng đặt bàn.".Green());
                            if (ModelBanAn.DatBan(id))
                                Console.WriteLine("\t\tBạn đã đặt bàn thành công!".Green());
                            else
                            {
                                Console.WriteLine("\t\tBạn đã đặt bàn thất bại!".Red());
                            }
                            break;
                        }
                    case "11":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng hủy đặt bàn.".Green());
                            if (ModelBanAn.HuyDatBan(id))
                            {
                                Console.WriteLine("\t\tBạn đã hủy đặt bàn thành công!".Green());
                            }
                            else
                            {
                                Console.WriteLine("\tBạn đã hủy đặt bàn thất bại!".Red());
                            }
                            break;
                        }
                    case "E":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn đăng xuất không?"))
                            {
                                ControllerLuuPW.DangXuat(id, "KhachHang", "MaKhachHang");
                                Console.WriteLine("\t\tHẹn gặp lại... Xin chào".Green());
                            }
                            else input = "";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!".Yellow().BlueBg());
                            break;
                        }
                }
                Console.ReadKey();
            } while (input != "E");
        }
    }
}