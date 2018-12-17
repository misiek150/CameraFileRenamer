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
            Dictionary<string, string> renamed = new Dictionary<string, string>();
            foreach (string file in files)
            {
                string oldName = Path.GetFileName(file);
                DateTime creationTime = File.GetLastWriteTime(file);
                string newName = string.Format("{0:yyyy-MM-dd HH-mm-ss}_{1}", creationTime, oldName);
                renamed.Add(oldName, newName);
            }
            foreach (KeyValuePair<string, string> item in renamed)
            {
                Console.WriteLine(string.Format("{0}{1}{2}", item.Key, " ---> ", item.Value));
            }
            Console.WriteLine("Rename? (Y/N)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                return;
            }
            Console.WriteLine();
            foreach (KeyValuePair<string, string> item in renamed)
            {
                File.Move(Path.Combine(pathToCopyFrom, item.Key), Path.Combine(pathToCopyFrom, item.Value));
                Console.WriteLine(string.Format("{0}{1}{2}{3}", item.Key, " ---> ", item.Value, " ----> OK!"));
            }

            Console.ReadLine();
        }
    }
}
