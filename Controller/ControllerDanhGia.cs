using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerDanhGia
    {
        public static void DanhGia(string Id)
        {
            List<string> maHoaDonList = new List<string>();
            List<SqlParameter> parameters = new List<SqlParameter>();

            string query = "SELECT DISTINCT MaHoaDon " +
                           "FROM HoaDon " +
                           "WHERE TrangThai = 1 AND MaKhachHang = '" + Id + "' AND  TongTien <> 0;";

            using (SqlConnection connection = ControllerConnectSQL.GetSqlConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string maHoaDon = reader.GetString(0);
                            maHoaDonList.Add(maHoaDon);
                        }
                    }
                }
            }
            if (maHoaDonList.Count > 0)
            {
                Console.WriteLine("\t\tDanh sách các mã đơn hàng cần bận đánh giá".Green());
                int stt = 0;
                foreach (string item in maHoaDonList) 
                {
                    stt++;
                    Console.WriteLine("\t "+stt.ToString().Red()+": ".Yellow()+item.Cyan());
                }
                string MaHoaDon;
                bool isValidMaDonHang = false;

                do
                {
                    Console.Write("\tNhập mã đơn hàng bạn muốn đánh giá: ".Magenta());
                    MaHoaDon = Console.ReadLine().ToUpper();

                    if (maHoaDonList.Contains(MaHoaDon))
                    {
                        isValidMaDonHang = true;
                    }
                    else
                    {
                        Console.WriteLine("\t\tMã đơn hàng không hợp lệ. Vui lòng nhập lại!".Yellow());
                    }
                } while (!isValidMaDonHang);
                using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                {
                    query = "SELECT MA.TenMonAn, MA.LoaiMonAn,MA.MoTa, CT.SoLuong, CT.DonGia, CT.DonViTinh, CT.GiamGia, CT.TongTien " +
                            "FROM ChiTietHoaDonMonAn CT " +
                            "JOIN MonAn MA ON CT.MaMonAn = MA.MaMonAn " +
                            "WHERE CT.MaHoaDon = '"+MaHoaDon+"';";
                    using (SqlCommand cmd = new SqlCommand(query,sql))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        var table = new ConsoleTable("Tên món ăn", "Loại món ăn", "Mô tả", "Số lượng", "Đơn giá", "Đơn vị tính", "Giảm giá", "Tổng tiền");

                        while (reader.Read())
                        {
                            string tenMonAn = reader.GetString(0);
                            string loaiMonAn = reader.GetString(1);
                            string moTa = reader.GetString(2);
                            int soLuong = reader.GetInt32(3);
                            float donGia = (float)reader.GetDouble(4);
                            string donViTinh = reader.GetString(5);
                            double giamGia = (double)reader.GetDouble(6);
                            float tongTien = (float)reader.GetDouble(7);

                            table.AddRow(tenMonAn, loaiMonAn, moTa, soLuong, donGia, donViTinh, giamGia, tongTien);
                        }

                        string[] rows = table.ToMarkDownString().Split(Environment.NewLine);
                        float sum=0;
                        int dem = 0;
                        Console.WriteLine(rows[0].Red());

                        for (int i = 1; i < rows.Length; i++)
                        {
                            Console.WriteLine(rows[i].Blue());
                            float monan = 5;
                            string input;
                            if (i > 1 && i != rows.Length - 1)
                            {
                                do
                                {
                                    Console.Write("\tChất lượng món ăn (1-5): ".Yellow());
                                    input = Console.ReadLine();
                                    dem++;
                                } while (!float.TryParse(input, out monan) || monan < 1 || monan > 5);
                                sum += monan;
                            }
                        }
                        sum = sum / dem;
                        query = "UPDATE HoaDon SET DanhGiaChatLuong = @DanhGiaChatLuong WHERE MaHoaDon = @MaHoaDon";
                        parameters.Add(new SqlParameter("@DanhGiaChatLuong", sum.ToString("N1")));
                        parameters.Add(new SqlParameter("@MaHoaDon", MaHoaDon));
                        ControllerUpdate.CapNhat(query, parameters);
                        parameters.Clear();
                    }
                }
                float dichvu;
                string input1;
                    do
                    {
                        Console.Write("\tChất lượng dịch vụ (1-5): ".Yellow());
                        input1 = Console.ReadLine();

                    } while (!float.TryParse(input1, out dichvu) || dichvu < 1 || dichvu > 5);
                query = "UPDATE HoaDon SET DanhGiaDichVu = @DanhGiaDichVu WHERE MaHoaDon = @MaHoaDon";
                parameters.Add(new SqlParameter("@DanhGiaDichVu",dichvu.ToString("N1")));
                parameters.Add(new SqlParameter("@MaHoaDon",MaHoaDon));
                ControllerUpdate.CapNhat(query, parameters);
                parameters.Clear();
                Console.Write("\tNhận xét: ".Cyan());
                string nhanxet = Console.ReadLine();
                query = "UPDATE HoaDon SET NhanXet = @NhanXet WHERE MaHoaDon = @MaHoaDon";
                parameters.Add(new SqlParameter("@NhanXet", nhanxet.ToString()));
                parameters.Add(new SqlParameter("@MaHoaDon", MaHoaDon));
                ControllerUpdate.CapNhat(query,parameters);
                Console.WriteLine("\t\t\tBạn đã cập nhật đánh giá thành công!".Green());
            }
            else
            {
                Console.WriteLine("\t\t\tBạn không có đơn hàng nào cần đánh giá!".Yellow());
            }
        }
    }
}
