using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CameraFileCopier2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string pathToCopyFrom = Clipboard.GetText();
            Console.WriteLine(pathToCopyFrom);
            Console.WriteLine("Continue? (Y/N)");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key != ConsoleKey.Y)
            {
                return;
            }
            Console.WriteLine();
            List<string> files = Directory.GetFiles(pathToCopyFrom).ToList();
            List<Tuple<string, string>> renamed = new List<Tuple<string, string>>();
            foreach (string file in files)
            {
                string oldName = Path.GetFileName(file);
                DateTime creationTime = File.GetCreationTime(file);
                string newName = string.Format("{0:yyyy-MM-dd HH:mm:ss}_{1}", creationTime, oldName);
                Console.WriteLine(string.Format("{0}{1}{2}", oldName, " ---> ", newName));
            }
            Console.WriteLine("Rename? (Y/N)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                return;
            }
            foreach (string file in files)
            {
                string oldName = Path.GetFileName(file);
                DateTime creationTime = File.GetCreationTime(file);
                string newName = string.Format("{0:yyyy-MM-dd HH-mm-ss}_{1}", creationTime, oldName);
                File.Move(file, Path.Combine(pathToCopyFrom, newName));
                Console.WriteLine(string.Format("{0}{1}{2}{3}", oldName, " ---> ", newName, " ----> OK!"));
            }
            Console.ReadLine();
        }
    }
}
