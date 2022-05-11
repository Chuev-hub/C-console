using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Threading;
namespace Project
{
    static class ConsoleManager
    {
        public delegate void Method(string[] s);
        static Dictionary<string, Method> comands = new Dictionary<string, Method>();
        static public List<string> history = new List<string>();

        static public void FillingData()
        {
            comands.Add("cd..", CdBack);
            comands.Add("cd", CdFront);
            comands.Add("cdd", Cdd); 
            comands.Add("cddd", CddShow);
            comands.Add("dir", Dir);
            comands.Add("help", Help);
            comands.Add("move", Move);
            comands.Add("copy", Copy);
            comands.Add("copyf", CopyF);
            comands.Add("touch", Touch);
            comands.Add("nano", Nano);
            comands.Add("clear", Clear);
            comands.Add("atr", Atr);
            comands.Add("del", Del);
            comands.Add("delf", DelF);
            comands.Add("mkdir", Mkdir);
            comands.Add("his", His);
            comands.Add("rename", Rename);
            comands.Add("search", Search);
            comands.Add("hackNasa", HackNasa);
            comands.Add("movef", MoveF);
        }
        static public Method IsComand(string cmd)
        {
            string[] mas = cmd.Split();
            if (mas.Length < 0 || mas.Length > 3)
                return null;
            else
            {
                    if (comands.ContainsKey(mas[0]))
                        return comands[mas[0]];
            }
            return null;
        }
        static public void Unknown()
        {
            if (Program.option == 0)
            {


                InterfaceDesign.Red();
                Console.Write("unfined command, use -help                                                                        ");
                InterfaceDesign.SetModeByCurrent();
            }
        }
        static void Dir(string[] s)
        {
            Console.WriteLine();
            foreach (var i in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                for (int j = Directory.GetCurrentDirectory().Length ; j < i.Length; j++)
                    Console.Write(i[j]);
                Console.WriteLine();
            }
            foreach (var i in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                for (int j = Directory.GetCurrentDirectory().Length ; j < i.Length; j++)
                    Console.Write(i[j]);
                Console.WriteLine();
            }
        }
        static void Copy(string[] s)
        {
            if (s.Length != 3)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveStar(ref s[2]);
                try
                {
                    Engine.RemoveChar(ref s[2]);

                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    {
                        //Now Create all of the directories
                        Directory.CreateDirectory(s[2] + "\\" + s[1]);
                        foreach (string dirPath in Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\" + s[1], "*", SearchOption.AllDirectories))
                            Directory.CreateDirectory(s[2] + "\\" + dirPath.Remove(0, Directory.GetCurrentDirectory().Length + 1));

                        //Copy all the files & Replaces any files with the same name
                        foreach (string newPath in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + s[1], "*.*", SearchOption.AllDirectories))
                            File.Copy(newPath, s[2] + "\\" + newPath.Remove(0, Directory.GetCurrentDirectory().Length + 1), true);//берется старый путь, потом от него о
                        if (Program.option == 0)
                        {
                            InterfaceDesign.Green();
                            Console.Write("copyed");
                            InterfaceDesign.SetModeByCurrent();
                        }
                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch (Exception e)
                {
                    Unknown();
                    if (Program.option == 0)
                        Console.WriteLine(e.Message);
                }
            }
        }
        static void Move(string[] s)
        {
            if (s.Length != 3)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveStar(ref s[2]);
                try
                {
                    Engine.RemoveChar(ref s[2]);
                    
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    {
                        //Now Create all of the directories
                        Directory.CreateDirectory(s[2] +"\\"+ s[1]);
                        foreach (string dirPath in Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\" + s[1], "*", SearchOption.AllDirectories))
                            Directory.CreateDirectory(s[2] + "\\" + dirPath.Remove(0, Directory.GetCurrentDirectory().Length + 1));

                        //Copy all the files & Replaces any files with the same name
                        foreach (string newPath in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + s[1], "*.*", SearchOption.AllDirectories))
                            File.Copy(newPath, s[2]+ "\\" + newPath.Remove(0, Directory.GetCurrentDirectory().Length + 1), true);//берется старый путь, потом от него о
                        if (Program.option == 0)
                        {
                            InterfaceDesign.Green();
                            Console.Write("moved");
                            InterfaceDesign.SetModeByCurrent();
                        }
                        Directory.Delete(Directory.GetCurrentDirectory() + "\\" + s[1],true);
                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch (Exception e)
                {
                    Unknown();
                    if (Program.option == 0)
                        Console.WriteLine(e.Message);
                }
            }
        }
        static void MoveF(string[] s)
        {
            if (s.Length != 3)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveStar(ref s[2]);
                try
                {
                    Engine.RemoveChar(ref s[2]);
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    {
                        File.Copy(Directory.GetCurrentDirectory() + "\\" + s[1], s[2] +"\\"+ s[1], true);
                        File.Delete(Directory.GetCurrentDirectory() + "\\" + s[1]);
                        if (Program.option == 0)
                        {
                            InterfaceDesign.Green();
                            Console.Write("moved");
                            InterfaceDesign.SetModeByCurrent();
                        }
                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch (Exception e)
                {
                    Unknown();
                    if (Program.option == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        static void CopyF(string[] s)
        {
            if (s.Length != 3)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveStar(ref s[2]);
                try
                {
                    Engine.RemoveChar(ref s[2]);
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    {
                        File.Copy(Directory.GetCurrentDirectory() + "\\" + s[1], s[2]  +"\\"+ s[1],true);
                        if (Program.option == 0)
                        {
                            InterfaceDesign.Green();
                            Console.Write("copied");
                            InterfaceDesign.SetModeByCurrent();
                        }

                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch (Exception e)
                {
                    Unknown();
                    if (Program.option == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        static void Help(string[] s)
        {
            InterfaceDesign.Magenta();
            Console.WriteLine("del [path]             - Удаление директорий.");                                            ////
            Console.WriteLine("mkdir [name]           - Создание директорий.");                                            ////
            Console.WriteLine("clear                  - Очистка экрана.");                                                 ////
            Console.WriteLine("dir                    - Отображение всех файлов и папок в текущей директории.");           ////
            Console.WriteLine("his                    - Просмотр истории введенных команд.");                              ////
            Console.WriteLine("cd.. / cd [path]       - Перемещение по директориям.       ");                              ////
            Console.WriteLine("cdd [name]             - Перемещение к диску.       ");                                     ////
            Console.WriteLine("cddd                   - Диски.                     ");                                     ////
            Console.WriteLine("touch [name]           - Создание текстовых файлов.");                                      ////
            Console.WriteLine("delf [name]            - Удаление текстовых файлов.");                                      ////
            Console.WriteLine("nano  [name]           - Просмотр текстовых файлов.");                                      ////
            Console.WriteLine("atr [path]             - Просмотр атрибутов указанного файла.");                            ////
            Console.WriteLine("rename [path]          - Переименование файлов.");                                          ////
            Console.WriteLine("movef [name] [path]    - Перемещение файлов.");                                             ////
            Console.WriteLine("move [name] [path]     - Перемещение папок.");                                              ////
            Console.WriteLine("copy [name] [path]     - Копирование папок.");                                              ////
            Console.WriteLine("copyf [name] [path]    - Копирование файлов");                                              ////
            Console.WriteLine("search [name]          - Поиск файлов в этой директории.");                                 ////
            Console.WriteLine("hackNasa               - **********************************");                              ////
            Console.WriteLine("ПРОБЕЛЫ В НАЗВАНИЯХ ДИРЕКТОРИЙ ЗАПОЛНЯТЬ ЗВёЗДОЧАМИ");
            InterfaceDesign.Gray();
            Console.Write("Esc[Exit] F1[change option] F3 [change theme(must clear)]                              ThereCouldBeYourAd(095-557-89-28)");
            InterfaceDesign.SetModeByCurrent();
        }
        static void HackNasa(string[] s)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            InterfaceDesign.Cyan();
            Console.Write("F#ckingCnsl ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Directory.GetCurrentDirectory()+ ">hackNasa");
            for (int i = 0; i < 101; i++)
            {
                if (i == 100)
                    InterfaceDesign.Green();
                Console.Write("start " + i + "%");
                Thread.Sleep(10);
                Console.SetCursorPosition(Console.CursorLeft - 7 - Convert.ToString(i).Length, Console.CursorTop);

            }
            Console.WriteLine();
            //hackNasa
            Console.Write("identify .");
            Thread.Sleep(1000);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" .");
            Thread.Sleep(1000);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" .");
            Thread.Sleep(1000);
            Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
            Console.Write(".   ");
            Console.SetCursorPosition(Console.CursorLeft - 3, Console.CursorTop);
            Thread.Sleep(1000);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" .");
            Thread.Sleep(1000);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" .");
            Thread.Sleep(1000);
            Random r = new Random();
            InterfaceDesign.Red();
            for (int i=0;i<5000;i++)
            {
                for (int j = 0; j < r.Next(50,105); j++)
                    Console.Write(Convert.ToChar(j* r.Next(1, 5)));
                Console.WriteLine();
            }
            InterfaceDesign.Green();
            Console.WriteLine("hacked! sakseasful");
            Console.ReadKey();
            InterfaceDesign.SetModeByCurrent();
            Console.Clear();
            InterfaceDesign.Gray();
            Console.Write("Esc[Exit] F1[change option] F3 [change theme(must clear)]                              ThereCouldBeYourAd(095-557-89-28)");
            InterfaceDesign.SetModeByCurrent();
        }
        static void CdBack(string[] s)
        {
            
            string ss = "";
            string[] path= Directory.GetCurrentDirectory().Split('\\');
            for (int i = 0; i < Directory.GetCurrentDirectory().Length - path[path.Length-1].Length; i++)
                ss += Directory.GetCurrentDirectory()[i];
            Directory.SetCurrentDirectory(ss);
        }
        static void CdFront(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);
                bool a = false;
                string[] ss = new string[Directory.GetDirectories(Directory.GetCurrentDirectory()).Length];
                int j = 0;
                foreach (var i in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                {
                    ss[j] = i;
                    j++;
                }
                string path = Directory.GetCurrentDirectory();
                if (path[path.Length - 1] != '\\')
                    path += '\\';
                path += s[1];
                for (int i=0;i<ss.Length;i++)
                {
                    if (ss[i]  == path)
                        a = true;
                }
                if (a)
                    Directory.SetCurrentDirectory(path);
                else
                {
                    Unknown();
                }
            }
        }
        static void Touch(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
              
                Engine.RemoveChar(ref s[1]);
                int o = 0;
                try
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    {
                        InterfaceDesign.Green();
                        Console.Write("This file already created");
                        InterfaceDesign.SetModeByCurrent();
                        o = 1;
                    }
                    else
                    {
                        FileStream fs = File.Create(Directory.GetCurrentDirectory() + "\\" + s[1]);
                        fs.Close();
                    }
                }
                catch (Exception)
                {
                    InterfaceDesign.Red();
                    Console.Write("This file cant be created here");
                    InterfaceDesign.SetModeByCurrent();
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                {
                    InterfaceDesign.Green();
                    if (o == 0)
                        Console.Write("This file created");
                    InterfaceDesign.SetModeByCurrent();
                }
            
        }
        }
        static void Nano(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveChar(ref s[1]);
                Engine.RemoveStar(ref s[1]);
                try
                { 
                    if (File.Exists(Directory.GetCurrentDirectory() + "\\" + s[1]))
                    { 
                        using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\" + s[1]))
                        {
                            Console.WriteLine(sr.ReadToEnd());
                        }
                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch (Exception)
                {
                    Unknown();
                }
            }
        }
        static void Clear(string[] s)
        {
            Console.Clear();
            InterfaceDesign.Gray();
            Console.Write("Esc[Exit] F1[change option] F3 [change theme(must clear)]                              ThereCouldBeYourAd(095-557-89-28)");
            InterfaceDesign.SetModeByCurrent();
        }
        static void Atr(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {

                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);
                string path = Directory.GetCurrentDirectory() + "\\" + s[1];
                FileInfo f = new FileInfo(path);
                Console.WriteLine(f.Attributes);
            }
        }
        static void Del(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);
                string path = Directory.GetCurrentDirectory() + "\\" + s[1];
                try
                {


                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                        InterfaceDesign.Green();
                        Console.Write("deleted");
                        InterfaceDesign.SetModeByCurrent();
                    }
                    else if (File.Exists(path))
                    {
                        File.Delete(path);
                        InterfaceDesign.Green();
                        Console.Write("deleted");
                        InterfaceDesign.SetModeByCurrent();
                    }
                    else
                    {
                        Unknown();
                    }
                }
                catch { }
            }
        }
        static void DelF(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);

                string path = Directory.GetCurrentDirectory() + "\\" + s[1];
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
        static void Mkdir(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);

                int o = 0;
                string path = Directory.GetCurrentDirectory() + "\\" + s[1];
                try
                {
                    if (Directory.Exists(path))
                    {
                        InterfaceDesign.Green();
                        Console.Write("This dir already created");
                        InterfaceDesign.SetModeByCurrent();
                        o = 1;
                    }
                    else
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                catch(Exception)
                {
                    InterfaceDesign.Red();
                    Console.Write("This dir cant be created here");
                    InterfaceDesign.SetModeByCurrent();
                }
                if (Directory.Exists(path))
                {
                    InterfaceDesign.Green();
                    if(o==0)
                    Console.Write("This dir created");
                    InterfaceDesign.SetModeByCurrent();
                }
                }
        }
        static void His(string[] s)
        {
            Console.WriteLine();
            foreach ( var a in history)
                Console.WriteLine(a);
        }
        static void Rename(string[] s)
        {
            if (s.Length != 3)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveStar(ref s[2]);
                Engine.RemoveChar(ref s[2]);
                try
                {
                    string path = Directory.GetCurrentDirectory() + "\\" + s[1];
                    if (File.Exists(path))
                    {
                        File.Move(path, Directory.GetCurrentDirectory() + "\\" + s[2]);
                    }
                    else
                    {
                        Unknown();                        
                    }
                }
                catch (Exception e)
                {
                    if (Program.option == 0)
                        Console.WriteLine(e.Message);
                    Unknown();
                }
                
            }
        }
        static void Search(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[2]);
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);
                foreach (var i in SearchM(Directory.GetCurrentDirectory(), s[1]))
                        Console.WriteLine(i);
            }
        }
        static IEnumerable<string> SearchM(string root, string searchPattern)
        {
            Queue<string> dirs = new Queue<string>();
            dirs.Enqueue(root);
            while (dirs.Count > 0)
            {
                string dir = dirs.Dequeue();

                // поиск по текущей директории
                string[] paths = null;
                try
                {
                    paths = Directory.GetFiles(dir, searchPattern);
                }
                catch { }

                if (paths != null && paths.Length > 0)
                {
                    foreach (string file in paths)
                    {
                        yield return file; //Оператор yield return используется для возврата каждого элемента по одному.
                    }
                }

                // поискать папки в этой директории
                paths = null;
                try
                {
                    paths = Directory.GetDirectories(dir);
                }
                catch { } 

                if (paths != null && paths.Length > 0)
                {
                    foreach (string subDir in paths)
                    {
                        dirs.Enqueue(subDir);
                    }
                }
            }
        }
        static void Cdd(string[] s)
        {
            if (s.Length != 2)
                Unknown();
            else
            {
                Engine.RemoveStar(ref s[1]);
                Engine.RemoveChar(ref s[1]);
                bool a = false;
                
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (var i in allDrives)
                    if (i.Name == s[1] + ":\\")
                        a = true;
                if (a)
                    Directory.SetCurrentDirectory(s[1] + ":\\");
                else Unknown();
            }
        }
        static void CddShow(string[] s)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (var i in allDrives)
                   Console.WriteLine(i.Name);
            
        }
    }
}
