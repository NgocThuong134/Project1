using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelDangNhap
    {
        public static void DangNhap()
        {
            int dem = 5;
            while (dem >= 0)
            {
                Console.Clear();
                Console.Write("\t\tTên đăng nhập: ".Green());
                string name = Console.ReadLine();
                string query1 = "SELECT COUNT(*) FROM KhachHang WHERE TenDangNhap = '" + name + "' AND LuuMatKhau = 1;";
                string query2 = "SELECT COUNT(*) FROM NhanVien WHERE TenDangNhap = '" + name + "' AND LuuMatKhau = 1";
                if (ControllerExcution.Excution(query1))
                {
                    Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                    string id = ControllerGetID.GetID(name, "KhachHang", "MaKhachHang");
                    ViewCustomers.MenuCustomers(id);
                    dem = -2;
                }
                else if (ControllerExcution.Excution(query2))
                {
                    string phucVuQuery = "SELECT COUNT(*) FROM NhanVien WHERE Chucvu = N'Nhân viên phục vụ' AND TenDangNhap = '" + name + "';";
                    string thuNganQuery = "SELECT COUNT(*) FROM NhanVien WHERE Chucvu = N'Nhân viên thu ngân' AND TenDangNhap = '" + name + "';";
                    if (name.ToLower() == "admin")
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                        ViewAdmin.MenuAd();
                        dem = -2;
                    }
                    else if (ControllerExcution.Excution(phucVuQuery))
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                        string id = ControllerGetID.GetID(name, "NhanVien", "MaNhanVien");
                        ViewPhucVu viewPhucVu = new ViewPhucVu();
                        viewPhucVu.MenuPhucVu(id);
                        dem = -2;
                    }
                    else if (ControllerExcution.Excution(thuNganQuery))
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                        string id = ControllerGetID.GetID(name, "NhanVien", "MaNhanVien");
                        ViewThuNgan viewThuNgan = new ViewThuNgan();
                        viewThuNgan.MenuThuNgan(id);
                        dem = -2;
                    }
                }
                else
                {
                    Console.Write("\t\tMật khẩu: ".Green());
                    string pass = ControllerMaHoaPW.ReadPassword();

                    if (ControllerLogin.IsLogin(name, pass, "KhachHang"))
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                        string id = ControllerGetID.GetID(name, "KhachHang", "MaKhachHang");
                        if (ControllerXacNhan.IsXacNhan("Bạn có muốn lưu mật khẩu không ?"))
                        {
                            ControllerLuuPW.LuuMatKhau(id, "KhachHang", "MaKhachHang");
                            Console.WriteLine("\t\t\tBạn đã lưu mật khẩu thành công!".Green());
                        }
                        ViewCustomers.MenuCustomers(id);
                        dem = -2;
                    }
                    else if (ControllerLogin.IsLogin(name, pass, "NhanVien") && name.ToLower() == "admin")
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                        if (ControllerXacNhan.IsXacNhan("Bạn có muốn lưu mật khẩu không ?"))
                        {
                            ControllerLuuPW.LuuMatKhau("NV000", "NhanVien", "MaNhanVien");
                            Console.WriteLine("\t\t\tBạn đã lưu mật khẩu thành công!".Green());
                        }
                        ViewAdmin.MenuAd();
                        dem = -2;
                    }
                    else if (ControllerLogin.IsLogin(name, pass, "NhanVien"))
                    {
                        string phucVuQuery = "SELECT COUNT(*) FROM NhanVien WHERE Chucvu = N'Nhân viên phục vụ' AND TenDangNhap = '" + name + "';";
                        string thuNganQuery = "SELECT COUNT(*) FROM NhanVien WHERE Chucvu = N'Nhân viên thu ngân' AND TenDangNhap = '" + name + "';";

                        if (ControllerExcution.Excution(phucVuQuery))
                        {
                            Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                            string id = ControllerGetID.GetID(name, "NhanVien", "MaNhanVien");
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn lưu mật khẩu không ?"))
                            {
                                ControllerLuuPW.LuuMatKhau(id, "NhanVien", "MaNhanVien");
                                Console.WriteLine("\t\t\tBạn đã lưu mật khẩu thành công!".Green());
                            }
                            ViewPhucVu viewPhucVu = new ViewPhucVu();
                            viewPhucVu.MenuPhucVu(id);
                            dem = -2;
                        }
                        else if (ControllerExcution.Excution(thuNganQuery))
                        {
                            Console.WriteLine("\t\t\tĐăng nhập thành công!".Green());
                            string id = ControllerGetID.GetID(name, "NhanVien", "MaNhanVien");
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn lưu mật khẩu không ?"))
                            {
                                ControllerLuuPW.LuuMatKhau(id, "NhanVien", "MaNhanVien");
                                Console.WriteLine("\t\t\tBạn đã lưu mật khẩu thành công!".Green());
                            }
                            ViewThuNgan viewThuNgan = new ViewThuNgan();
                            viewThuNgan.MenuThuNgan(id);
                            dem = -2;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\t\t\tĐăng nhập thất bại!\n\t\t\tTên đăng nhập hoặc mật khẩu không đúng!\n\t\t\tVui lòng nhập lại!".Red());
                        Console.WriteLine("\t\t\tBạn còn ".Yellow() + dem.ToString().Red() + " đăng nhập!".Yellow());
                        Console.ReadKey();
                        dem--;
                    }
                }
            }
            if (dem < 0 && dem != -2)
            {
                if (ControllerXacNhan.IsXacNhan("Bạn muốn đăng ký tài khoản không"))
                    ModelDangKy.DangKy();
            }
        }
    }
}
