using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelFood
    {
        public static List<SqlParameter> ThongTinMonAn(string id)
        {
            Console.Clear();
            ControllerView.Print("SELECT TenMonAn,MaUuDai,LoaiMonAn,MoTa,HinhAnh,SoLuong,DonGia,DonViTinh,DoUaChuong FROM MonAn WHERE MaMonAn = '" + id + "';");

            string tenMonAn = null;
            Console.Write("\tNhập tên món ăn: ".Magenta());
            tenMonAn = Console.ReadLine();

            string input;
            ControllerView.Print("SELECT MaUuDai,ThoiGianBatDau,ThoiGianKetThuc,GiaTri,DonViTinh,DieuKien,MoTa FROM UuDai WHERE TenUuDai = 'Uu dai mon an';");
            string maUuDai = null;
            bool ok;
            do
            {
                Console.Write("\tMã ưu đãi: ".Magenta());
                maUuDai = Console.ReadLine().ToUpper();
                if (!string.IsNullOrEmpty(maUuDai))
                {
                    string sql = "SELECT COUNT(*) FROM UuDai WHERE MaUuDai = '" + maUuDai + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tKhông tìm thấy mã ưu đãi... Vui lòng nhập lại!".Yellow());
                }
                else ok = true;
            } while (!ok);

            string loaiMonAn = null;
            Console.Write("\tNhập loại món ăn: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                loaiMonAn = input;
            }

            string moTa = null;
            Console.Write("\tNhập mô tả: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                moTa = input;
            }

            string hinhAnh = null;
            Console.Write("\tNhập đường dẫn hình ảnh: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                hinhAnh = input;
            }

            string soLuong = null;
            Console.Write("\tNhập số lượng: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                soLuong = input;
            }

            string donGia = null;
            Console.Write("\tNhập đơn giá: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                donGia = input;
            }
            string donViTinh = null;
            Console.Write("\tNhập đơn vị tính: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                donViTinh = input;
            }

            string doUaChuong = null;
            Console.Write("\tNhập độ ưu chuộng: ".Magenta());
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                doUaChuong= input;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();
            string query = null;
            if (!string.IsNullOrEmpty(maUuDai))
            {
                query += "MaUuDai = @maUuDai, ";
                parameters.Add(new SqlParameter("@maUuDai", maUuDai));
            }
            if (!string.IsNullOrEmpty(tenMonAn))
            {
                query += "TenMonAn = @tenMonAn, ";
                parameters.Add(new SqlParameter("@tenMonAn", tenMonAn));
            }
            if (!string.IsNullOrEmpty(loaiMonAn))
            {
                query += "LoaiMonAn = @loaiMonAn, ";
                parameters.Add(new SqlParameter("@loaiMonAn", loaiMonAn));
            }
            if (!string.IsNullOrEmpty(moTa))
            {
                query += "MoTa = @moTa, ";
                parameters.Add(new SqlParameter("@moTa", moTa));
            }
            if (!string.IsNullOrEmpty(hinhAnh))
            {
                query += "HinhAnh = @hinhAnh, ";
                parameters.Add(new SqlParameter("@hinhAnh", hinhAnh));
            }
            

            if (!string.IsNullOrEmpty(soLuong))
            {
                query += "SoLuong = @soLuong, ";
                parameters.Add(new SqlParameter("@soLuong", soLuong));
            }

            if (!string.IsNullOrEmpty(donGia))
            {
                query += "DonGia = @donGia, ";
                parameters.Add(new SqlParameter("@donGia", donGia));
            }
            if (!string.IsNullOrEmpty(donViTinh))
            {
                query += "DonViTinh = @donViTinh, ";
                parameters.Add(new SqlParameter("@donViTinh", donViTinh));
            }
            if (!string.IsNullOrEmpty(doUaChuong))
            {
                query += "DoUaChuong = @doUaChuong ";
                parameters.Add(new SqlParameter("@doUaChuong", doUaChuong));
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
        public static List<SqlParameter> ThongTinCapNhatMonAn()
        {
            Console.WriteLine("* Bắt buộc nhập !".Red().YellowBg());
            string tenMonAn;
            do
            {
                Console.Write("\tNhập tên món ăn (*): ".Magenta());
                tenMonAn = Console.ReadLine();
            } while (string.IsNullOrEmpty(tenMonAn));

            ControllerView.Print("SELECT MaUuDai,ThoiGianBatDau,ThoiGianKetThuc,GiaTri,DonViTinh,DieuKien,MoTa,CachThucDangKy FROM UuDai WHERE TenUuDai = 'Uu dai mon an';");
            string maMonAn = ControllerGenerate.Generate("MA", "MonAn", "MaMonAn");
            bool ok;
            string maUuDai = null;
            do
            {
                Console.Write("\tMã ưu đãi: ".Magenta());
                maUuDai = Console.ReadLine();
                if (!string.IsNullOrEmpty(maUuDai))
                {
                    string sql = "SELECT COUNT(*) FROM UuDai WHERE MaUuDai = '" + maUuDai + "';";
                    ok = ControllerExcution.Excution(sql);
                    if (!ok)
                        Console.WriteLine("\t\tKhông tìm thấy mã ưu đãi... Vui lòng nhập lại!".Yellow());
                }
                else ok = true;
            } while (!ok);
            
            Console.Write("\tLoại món ăn: ".Magenta());
            string loaiMonAn = Console.ReadLine();

            Console.Write("\tMô tả: ".Magenta());
            string moTa = Console.ReadLine();

            Console.Write("\tHình ảnh: ".Magenta());
            string hinhAnh = Console.ReadLine();

            Console.Write("\tSố lượng: ".Magenta());
            int soLuong;
            int.TryParse(Console.ReadLine(), out soLuong);

            float donGia;
            string input;
            do
            {
                Console.Write("\tĐơn giá (*): ".Magenta());
                input = Console.ReadLine();
            } while (!float.TryParse(input, out donGia));

            string donViTinh;
            do
            {
                Console.Write("\tĐơn vị tính (*): ".Magenta());
                donViTinh = Console.ReadLine();
            } while (string.IsNullOrEmpty(donViTinh));

            Console.Write("\tĐộ ưa chuộng: ".Magenta());
            float doUaChuong = 0;
            float.TryParse(Console.ReadLine(), out doUaChuong);

            string query = "INSERT INTO MonAn (MaMonAn, MaUuDai, TenMonAn, LoaiMonAn, MoTa, HinhAnh, SoLuong," +
                " DonGia, DonViTinh, DoUaChuong) " +
                             "VALUES (@MaMonAn, @MaUuDai, @TenMonAn, @LoaiMonAn, @MoTa, @HinhAnh, @SoLuong," +
                             " @DonGia, @DonViTinh, @DoUaChuong)";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MaMonAn", maMonAn));
            parameters.Add(new SqlParameter("@MaUuDai", string.IsNullOrEmpty(maUuDai) ? (object)DBNull.Value : maUuDai));
            parameters.Add(new SqlParameter("@TenMonAn", tenMonAn));
            parameters.Add(new SqlParameter("@LoaiMonAn", loaiMonAn));
            parameters.Add(new SqlParameter("@MoTa", string.IsNullOrEmpty(moTa) ? (object)DBNull.Value : moTa));
            parameters.Add(new SqlParameter("@HinhAnh", string.IsNullOrEmpty(hinhAnh) ? (object)DBNull.Value : hinhAnh));
            parameters.Add(new SqlParameter("@SoLuong", soLuong));
            parameters.Add(new SqlParameter("@DonGia", donGia));
            parameters.Add(new SqlParameter("@DonViTinh", donViTinh));
            parameters.Add(new SqlParameter("@DoUaChuong", doUaChuong));
            parameters.Add(new SqlParameter("query", query));
            return parameters;
        }
        public static bool XoaFood()
        {
            string query = "SELECT MaMonAn,TenMonAn FROM MonAn";
            if (!ControllerView.Print(query))
            {
                Console.WriteLine("\tKhông tìm thấy món ăn nào.".Yellow());
                return false;
            }
            else
            {
                bool ok;
                string maMonAn;
                do
                {
                    Console.Write("\tNhập mã món ăn bạn muốn xóa: ".Magenta());
                    maMonAn = Console.ReadLine().ToUpper();
                    query = "SELECT COUNT(*) FROM MonAn WHERE MaMonAn = '" + maMonAn + "';";
                    ok = ControllerExcution.Excution(query);
                } while (!ok);
                if (ControllerXacNhan.IsXacNhan("Bạn chắc chắn muốn xóa món ăn này?"))
                {
                    query = "DELETE FROM MonAn WHERE MaMonAn = '" + maMonAn + "';";
                    ControllerExcution.Excution(query);
                    Console.WriteLine("\tĐã xóa món ăn có mã {0}".Green(), maMonAn);
                    return true;
                }
                else
                {
                    Console.WriteLine("\tKhông xóa món ăn có mã {0}".Red(), maMonAn);
                    return false;
                }
            }
        }
        public static void XemFooD()
        {
            while (true)
            {
                Console.Clear();
                string query = "SELECT TenMonAn,TRY_CONVERT(VARCHAR,UuDai.GiaTri) + ' ' + UuDai.DonViTinh AS 'GiamGia',LoaiMonAn,MonAn.MoTa,SoLuong,DonGia,MonAn.DonViTinh,DoUaChuong FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai; ";
                ControllerView.Print(query);
                Console.Write("\tNhập thông tin bạn muốn tìm kiếm:  ".Magenta());
                string keyword = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = "SELECT MaMonAn, TenMonAn, MaUuDai, LoaiMonAn, MoTa, HinhAnh, SoLuong, DonGia, DonViTinh, DoUaChuong FROM MonAn WHERE MaMonAn LIKE '%"
                                    + keyword + "%' OR MaUuDai LIKE '%"
                                    + keyword + "%' OR TenMonAn LIKE N'%"
                                    + keyword + "%' OR LoaiMonAn LIKE N'%"
                                    + keyword + "%' OR MoTa LIKE N'%"
                                    + keyword + "%' OR HinhAnh LIKE '%"
                                    + keyword + "%' OR SoLuong LIKE '%"
                                    + keyword + "%' OR DonGia LIKE '%"
                                    + keyword + "%' OR DonViTinh LIKE N'%"
                                    + keyword + "%' OR DoUaChuong LIKE '%"
                                    + keyword + "%';";
                    if (!ControllerView.Print(query))
                    {
                        Console.WriteLine("\t\tKhông tìm thấy món ăn hoặc thức uống có thông tin '{0}'".Red(), keyword);
                    }
                    else if (ControllerXacNhan.IsXacNhan("Bạn có muốn mua hàng không"))
                    {
                        Console.WriteLine("\t\tBạn vui lòng đăng nhập để mua hàng!".Yellow());
                        Console.WriteLine("\t\t\tNhấn phím bất kỳ để tiếp tục...".Cyan());
                        Console.ReadKey();
                        ModelDangNhap.DangNhap();
                    }
                }
                if (!ControllerXacNhan.IsXacNhan("Bạn có muốn tiếp tục tìm kiếm thông tin món ăn"))
                    break;
            }
        }
        public static void FindFood()
        {
            string query = null;
            Console.Write("\tNhập thông tin bạn muốn tìm kiếm:  ".Magenta());
            string keyword = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = "SELECT MaMonAn, TenMonAn, MaUuDai, LoaiMonAn, MoTa, HinhAnh, SoLuong, DonGia, DonViTinh, DoUaChuong FROM MonAn WHERE MaMonAn LIKE '%"
                                + keyword + "%' OR MaUuDai LIKE '%"
                                + keyword + "%' OR TenMonAn LIKE N'%"
                                + keyword + "%' OR LoaiMonAn LIKE N'%"
                                + keyword + "%' OR MoTa LIKE N'%"
                                + keyword + "%' OR HinhAnh LIKE '%"
                                + keyword + "%' OR SoLuong LIKE '%"
                                + keyword + "%' OR DonGia LIKE '%"
                                + keyword + "%' OR DonViTinh LIKE N'%"
                                + keyword + "%' OR DoUaChuong LIKE '%"
                                + keyword + "%';";
                if (!ControllerView.Print(query))
                {
                    Console.WriteLine("\t\tKhông tìm thấy món ăn hoặc thức uống có thông tin '{0}'".Red(), keyword);
                }
            }
        }
    }
}