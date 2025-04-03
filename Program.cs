using Project.Controller;
using Project.View;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
namespace Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            Console.OutputEncoding = Encoding.UTF8;
            ViewLogin viewLogin = new ViewLogin();
            viewLogin.Login();
            Console.ReadKey();
        }
    }
}