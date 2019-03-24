using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace Test
{
    class FileMover
    {
        string sourcePath, destinationPath;

        static void Main(string[] args)
        {
            FileMover fm1 = new FileMover();
            fm1.sourcePath = @"C:\";
            fm1.destinationPath = @"D:\";
            Console.WriteLine("Добро пожаловать!\nДля копирования файлов введите путь к исходному и конечному каталогу и выберите необходимые параметры.");
            Console.WriteLine("Путь к исходной папке: ");
            fm1.sourcePath = string.Copy(Console.ReadLine());
            Console.WriteLine("Путь к конечной папке: ");
            fm1.destinationPath = string.Copy(Console.ReadLine());
            Console.WriteLine(fm1.sourcePath+"->"+fm1.destinationPath);


            

            DirectoryInfo diSource = new DirectoryInfo(fm1.sourcePath);
            DirectoryInfo diTarget = new DirectoryInfo(fm1.destinationPath);

            Copy(diSource, diTarget);
        }
        public static void Copy(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                Copy(diSourceSubDir, nextTargetSubDir);
            }
        }

        
    }
}

