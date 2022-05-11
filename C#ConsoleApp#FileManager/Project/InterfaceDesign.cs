using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class InterfaceDesign
    {
        static public int currentMode;
        static public void PrintFrame()
        {
            Console.SetCursorPosition(0, 0);
            InterfaceDesign.DarkColorFar();
            InterfaceDesign.Yellow();
            Console.Write("                                                                                           bad far manager b#tches                                                                                                ");
            InterfaceDesign.DarkColorFar();
            Console.Write("  Page:                                                                                                   Page:                                                                                                   ");
            for (int i = 0; i < 35; i++)
            {
                InterfaceDesign.DarkColorFar();
                Console.Write("  ");
                InterfaceDesign.ColorFar();
                Console.Write("                                                                                                      ");
                InterfaceDesign.DarkColorFar();
                Console.Write("  ");
                InterfaceDesign.ColorFar();
                Console.Write("                                                                                                      ");
                InterfaceDesign.DarkColorFar();
                Console.Write("  ");
            }
            for (int i = 0; i < 210; i++)
                Console.Write(" ");
            InterfaceDesign.DarkColorFar();
            Console.Write("  Esc[Exit] F1[change option] F3 [change theme] F9[move] F10[copy] F4[del]  [arrows to move, backspace to back]                                                               ThereCouldBeYourAd(095-557-89-28) ");
            for (int i = 0; i < 210; i++)
                Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop - 1);
        }
        static public void ChangeTheme()
        {
            if (currentMode == 0)
            {
                currentMode = 1;
                SetDarkMode();
            }
            else if (currentMode == 1)
            {
                currentMode = 0;
                SetLightMode();
            }
            Console.Clear();
            InterfaceDesign.Gray();
            Console.Write("Esc[Exit] F1[change option] F3 [change theme(must clear)]                              ThereCouldBeYourAd(095-557-89-28)");
            InterfaceDesign.SetModeByCurrent();
            Yellow();
            Console.Write("Theme Changed!");
            SetModeByCurrent();
        }
        static public void ChangeThemeFar()
        {
            if (currentMode == 0)
            {
                currentMode = 1;
                SetDarkMode();
            }
            else if (currentMode == 1)
            {
                currentMode = 0;
                SetLightMode();
            }
            Console.Clear();
            PrintFrame();
        }

        static public void SetDarkMode()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static public void SetLightMode()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static public void SetModeByTime()
        {
            if (DateTime.Now.Hour > 5 && DateTime.Now.Hour < 18)
            {
                SetLightMode();
                currentMode = 0;
            }
            else
            {
                SetDarkMode();
                currentMode = 1;
            }
        }
        static public void DarkColorFar()
        {
            if (currentMode == 0)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Magenta;
            }

            if (currentMode == 1)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        static public void ColorFar()
        {
            if (currentMode == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

            if (currentMode == 1)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
        static public void SetModeByCurrent()
        {
            if (currentMode == 0)
                SetLightMode();
            if (currentMode == 1)
                SetDarkMode();
        }
        static public void Red()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Red;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkRed;
            
        }
        static public void Green()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Green;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            
        }
        static public void Magenta()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Magenta;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
           
        }
        static public void Cyan()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Cyan;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkCyan;

        }
        static public void Gray()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Gray;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        static public void Yellow()
        {
            if (currentMode == 1)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (currentMode == 0)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

    }
}
