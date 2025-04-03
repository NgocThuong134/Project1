using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelDangKy
    {
        public static void DangKy()
        {
            Console.WriteLine("\t\tVui lòng nhập thông tin".Green());
            Console.WriteLine("\t\t\t(*) Bắt buộc nhập!".Red());
            string name;
            do
            {
                Console.Write("\tHọ và tên(*): ".Magenta());
                name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(name));

            string birthdaystr = null;
            DateTime birthday = DateTime.Now;
            Console.Write("\tNgày sinh: ".Magenta());
            birthdaystr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(birthdaystr))
            {
                birthday = DateTime.Parse(birthdaystr);
            }

            int opt;
            bool isValidOption = false;

            do
            {
                Console.WriteLine("\tGiới tính(*)".Green());
                Console.WriteLine("\t\t0. Nam".Red());
                Console.WriteLine("\t\t1. Nữ".Yellow());
                Console.Write("\tMời chọn: ".Magenta());
                string input = Console.ReadLine();

                if (int.TryParse(input, out opt) && (opt == 0 || opt == 1))
                {
                    isValidOption = true;
                }
                else
                {
                    Console.WriteLine("\t\tGiá trị nhập vào không hợp lệ. Vui lòng nhập lại.".Red());
                }
            } while (!isValidOption);

            Console.Write("\tEmail: ".Magenta());
            string email = Console.ReadLine();

            string adr;
            do
            {
                Console.Write("\tĐịa chỉ(*): ".Magenta());
                adr = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(adr));

            string number;
            do
            {
                Console.Write("\tSố điện thoại(*): ".Magenta());
                number = Console.ReadLine();
                if (number.Length != 10 || !number.All(char.IsDigit))
                {
                    Console.WriteLine("\t\tSố điện thoại phải đủ 10 chữ số và chỉ chứa các số từ 0 đến 9. Vui lòng nhập lại!".Red());
                    number = null;
                }
            } while (string.IsNullOrWhiteSpace(number));

            string ok = null;
            string tenDangNhap = null;
            do
            {
                Console.Write("\tTên đăng nhập(*): ".Magenta());
                tenDangNhap = Console.ReadLine();
                string query = "SELECT TenDangNhap FROM KhachHang WHERE TenDangNhap = '"+tenDangNhap+"'";
                ok = ControllerExcution.ExcutionString(query);
                if (!string.IsNullOrEmpty(ok))
                    Console.WriteLine("\t\tTên đăng nhập đã bị trùng. Vui lòng nhập lại!".Red());
            } while ((string.IsNullOrEmpty(tenDangNhap)) || (!string.IsNullOrEmpty(ok)));

            string pass = null, repass = null;
            do
            {
                Console.Write("\tNhập mật khẩu(*): ".Magenta());
                pass = ControllerMaHoaPW.ReadPassword();
                Console.Write("\tXác nhận lại mật khẩu: ".Magenta());
                repass = ControllerMaHoaPW.ReadPassword();
                if (pass != repass)
                    Console.WriteLine("\t\tMật khẩu không khớp! Vui lòng nhập lại!".Red());
            } while (pass != repass || string.IsNullOrEmpty(pass));

            if (ControllerAddCus.AddCus(name, birthday, opt, email, adr, number, tenDangNhap, pass))
                Console.WriteLine("Đăng ký thành công".Green());
            else
                Console.WriteLine("Đăng ký thất bại".Red());
        }
    }
}
