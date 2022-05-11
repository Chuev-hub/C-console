using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    class Program
    {
        static public int option = 0;
        static public bool first = true;
        static void Main(string[] args)
        {
            Console.Title = "ThereCouldBeYourAd(095-557-89-28)";
            Console.SetWindowSize(120, 40);
            ConsoleManager.FillingData();
            InterfaceDesign.SetModeByTime();
            Console.Clear();
            Engine.ConsoleOption();
        }
    }
}
