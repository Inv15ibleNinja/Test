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
            fm1.sourcePath += Console.Read();
            Console.WriteLine("Путь к конечной папке: ");
            fm1.destinationPath += Console.Read();
            //Console.Clear();
            Console.WriteLine("Выберите параметры копирования: ");


            DirectoryInfo dirInfo = new DirectoryInfo(fm1.sourcePath);

            foreach (FileInfo file in dirInfo.GetFiles("*.*"))
            {
                try
                {
                    File.Copy(file.FullName, fm1.destinationPath + "\\" + file.Name, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибочка вышла" + ex.Message);
                }
            }


            //f.sourcePath="";
            /*try{
                File.Copy(f.sourcePath, f.destinationPath, true);
            }
            catch (IOException copyError){
                Console.WriteLine(copyError.Message);
            }*/

        }
    }
}

