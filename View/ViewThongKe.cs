using ConsoleTables;
using CSharpExtensions.OpenSource.ConsoleColors;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.View
{
    internal class ViewThongKe
    {
        public void MenuThongKe()
        {
            string chon;
            do
            {
                Console.Clear();
                var table = new ConsoleTable("Lựa chọn", "Mô tả")
    .AddRow("1", "Thống kê theo ngày")
    .AddRow("2", "Thống kê theo tháng")
    .AddRow("3", "Thống kê theo quý")
    .AddRow("4", "Thống kê theo năm")
    .AddRow("0", "Quay lại");
                table.Options.EnableCount = false;
                int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
                int leftMargin = (Console.WindowWidth - tableWidth) / 2;

                Console.SetCursorPosition(leftMargin, 2);
                Console.WriteLine("-------------THỐNG KÊ-------------".Yellow());
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
                Console.Write("Mời chọn: ".Magenta());
                chon = Console.ReadLine();
                Console.WriteLine("\t\t\t(*) Nhấn Enter để lấy thời gian hiện tại!".Yellow());
                switch (chon)
                {
                    case "1":
                        {
                            string input;
                            DateTime startTime = DateTime.Now, endTime = DateTime.Now;
                            Console.Write("\tNhập ngày bắt đầu (dd-mm-yyyy)(*): ".Magenta());
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input))
                            {
                                startTime = DateTime.Parse(input);
                            }
                            Console.Write("\tNhập ngày kết thúc (dd-mm-yyyy)(*): ".Magenta());
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input))
                            {
                                endTime = DateTime.Parse(input);
                            }
                            endTime = endTime.AddDays(1);
                            ControllerThongKe.ThongKe(startTime, endTime);
                            break;
                        }
                    case "2":
                        {
                            string input;
                            int month = DateTime.Now.Month;
                            Console.Write("\tNhập tháng(*): ".Magenta());
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input))
                            {
                                month = int.Parse(input);
                            }
                            int year = DateTime.Now.Year;
                            Console.Write("\tNhập năm(*): ".Magenta());
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input))
                            {
                                year = int.Parse(input);
                            }
                            DateTime startTime = new DateTime(year, month, 1);
                            DateTime endTime = startTime.AddMonths(1).AddSeconds(-1);
                            ControllerThongKe.ThongKe(startTime, endTime);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("\tCác quý trong năm {0}(*)".Green(), DateTime.Now.Year);
                            Console.WriteLine("\t\t1. Quý 1".Yellow());
                            Console.WriteLine("\t\t2. Quý 2".Yellow());
                            Console.WriteLine("\t\t3. Quý 3".Yellow());
                            Console.WriteLine("\t\t4. Quý 4".Yellow());
                            Console.Write("\tMời chọn: ".Magenta());
                            string input = Console.ReadLine();
                            int month;
                            DateTime startTime, endTime;
                            if (!string.IsNullOrEmpty(input))
                            {
                                switch (input)
                                {
                                    case "1":
                                        {
                                            month = 1;
                                            startTime = new DateTime(DateTime.Now.Year, month, 1);
                                            endTime = startTime.AddMonths(2).AddSeconds(-1);
                                            ControllerThongKe.ThongKe(startTime, endTime);
                                            break;
                                        }
                                    case "2":
                                        {
                                            month = 4;
                                            startTime = new DateTime(DateTime.Now.Year, month, 1);
                                            endTime = startTime.AddMonths(2).AddSeconds(-1);
                                            ControllerThongKe.ThongKe(startTime, endTime);
                                            break;
                                        }
                                    case "3":
                                        {
                                            month = 7;
                                            startTime = new DateTime(DateTime.Now.Year, month, 1);
                                            endTime = startTime.AddMonths(2).AddSeconds(-1);
                                            ControllerThongKe.ThongKe(startTime, endTime);
                                            break;
                                        }
                                    case "4":
                                        {
                                            month = 10;
                                            startTime = new DateTime(DateTime.Now.Year, month, 1);
                                            endTime = startTime.AddMonths(2).AddSeconds(-1);
                                            ControllerThongKe.ThongKe(startTime, endTime);
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                month = DateTime.Now.Month;
                                int currentQuarter = (month - 1) / 3 + 1;
                                if (currentQuarter == 1)
                                    month = 1;
                                else if (currentQuarter == 2)
                                    month = 4;
                                else if (currentQuarter == 3)
                                    month = 7;
                                else if (currentQuarter == 4)
                                    month = 10;
                                startTime = new DateTime(DateTime.Now.Year, month, 1);
                                endTime = startTime.AddMonths(2).AddSeconds(-1);
                                ControllerThongKe.ThongKe(startTime, endTime);
                            }
                            break;
                        }
                    case "4":
                        {
                            string input;
                            int year = DateTime.Now.Year;
                            Console.Write("\tNhập số năm (*): ".Magenta());
                            input = Console.ReadLine();
                            if (!string.IsNullOrEmpty(input))
                            {
                                year = int.Parse(input);
                            }
                            DateTime startTime = new DateTime(year, 1, 1);
                            DateTime endTime = startTime.AddYears(1).AddSeconds(-1);
                            ControllerThongKe.ThongKe(startTime, endTime);
                            break;
                        }
                    case "0":
                        {
                            if (ControllerXacNhan.IsXacNhan("Bạn có muốn quay lại không)?"))
                                Console.WriteLine("\t\tBạn đã chọn quay lại chương trình".Green());
                            else
                                chon = "";
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\t\tLựa chọn không hợp lệ. Vui lòng thử lại!".Yellow().BlueBg());
                            break;
                        }
                }
            } while (chon != "0");
        }
    }
}
