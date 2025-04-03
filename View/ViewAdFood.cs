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
    internal class ViewAdFood
    {
        public void MenuAdFood()
        {
            string luaChon;
            do
            {
                Console.Clear();
                ConsoleTable table = new ConsoleTable("Mã lựa chọn", "Chức năng")
    .AddRow("1", "Xem danh sách các món ăn và thức uống")
    .AddRow("2", "Cập nhật thông tin món ăn và thức uống")
    .AddRow("3", "Thêm món ăn hoặc thức uống vào danh sách")
    .AddRow("4", "Xóa món ăn hoặc thức uống khỏi danh sách")
    .AddRow("5", "Tìm kiếm thông tin món ăn hoặc thức uống")
    .AddRow("0", "Quay lại");

                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("----------------QUẢN LÝ MÓN ĂN VÀ THỨC UỐNG---------------".Yellow());

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
                luaChon = Console.ReadLine();

                switch (luaChon)
                {
                    case "1":
                        Console.WriteLine("\t\tBạn đã chọn chức năng xem danh sách món ăn và thức uống".Green());
                        ControllerView.Print("SELECT MaMonAn, TenMonAn, LoaiMonAn,CAST(UuDai.GiaTri AS VARCHAR) + ' ' + UuDai.DonViTinh AS GiamGia, MonAn.MoTa, SoLuong, DonGia, MonAn.DonViTinh, DoUaChuong FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai;");
                        break;
                    case "2":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng cập nhật thông tin món ăn và thức uống.".Green());
                            ControllerView.Print("SELECT MaMonAn, TenMonAn, LoaiMonAn, CAST(UuDai.GiaTri AS VARCHAR) + ' ' + UuDai.DonViTinh AS GiamGia , HinhAnh, SoLuong, DonGia, MonAn.DonViTinh, DoUaChuong FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai;");
                            Console.Write("\tNhập mã món ăn hoặc thức uống cần cập nhật: ".Magenta());
                            string mafood = Console.ReadLine();
                            if (ControllerCheckID.CheckID(mafood.ToUpper(), "MonAn", "MaMonAn"))
                            {
                                Console.WriteLine("\t\t\tMã {0} tồn tại!".Green(), mafood);
                                Console.ReadKey();
                                List<SqlParameter> sqlParameters = ModelFood.ThongTinMonAn(mafood);
                                if (sqlParameters == null)
                                {
                                    Console.WriteLine("\t\t\tKhông có thay đổi mới...".Green());
                                }
                                else
                                {
                                    SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                                    string query = queryParameter.Value.ToString();
                                    query = "UPDATE MonAn SET " + query + " WHERE MaMonAn = @id";
                                    sqlParameters.RemoveAt(sqlParameters.Count - 1);
                                    if (ControllerUpdate.CapNhat(query, sqlParameters))
                                        Console.WriteLine("\t\t\tCập nhật thông tin thành công!".Green());
                                    else Console.WriteLine("\t\t\tCập nhật thông tin thất bại!".Red());
                                }
                            }
                            else Console.WriteLine("\t\t\tMã {0} không tồn tại!".Red(), mafood);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng thêm món ăn hoặc thức uống vào danh sách.".Green());
                            List<SqlParameter> sqlParameters = ModelFood.ThongTinCapNhatMonAn();
                            SqlParameter queryParameter = sqlParameters[sqlParameters.Count - 1];
                            string query = queryParameter.Value.ToString();
                            sqlParameters.RemoveAt(sqlParameters.Count - 1);
                            if (ControllerUpdate.CapNhat(query, sqlParameters))
                                Console.WriteLine("\t\t\tThêm thành công!".Green());
                            else Console.WriteLine("\t\t\tThêm thất bại!".Red());
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng xóa món ăn hoặc thức uống ra khỏi danh sách.".Green());
                            if (ModelFood.XoaFood())
                            {
                                Console.WriteLine("\t\t\tXóa thành công!".Green());
                            }
                            else Console.WriteLine("\t\t\tXóa thất bại!".Red());
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("\t\tBạn đã chọn chức năng tìm kiếm thông tin món ăn hoặc thức uống.".Green());
                            Console.Write("\tNhập thông tin bạn muốn tìm kiếm: ".Magenta());
                            string keyword = Console.ReadLine().Trim();

                            string query = "SELECT MaMonAn, TenMonAn, MaUuDai, LoaiMonAn, MoTa, HinhAnh, SoLuong, DonGia, DonViTinh, DoUaChuong FROM MonAn WHERE MaMonAn LIKE '%"
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
                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không?"))
                                Console.WriteLine("\t\tBạn đã chọn quay lại chương trình".Green());
                            else
                                luaChon = "";
                            break;
                        }
                    default:
                        Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng nhập lại!".Yellow().BlueBg());
                        break;
                }
                Console.ReadKey();
            } while (luaChon != "0");
        }
    }
}
