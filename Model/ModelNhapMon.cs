using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    internal class ModelNhapMon
    {
        static int NhapSoLuong()
        {
            int soLuong = 1;
            string input = null;

            do
            {
                Console.WriteLine("\t\tNhấn Enter để lấy số lượng là 1".Yellow());
                Console.Write("\tNhập số lượng (phải là số nguyên dương): ".Magenta());

                try
                {
                    input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        soLuong = 1;
                    }
                    else
                    {
                        soLuong = Convert.ToInt32(input);
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\t\tNhập không hợp lệ, vui lòng nhập lại!".Yellow());
                    soLuong = -1;
                }
            } while (soLuong <= 0);

            return soLuong;
        }
        public static void NhapMon(string maHoaDon)
        {
            string MaMonAn = null, chuThich = null, input = null;
            int SoLuong = 1;
            Console.Clear();
            ControllerView.Print("SELECT MaMonAn,TenMonAn,DonGia,MonAn.DonViTinh,TRY_CONVERT(VARCHAR,UuDai.GiaTri) + ' ' + UuDai.DonViTinh AS 'GiamGia',DoUaChuong " +
                                    "FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai WHERE SoLuong > 0 ;");
            if (!ControllerXacNhan.IsXacNhan("Bạn muốn mua hàng không ?"))
                Console.WriteLine("\t\t\tNhấn phím bất kỳ để tiếp tục...");
            else
                while (true)
                {
                    Console.Clear();
                    ControllerView.Print("SELECT MaMonAn,TenMonAn,DonGia,MonAn.DonViTinh,TRY_CONVERT(VARCHAR,UuDai.GiaTri) + ' ' + UuDai.DonViTinh AS 'GiamGia',DoUaChuong " +
                                            "FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai WHERE SoLuong > 0 ;");
                    if (ControllerXacNhan.IsXacNhan("Bạn muốn tìm kiếm thông tin món ăn không"))
                        ModelFood.FindFood();
                    do
                    {
                        Console.Write("\tNhập mã món ăn: ".Magenta());
                        MaMonAn = Console.ReadLine().ToUpper();
                        if (!ControllerCheckID.CheckID(MaMonAn, "MonAn", "MaMonAn"))
                            Console.WriteLine("\t\tMã món ăn không tồn tại, vui lòng nhập lại!".Yellow());
                        else
                        {
                            ControllerView.Print("SELECT TenMonAn,DonGia,MonAn.DonViTinh," +
                                "TRY_CONVERT(VARCHAR,UuDai.GiaTri) + ' ' + UuDai.DonViTinh AS 'GiamGia',DoUaChuong " +
                                "FROM MonAn LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai " +
                                "WHERE SoLuong > 0 AND MaMonAn = '" + MaMonAn + "' ;");
                        }
                    } while (!ControllerCheckID.CheckID(MaMonAn, "MonAn", "MaMonAn"));
                    float donGia = 0f;
                    string donViTinh = null, query = null;
                    using (SqlConnection sql = ControllerConnectSQL.GetSqlConnection())
                    {
                        query = "SELECT DonGia, DonViTinh FROM MonAn WHERE MaMonAn = @MaMonAn";
                        SqlCommand command = new SqlCommand(query, sql);
                        command.Parameters.AddWithValue("@MaMonAn", MaMonAn);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                donGia = (float)reader.GetDouble(0);
                                donViTinh = reader.GetString(1);
                            }
                        }
                    }
                    query = "SELECT COUNT(*) FROM ChiTietHoaDonMonAn WHERE MaMonAn = '" + MaMonAn + "' AND MaHoaDon = '" + maHoaDon + "';";
                    if (ControllerExcution.Excution(query))
                    {
                        query = "SELECT SoLuong FROM ChiTietHoaDonMonAn WHERE MaMonAn = '" + MaMonAn + "' AND MaHoaDon = '" + maHoaDon + "';";
                        int soLuong = ControllerExcution.ExcutionInt(query);
                        Console.WriteLine("\tMã ".Green() + MaMonAn.Yellow() + " đã tồn tại trong đơn hàng của bạn ".Green() + "với số lượng hiện có là: ".Green() + soLuong.ToString().Red());
                        do
                        {
                            Console.WriteLine("\t\t\tChức năng".Yellow());
                            Console.WriteLine("\t1. Thêm vào đơn hàng".Green());
                            Console.WriteLine("\t2. Xóa ra khỏi đơn hàng".Red());
                            Console.Write("\tMời chọn: ".Magenta());
                            input = Console.ReadLine();
                        } while (input == null || (input != "1" && input != "2"));
                        if (input == "1")
                        {
                            SoLuong = NhapSoLuong();
                            query = "UPDATE MonAn SET SoLuong = SoLuong - " + SoLuong + " WHERE MaMonAn = '" + MaMonAn + "';";
                            ControllerExcution.Excution(query);
                            Console.Write("\tNhập chú thích: ".Magenta());
                            chuThich = Console.ReadLine();
                            query = "UPDATE ChiTietHoaDonMonAn SET SoLuong = SoLuong + @SoLuong " +
                                   "WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn ";
                            List<SqlParameter> parameters = new List<SqlParameter>();
                            parameters.Add(new SqlParameter("@MaHoaDon", maHoaDon));
                            parameters.Add(new SqlParameter("@MaMonAn", MaMonAn));
                            parameters.Add(new SqlParameter("@SoLuong", SoLuong));
                            parameters.Add(new SqlParameter("@ChuThich", string.IsNullOrEmpty(chuThich) ? (object)DBNull.Value : chuThich));

                            if (ControllerUpdate.CapNhat(query, parameters))
                            {
                                Console.WriteLine("\t\t\tThêm thành công!".Green());
                                query = "UPDATE ChiTietHoaDonMonAn " +
                                        "SET GiamGia = CASE WHEN UuDai.DonViTinh = N'VNĐ' THEN UuDai.GiaTri*cthd.SoLuong WHEN UuDai.DonViTinh = '%' THEN UuDai.GiaTri*cthd.SoLuong*cthd.DonGia/100 ELSE 0 END " +
                                        "FROM ChiTietHoaDonMonAn cthd " +
                                        "LEFT JOIN MonAn ON cthd.MaMonAn = MonAn.MaMonAn " +
                                        "LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai";
                                ControllerExcution.Excution(query);
                                ControllerSum.Sum(maHoaDon, MaMonAn);
                            }
                            else
                            {
                                Console.WriteLine("\t\t\tThêm thất bại!".Red());
                            }
                        }
                        else if (input == "2")
                        {
                            SoLuong = NhapSoLuong();
                            query = "UPDATE MonAn SET SoLuong = SoLuong + " + SoLuong + " WHERE MaMonAn = '" + MaMonAn + "';";
                            ControllerExcution.Excution(query);
                            query = "IF @SoLuong >= (SELECT SoLuong FROM ChiTietHoaDonMonAn WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn) " +
                                    "DELETE FROM ChiTietHoaDonMonAn WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn " +
                                    "ELSE UPDATE ChiTietHoaDonMonAn SET SoLuong = SoLuong - @SoLuong WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn ";
                            List<SqlParameter> parameters = new List<SqlParameter>();
                            parameters.Add(new SqlParameter("@MaHoaDon", maHoaDon));
                            parameters.Add(new SqlParameter("@MaMonAn", MaMonAn));
                            parameters.Add(new SqlParameter("@SoLuong", SoLuong));
                            parameters.Add(new SqlParameter("@DonGia", donGia));
                            parameters.Add(new SqlParameter("@DonViTinh", donViTinh));
                            parameters.Add(new SqlParameter("@ChuThich", string.IsNullOrEmpty(chuThich) ? (object)DBNull.Value : chuThich));
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn xóa không ?"))
                            {
                                if (ControllerUpdate.CapNhat(query, parameters))
                                {
                                    query = "UPDATE ChiTietHoaDonMonAn " +
                                        "SET GiamGia = CASE WHEN UuDai.DonViTinh = N'VNĐ' THEN UuDai.GiaTri*cthd.SoLuong WHEN UuDai.DonViTinh = '%' THEN UuDai.GiaTri*cthd.SoLuong*cthd.DonGia/100 ELSE 0 END " +
                                        "FROM ChiTietHoaDonMonAn cthd " +
                                        "LEFT JOIN MonAn ON cthd.MaMonAn = MonAn.MaMonAn " +
                                        "LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai";
                                    ControllerExcution.Excution(query);
                                    Console.WriteLine("\t\t\tXóa thành công!".Green());
                                    ControllerSum.Sum(maHoaDon, MaMonAn);
                                }
                                else
                                {
                                    Console.WriteLine("\t\t\tXóa thất bại!".Red());
                                }
                            }
                        }
                    }
                    else
                    {
                        SoLuong = NhapSoLuong();
                        query = "UPDATE MonAn SET SoLuong = SoLuong - " + SoLuong + " WHERE MaMonAn = '" + MaMonAn + "';";
                        ControllerExcution.Excution(query);
                        Console.Write("\tNhập chú thích: ".Magenta());
                        chuThich = Console.ReadLine();
                        query = "INSERT INTO ChiTietHoaDonMonAn (MaHoaDon, MaMonAn, SoLuong, DonGia, DonViTinh, ChuThich) " +
                                "VALUES (@MaHoaDon, @MaMonAn, @SoLuong, @DonGia, @DonViTinh, @ChuThich)";

                        List<SqlParameter> parameters = new List<SqlParameter>();
                        parameters.Add(new SqlParameter("@MaHoaDon", maHoaDon));
                        parameters.Add(new SqlParameter("@MaMonAn", MaMonAn));
                        parameters.Add(new SqlParameter("@SoLuong", SoLuong));
                        parameters.Add(new SqlParameter("@DonGia", donGia));
                        parameters.Add(new SqlParameter("@DonViTinh", donViTinh));
                        parameters.Add(new SqlParameter("@ChuThich", string.IsNullOrEmpty(chuThich) ? (object)DBNull.Value : chuThich));

                        if (ControllerUpdate.CapNhat(query, parameters))
                        {
                            Console.WriteLine("\t\t\tThêm thành công!".Green());
                            query = "UPDATE ChiTietHoaDonMonAn " +
                                        "SET GiamGia = CASE WHEN UuDai.DonViTinh = N'VNĐ' THEN UuDai.GiaTri*cthd.SoLuong WHEN UuDai.DonViTinh = '%' THEN UuDai.GiaTri*cthd.SoLuong*cthd.DonGia/100 ELSE 0 END " +
                                        "FROM ChiTietHoaDonMonAn cthd " +
                                        "LEFT JOIN MonAn ON cthd.MaMonAn = MonAn.MaMonAn " +
                                        "LEFT JOIN UuDai ON MonAn.MaUuDai = UuDai.MaUuDai";
                            ControllerExcution.Excution(query);
                            ControllerSum.Sum(maHoaDon, MaMonAn);
                        }
                        else
                        {
                            Console.WriteLine("\t\t\tThêm thất bại!".Red());
                        }
                    }
                    if (!ControllerXacNhan.IsXacNhan("Bạn có muốn tiếp tục mua hàng không"))
                        break;
                }
        }
    }
}
