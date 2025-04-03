using CSharpExtensions.OpenSource.ConsoleColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    internal class ControllerXacNhan
    {
        public static bool IsXacNhan(string Xau)
        {
            Console.WriteLine("\t\t\tNhấn Enter để đồng ý".Red());
            Console.Write("\t\t {0} (Y/N): ".Yellow(), Xau);
            string input = Console.ReadLine();
            if ((input != null && input.ToLower().Equals("n")))
            {
                return false;
            }
            return true;
        }
    }
}
