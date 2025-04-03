using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using ConsoleGUI;
using ConsoleTableExt;
namespace Project.Model
{
    internal class ModelPrint
    {
        public static void PrintData(SqlDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();

            if (schemaTable == null || schemaTable.Rows.Count == 0)
            {
                Console.WriteLine("Không tìm thấy dữ liệu...");
                return;
            }

            ConsoleTable table = new ConsoleTable();
            string[] columnNames = new string[schemaTable.Rows.Count];
            for (int i = 0; i < schemaTable.Rows.Count; i++)
            {
                columnNames[i] = schemaTable.Rows[i]["ColumnName"].ToString();
            }
            table.AddColumn(columnNames);
            while (reader.Read())
            {
                object[] row = new object[table.Columns.Count];
                reader.GetValues(row);
                table.AddRow(row);
            }
            int tableWidth = table.ToString().Split(Environment.NewLine)[0].Length;
            int leftMargin = (Console.WindowWidth - tableWidth) / 2;
            if (leftMargin < 0)
            {
                leftMargin = 0;
            }

            
            Console.SetCursorPosition(leftMargin, Console.CursorTop + 1);
            
            table.Options.EnableCount = false;

            var tableString = table.ToMarkDownString();
            var lines = tableString.Split(Environment.NewLine);
            int lineCount = 0;
            foreach (var row in lines)
            {
                
                Console.SetCursorPosition(leftMargin, Console.CursorTop);

                if (lineCount == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (row.StartsWith("|"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine(row);
                lineCount++;
                
            }
            Console.ResetColor();
        }
    }
}