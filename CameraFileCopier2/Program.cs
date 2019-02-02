using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CameraFileCopier2
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Give your start path...");
            string pathToCopyFrom = Console.ReadLine();
            Console.WriteLine();
            List<string> files = Directory.GetFiles(pathToCopyFrom, "*", SearchOption.AllDirectories).ToList();
            string destinationDirectory = Path.Combine(pathToCopyFrom, "renamed");
            Dictionary<string, string> renamed = new Dictionary<string, string>();
            foreach (string file in files)
            {
                if (Path.GetDirectoryName(file).Contains(destinationDirectory))
                {
                    continue;
                }
                string oldName = Path.GetFileName(file);
                string extension = Path.GetExtension(file);
                bool isJpgFile = extension.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase);
                bool isMtsFile = extension.Equals(".mts", StringComparison.InvariantCultureIgnoreCase);
                if (!isJpgFile && !isMtsFile)
                {
                    continue;
                }

                DateTime timeStamp = File.GetLastWriteTime(file);
                string newName = string.Format("{0:yyyy-MM-dd HH-mm-ss}_{1}", timeStamp, oldName);
                renamed.Add(file, Path.Combine(destinationDirectory, newName));
            }
            foreach (KeyValuePair<string, string> item in renamed)
            {
                Console.WriteLine(string.Format("{0}{1}{2}", Path.GetFileName(item.Key), " ---> ", Path.GetFileName(item.Value)));
            }
            Console.WriteLine("Rename? (Y/N)");
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                return;
            }
            Console.WriteLine();
            
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
            foreach (KeyValuePair<string, string> item in renamed)
            {
                File.Move(item.Key, item.Value);
                Console.WriteLine(string.Format("{0}{1}{2}{3}", item.Key, " ---> ", item.Value, " ----> OK!"));
            }

            Console.ReadLine();
        }
    }
}
