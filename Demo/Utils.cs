using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Utils
    {
        public static void Print(string s, ConsoleColor f = ConsoleColor.White,bool Cr=false)
        {
            ConsoleColor c = System.Console.ForegroundColor;                                                                    // This is the current color
            Console.ForegroundColor = f;                                                                                        // ConsoleColor whatever it takes
            if (Cr) Console.WriteLine(s); else Console.Write(s);                                                                // Print 
            Console.ForegroundColor = c;                                                                                        // Restore color    
        }

        public static void Blue(string msg, bool Cr = false) => Print(msg, ConsoleColor.Blue,Cr);
        public static void Red(string msg, bool Cr = false) => Print(msg, ConsoleColor.Red, Cr);
        public static void White(string msg, bool Cr = false) => Print(msg, ConsoleColor.White, Cr);
        public static void Yellow(string msg, bool Cr = false) => Print(msg, ConsoleColor.Yellow, Cr);
        public static void Green(string msg, bool Cr = false) => Print(msg, ConsoleColor.Green, Cr);
        public static void Cyan(string msg, bool Cr = false) => Print(msg, ConsoleColor.Cyan, Cr);
        public static void Gray(string msg, bool Cr = false) => Print(msg, ConsoleColor.Gray, Cr);
        public static void DarkGray(string msg, bool Cr = false) => Print(msg, ConsoleColor.DarkGray, Cr);
        public static void DarkBlue(string msg, bool Cr = false) => Print(msg, ConsoleColor.DarkBlue, Cr);
    }
}
