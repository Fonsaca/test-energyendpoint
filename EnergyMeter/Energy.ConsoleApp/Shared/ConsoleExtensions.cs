using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy.ConsoleApp.Shared
{
    public static class ConsoleExtensions
    {
        public static void Print(this string text) {
            Console.WriteLine(text);
        }
        public static void PrintInline(this string text) {
            Console.Write(text);
        }


        public static int? GetInt() {
            var line = Console.ReadLine();
            if (!Int32.TryParse(line, out int number))
                return null;
            return number;
        }
        public static string GetLine() {
            return Console.ReadLine();
        }

        public static void Clear() {
            Console.Clear();
        }


    }
}
