using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
namespace Project
{
    class Engine
    {
        
        static List<List<string>> rightList = new List<List<string>>();
        static List<List<string>> leftList = new List<List<string>>();
        static DriveInfo[] ooo;
        static public void RemoveStar(ref string s)
        {
            bool isThere = false;
            foreach (var i in s)
                if (i == '*')
                    isThere = true;
            if (isThere)
            {
                string[] ss = s.Split('*');
                s = string.Empty;
                s += ss[0] + " " + ss[1];
            }
        }
        static public void RemoveChar(ref string s)
        {
            bool isThere = false;
            foreach (var i in s)
                if (i == '\0')
                    isThere = true;
            if (isThere)
            {
                string[] ss = s.Split('\0');
                s = string.Empty;
                s += ss[0];
                s += ss[1];
            }
            isThere = false;
            foreach (var i in s)
                if (i == '\r')
                    isThere = true;
            if (isThere)
            {
                string[] ss = s.Split('\r');
                s = string.Empty;
                s += ss[0];
                s += ss[1];
            }

        }
        static public void readLine(ref string s)
        {

            ConsoleKeyInfo key = new ConsoleKeyInfo();

            int hisI = ConsoleManager.history.Count;

            while (key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                    break;
                }
                if (key.Key == ConsoleKey.F1)
                {
                    Console.Clear();

                    FatOption();
                    break;
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (ConsoleManager.history.Count != 0 && hisI != 0)
                    {
                        Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop);
                        Console.Write("                                                  ");
                        Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop);
                        hisI--;
                        s = ConsoleManager.history[hisI];

                        RemoveChar(ref s);
                        Console.Write(s);
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (ConsoleManager.history.Count != 0 && hisI != ConsoleManager.history.Count && hisI != ConsoleManager.history.Count - 1)
                    {
                        Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop);
                        Console.Write("                                                  ");
                        Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop);
                        hisI++;
                        s = ConsoleManager.history[hisI];
                        RemoveChar(ref s);

                        Console.Write(s);
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                }
                if (key.Key == ConsoleKey.RightArrow)
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                if (key.Key == ConsoleKey.LeftArrow)
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                if (key.Key == ConsoleKey.F3)
                {
                    InterfaceDesign.ChangeTheme();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (s.Length > 0)
                    {
                        s = s.Remove(s.Length - 1);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                    else Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 14, Console.CursorTop);
                }
                else s += key.KeyChar;
            }

            RemoveChar(ref s);


        }
        static public string Paths(string s)
        {
            if (s.Length == 3)
            {
                return s;
            }
            else return Path.GetFileNameWithoutExtension(s);
        }
        static public string PathsFar(string s, string dir)
        {
            if (s.Length == 3)
            {
                return s;
            }
            else return s.Remove(0, dir.Length);
        }
        static public void readKey(ref bool isRght, ref ListInfo r, ref ListInfo l)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            key = Console.ReadKey();
            if (key.Key == ConsoleKey.LeftArrow)
            {
                isRght = !isRght;
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                isRght = !isRght;
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (isRght && l.path != "")
                {
                    if (leftList[l.page].Count != 0)
                    {
                        if (Directory.GetParent(leftList[l.page][l.pos]).FullName.Length == 3)
                            l.path = "";
                        else
                            l.path = Directory.GetParent(l.path).FullName;
                    }
                    else
                        l.path = Directory.GetParent(l.path).FullName;
                    l.page = 0;
                    l.pos = 0;
                }
                else if (!isRght && r.path != "")
                {
                    if (rightList[r.page].Count != 0)
                    {
                        if (Directory.GetParent(rightList[r.page][r.pos]).FullName.Length == 3)
                            r.path = "";
                        else
                            r.path = Directory.GetParent(r.path).FullName;
                    }
                    else
                        r.path = Directory.GetParent(r.path).FullName;
                    r.page = 0;
                    r.pos = 0;
                }
            }

            if (key.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            if (key.Key == ConsoleKey.DownArrow)
            {
                if (isRght)
                    l.pos++;
                else
                    r.pos++;
            }

            if (key.Key == ConsoleKey.UpArrow)
            {
                if (isRght)
                    l.pos--;
                else
                    r.pos--;
            }
            if (key.Key == ConsoleKey.F9)
            {
                try
                {
                    if (isRght)
                    {
                        string cmd = "";
                        if (File.Exists(leftList[l.page][l.pos]))
                            cmd = "movef";
                        else
                            cmd = "move";
                        string[] s = new string[3];
                        Directory.SetCurrentDirectory(l.path);

                        s[0] = cmd;
                        s[1] = PathsFar(leftList[l.page][l.pos], l.path);
                        s[2] = r.path;
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                    else
                    {
                        string cmd = "";
                        if (File.Exists(rightList[r.page][r.pos]))
                            cmd = "movef";
                        else
                            cmd = "move";
                        string[] s = new string[3];
                        Directory.SetCurrentDirectory(r.path);
                        s[0] = cmd;
                        s[1] = PathsFar(rightList[r.page][r.pos], r.path);
                        s[2] = l.path;
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                }
                catch
                {

                }
            }
            if (key.Key == ConsoleKey.F4)
            {
                try
                {
                    if (isRght)
                    {
                        string cmd = "del";
                        
                        string[] s = new string[2];
                        Directory.SetCurrentDirectory(l.path);

                        s[0] = cmd;
                        s[1] = PathsFar(leftList[l.page][l.pos], l.path);
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                    else
                    {
                        string cmd = "del";
                        string[] s = new string[2];
                        Directory.SetCurrentDirectory(r.path);
                        s[0] = cmd;
                        s[1] = PathsFar(rightList[r.page][r.pos], r.path);
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                }
                catch
                {

                }
            }
            if (key.Key == ConsoleKey.F10)
            {
                try
                {
                    if (isRght)
                    {
                        string cmd = "";
                        if (File.Exists(leftList[l.page][l.pos]))
                            cmd = "copyf";
                        else
                            cmd = "copy";
                        string[] s = new string[3];
                        Directory.SetCurrentDirectory(l.path);

                        s[0] = cmd;
                        s[1] = PathsFar(leftList[l.page][l.pos], l.path);
                        s[2] = r.path;
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                    else
                    {
                        string cmd = "";
                        if (File.Exists(rightList[r.page][r.pos]))
                            cmd = "copyf";
                        else
                            cmd = "copy";
                        string[] s = new string[3];
                        Directory.SetCurrentDirectory(r.path);
                        s[0] = cmd;
                        s[1] = PathsFar(rightList[r.page][r.pos], r.path);
                        s[2] = l.path;
                        ConsoleManager.IsComand(cmd).Invoke(s);
                    }
                }
                catch
                {

                }
            }


            if (key.Key == ConsoleKey.Enter)
            {
                try
                {
                    if (isRght)
                    {
                        //if (Directory.GetDirectories(leftList[l.page][l.pos]).Length == 0 && Directory.GetFiles(leftList[l.page][l.pos]).Length == 0)
                        //{
                        //    Console.WriteLine();
                        //    Console.BackgroundColor = ConsoleColor.Red;
                        //    Console.ForegroundColor = ConsoleColor.White;
                        //    Console.Write("empty");
                        //    Thread.Sleep(1000);
                        //}
                        //else
                        {
                            l.path = leftList[l.page][l.pos];
                            l.page = 0;
                            l.pos = 0;
                        }
                    }
                    else
                    {
                        //if (Directory.GetDirectories(rightList[r.page][r.pos]).Length == 0 && Directory.GetFiles(rightList[r.page][r.pos]).Length == 0)
                        //{
                        //    Console.WriteLine();
                        //    Console.BackgroundColor = ConsoleColor.Red;
                        //    Console.ForegroundColor = ConsoleColor.White;
                        //    Console.Write("empty");
                        //    Thread.Sleep(1000);
                        //}
                        //else
                        {
                            r.path = rightList[r.page][r.pos];
                            r.page = 0;
                            r.pos = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(System.UnauthorizedAccessException))
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("system folder!");
                        Thread.Sleep(1000);
                    }
                }

            }
            if (key.Key == ConsoleKey.F1)
            {
                Console.Clear();
                rightList.Clear();
                leftList.Clear();
                ConsoleOption();
            }
            if (key.Key == ConsoleKey.F3)
            {
                InterfaceDesign.ChangeThemeFar();
            }


        }

        static public void ConsoleOption()
        {
            Console.SetWindowSize(120, 40);
            Program.option =  0;

            InterfaceDesign.SetModeByCurrent();
            Console.Clear();
            InterfaceDesign.Gray();
            Console.Write("Esc[Exit] F1[change option] F3 [change theme(must clear)]                              ThereCouldBeYourAd(095-557-89-28)");
            InterfaceDesign.SetModeByCurrent();
            InterfaceDesign.Green();
            Console.WriteLine("\n      version 0.0.1 \n    made By c# b!tcheZ");
            Console.WriteLine();
            InterfaceDesign.SetModeByCurrent();

            string comand = "";
            ooo = DriveInfo.GetDrives();
            Directory.SetCurrentDirectory(ooo[0].Name);


            while (true)
            {
                InterfaceDesign.Cyan();
                Console.Write("F#ckingCnsl ");
                InterfaceDesign.SetModeByCurrent();
                Console.Write(Directory.GetCurrentDirectory() + ">");
                for (int i = 0; i < 120 - Directory.GetCurrentDirectory().Length + 13; i++)
                    Console.Write(" ");

                if (Program.first)
                    Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop - 1);
                else
                    Console.SetCursorPosition(Directory.GetCurrentDirectory().Length + 13, Console.CursorTop);

                readLine(ref comand);
                Console.WriteLine();
                string[] s = comand.Split(' ');
                if (ConsoleManager.IsComand(s[0]) == null && comand.Length > 0)
                {
                    ConsoleManager.Unknown();
                }
                else if (ConsoleManager.IsComand(s[0]) != null)
                    ConsoleManager.IsComand(s[0]).Invoke(s);

                Console.WriteLine();
                ConsoleManager.history.Add(comand);
                comand = "";

            }
        }
        static public void FatOption()
        {
            Program.option = 1;
            Console.Clear();
            Console.SetWindowSize(210, 40);
            Program.first = false;
            InterfaceDesign.PrintFrame();
            ListInfo right = new ListInfo(0);
            ListInfo left = new ListInfo(0);

            bool isRight = false;


            rightList.Add(new List<string>());
            leftList.Add(new List<string>());
            foreach (var i in ooo)
            {
                rightList[right.page].Add(i.Name);
                leftList[left.page].Add(i.Name);
            }

            while (true)
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ПРОВЕРКА/////////////////////////////////////////////////
                //////////////////////////////////////////на позицию и страницы////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                if (rightList.Count == 1)
                {
                    if (right.pos > rightList[0].Count - 1)
                        right.pos = rightList[0].Count - 1;
                    if (right.pos < 0) right.pos = 0;
                }
                else
                {
                    int countOf = 0;
                    for (int k = 0; k < rightList.Count; k++)
                        countOf += rightList[k].Count;

                    while (countOf >= 35)
                        countOf -= 35;


                    for (int i = 1; i < rightList.Count; i++)
                        if (right.pos == 35 * i)
                        {
                            right.pos = 0;
                            if (right.page != rightList.Count - 1)
                                right.page++;
                        }

                    if (right.pos == -1)
                    {
                        if (right.page != 0)
                        {
                            right.pos = 34;
                            right.page--;
                        }
                        else right.pos = 0;

                    }
                    if (right.pos > countOf - 1 && right.page == rightList.Count - 1)
                        right.pos = countOf - 1;
                }



                if (leftList.Count == 1)
                {
                    if (left.pos > leftList[0].Count - 1)
                        left.pos = leftList[0].Count - 1;
                    if (left.pos < 0) left.pos = 0;

                }
                else
                {
                    int countOf = 0;
                    for (int k = 0; k < leftList.Count; k++)
                        countOf += leftList[k].Count;

                    while (countOf >= 35)
                        countOf -= 35;


                    for (int i = 1; i < leftList.Count; i++)
                        if (left.pos == 35 * i)
                        {
                            left.pos = 0;
                            if (left.page != leftList.Count - 1)
                                left.page++;
                        }

                    if (left.pos == -1)
                    {
                        if (left.page != 0)
                        {
                            left.pos = 34;
                            left.page--;
                        }
                        else left.pos = 0;

                    }
                    if (left.pos > countOf - 1 && left.page == leftList.Count - 1)
                        left.pos = countOf - 1;
                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ПРОВЕРКА/////////////////////////////////////////////////
                //////////////////////////////////////////на позицию и страницы////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////




                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ПРОРИСОВКА///////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                int size = leftList[left.page].Count;
                if (rightList[right.page].Count > size)
                    size = rightList[right.page].Count;
                InterfaceDesign.DarkColorFar();
                Console.SetCursorPosition(8, 1);
                Console.Write(right.page);
                int strLenth = 105 - right.page.ToString().Length - 8;
                if (right.path != "")
                {
                    if (70 > right.path.Length)
                    {
                        Console.Write(" " + right.path);
                        for (int i = 0; i < strLenth - right.path.Length - 1; i++)
                            Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(" " + right.path[0] + right.path[1] + right.path[3] + " . . . \\" + Paths(right.path));
                        for (int i = 0; i < strLenth - 12 - Paths(right.path).Length; i++)
                            Console.Write(" ");
                    }

                }
                else
                    for (int i = 0; i < strLenth+1; i++)
                        Console.Write(" ");
                Console.Write("Page: " + left.page);
                strLenth = 106 - 8 - left.page.ToString().Length;
                if (left.path != "")
                {
                    if (70 > left.path.Length)
                    {
                        Console.Write(" " + left.path);
                        for (int i = 0; i < strLenth - left.path.Length - 1; i++)
                            Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(" " + left.path[0] + left.path[1] + left.path[3] + " . . . \\" + Paths(left.path));
                        for (int i = 0; i < strLenth - 12 - Paths(left.path).Length; i++)
                            Console.Write(" ");
                    }
                }
                else
                    for (int i = 0; i < strLenth-1; i++)
                        Console.Write(" ");

                
                Console.SetCursorPosition(0, 2);




                

                bool isFile = true;
                for (int i = 0; i < size; i++)
                {
                    Console.Write("  ");
                    InterfaceDesign.ColorFar();
                    if (rightList[right.page].Count - 1 >= i && leftList[left.page].Count - 1 >= i)
                    {

                        if (Directory.Exists(rightList[right.page][i]))
                            isFile = false;
                        else
                            isFile = true;

                        if (!isRight && i == right.pos)
                        {
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(PathsFar(rightList[right.page][i], right.path));
                            InterfaceDesign.ColorFar();
                        }
                        else
                        {
                            if (!isFile)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else
                                Console.ForegroundColor = ConsoleColor.Green;

                            Console.Write(PathsFar(rightList[right.page][i], right.path));
                            InterfaceDesign.ColorFar();
                        }

                        for (int h = 0; h < 102 - PathsFar(rightList[right.page][i], right.path).Length; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                        InterfaceDesign.ColorFar();

                        if (Directory.Exists(leftList[left.page][i]))
                            isFile = false;
                        else
                            isFile = true;

                        if (isRight && i == left.pos)
                        {
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(PathsFar(leftList[left.page][i], left.path));
                            InterfaceDesign.ColorFar();
                        }
                        else
                        {
                            if (!isFile)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else
                                Console.ForegroundColor = ConsoleColor.Green;

                            Console.Write(PathsFar(leftList[left.page][i], left.path));
                        }
                        for (int h = 0; h < 102 - PathsFar(leftList[left.page][i], left.path).Length; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                    }

                    else if (rightList[right.page].Count - 1 < i && leftList[left.page].Count - 1 >= i)
                    {

                        if (Directory.Exists(leftList[left.page][i]))
                            isFile = false;
                        else
                            isFile = true;
                        for (int h = 0; h < 102; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                        InterfaceDesign.ColorFar();
                        if (isRight && i == left.pos)
                        {
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(PathsFar(leftList[left.page][i], left.path));
                            InterfaceDesign.ColorFar();
                        }
                        else
                        {
                            if (!isFile)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else
                                Console.ForegroundColor = ConsoleColor.Green;

                            Console.Write(PathsFar(leftList[left.page][i], left.path));
                        }
                        for (int h = 0; h < 102 - PathsFar(leftList[left.page][i], left.path).Length; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                    }
                    else if (rightList[right.page].Count - 1 >= i && leftList[left.page].Count - 1 < i)
                    {


                        if (Directory.Exists(rightList[right.page][i]))
                            isFile = false;
                        else
                            isFile = true;
                        if (!isRight && i == right.pos)
                        {
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.Write(PathsFar(rightList[right.page][i], right.path));
                            InterfaceDesign.ColorFar();
                        }
                        else
                        {
                            if (!isFile)
                                Console.ForegroundColor = ConsoleColor.Blue;
                            else
                                Console.ForegroundColor = ConsoleColor.Green;

                            Console.Write(PathsFar(rightList[right.page][i], right.path));
                        }
                        for (int h = 0; h < 102 - PathsFar(rightList[right.page][i], right.path).Length; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                        InterfaceDesign.ColorFar();
                        for (int h = 0; h < 102; h++)
                            Console.Write(" ");
                        InterfaceDesign.DarkColorFar();
                        Console.Write("  ");
                    }
                }
                for (int i = 0; i < 35-size; i++)
                {
                    InterfaceDesign.DarkColorFar();
                    Console.Write("  ");
                    InterfaceDesign.ColorFar();
                    for (int h = 0; h < 102; h++)
                        Console.Write(" ");
                    InterfaceDesign.DarkColorFar();
                    Console.Write("  ");
                    InterfaceDesign.ColorFar();
                    for (int h = 0; h < 102; h++)
                        Console.Write(" ");
                    InterfaceDesign.DarkColorFar();
                    Console.Write("  ");
                }

                InterfaceDesign.DarkColorFar();
                
                Console.SetCursorPosition(208,38);
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ПРОРИСОВКА///////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////







                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ЗАПОЛНЕНИЕ///////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                List<List<string>> rz= new List<List<string>>();
                List<List<string>> lz = new List<List<string>>();
                foreach (var i in rightList)
                    rz.Add(i);
                foreach (var i in leftList)
                    lz.Add(i);
                ListInfo r = new ListInfo();
                ListInfo l = new ListInfo();
                r.page = right.page;
                r.path = right.path;
                r.pos = right.pos;
                l.page = left.page;
                l.path = left.path;
                l.pos = left.pos;

                try
                {
                    readKey(ref isRight, ref right, ref left);

                    rightList.Clear();
                    leftList.Clear();
                    rightList.Add(new List<string>());
                    leftList.Add(new List<string>());


                    if (right.path != "")
                    {
                        List<string> d = new List<string>();
                        foreach (var i in Directory.GetDirectories(right.path, "*", SearchOption.TopDirectoryOnly))
                            d.Add(i);
                        foreach (var i in Directory.GetFiles(right.path, "*", SearchOption.TopDirectoryOnly))
                            d.Add(i);

                        if (d.Count < 35)
                            foreach (var i in d)
                                rightList[right.page].Add(i);
                        else
                        {
                            int e = 0;
                            int count = d.Count;
                            int numPages = 0;
                            while (count >= 35)
                            {
                                count -= 35;
                                numPages++;
                            }
                            for (int i = 0; i < numPages - 1; i++)
                            {
                                for (int j = 0; j < 35; j++, e++)
                                    rightList[i].Add(d[e]);
                                if (i != numPages - 2)
                                    rightList.Add(new List<string>());
                            }
                            rightList.Add(new List<string>());

                            for (int j = 0; j < count; j++, e++)
                                rightList[numPages - 1].Add(d[e]);
                        }
                    }
                    else
                        foreach (var i in ooo)
                            rightList[right.page].Add(i.Name);


                    if (left.path != "")
                    {
                        List<string> d = new List<string>();
                        foreach (var i in Directory.GetDirectories(left.path, "*", SearchOption.TopDirectoryOnly))
                            d.Add(i);
                        foreach (var i in Directory.GetFiles(left.path, "*", SearchOption.TopDirectoryOnly))
                            d.Add(i);

                        if (d.Count < 35)
                            foreach (var i in d)
                                leftList[left.page].Add(i);
                        else
                        {
                            int e = 0;
                            int count = d.Count;
                            int numPages = 0;
                            while (count >= 35)
                            {
                                count -= 35;
                                numPages++;
                            }
                            for (int i = 0; i < numPages - 1; i++)
                            {
                                for (int j = 0; j < 35; j++, e++)
                                    leftList[i].Add(d[e]);
                                if (i != numPages - 2)
                                    leftList.Add(new List<string>());
                            }
                            leftList.Add(new List<string>());

                            for (int j = 0; j < count; j++, e++)
                                leftList[numPages - 1].Add(d[e]);
                        }
                    }
                    else
                        foreach (var i in ooo)
                            leftList[left.page].Add(i.Name);
                }
                catch
                {
                    leftList = lz;
                    rightList = rz;
                    right = r;
                    left = l;
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////ЗАПОЛНЕНИЕ///////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

            }
        }

    }
}
