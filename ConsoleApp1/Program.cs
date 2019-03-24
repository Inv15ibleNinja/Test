using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Threading;

namespace Test
{
    class FileMover
    {
        string sourcePath, destinationPath;
        double interval;

        static void Main(string[] args)
        {
            FileMover fm1 = new FileMover();//создаём копир
            fm1.sourcePath = @"C:\";//папки по дефолту
            fm1.destinationPath = @"D:\";
            Console.WriteLine("Добро пожаловать!\nДля копирования файлов введите путь к исходному и конечному каталогу и выберите необходимые параметры.");
            Console.WriteLine("Путь к исходной папке: ");
            fm1.sourcePath = string.Copy(Console.ReadLine());
            Console.WriteLine("Путь к конечной папке: ");
            fm1.destinationPath = string.Copy(Console.ReadLine());
            Console.WriteLine("Введите интервал в секундах:");
            fm1.interval = int.Parse(Console.ReadLine());
            Console.WriteLine("Копируем из "+fm1.sourcePath + " в " + fm1.destinationPath+" втечение "+fm1.interval+" секунд");

            DirectoryInfo diSource = new DirectoryInfo(fm1.sourcePath);
            DirectoryInfo diTarget = new DirectoryInfo(fm1.destinationPath);



            DateTime finish = DateTime.Now.AddSeconds(fm1.interval);
            ;
            Console.WriteLine("Готово. скопировано "+ Copy(diSource, diTarget,finish, 0)+ "файлов");
            Console.WriteLine("выход");
        }
        public static int Copy(DirectoryInfo source, DirectoryInfo target, DateTime stop, int count)
        {

   


                if (source.FullName.ToLower() == target.FullName.ToLower())//проверяем папки
                {
                    Console.WriteLine("Начальная и конечная папки совпадают");
                    return count;
                }


                if (Directory.Exists(target.FullName) == false) //если конечной папки нет, создаем
                {
                    Directory.CreateDirectory(target.FullName);
                }

                foreach (FileInfo fi in source.GetFiles())//копируем файлы
                {
                if (DateTime.Now < stop)
                {

                    Console.WriteLine(@"Копирую {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                    count++;
                    Console.WriteLine("Скопировано " + count + " файлов");
                }
                else return count;

                }

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()) //копируем вложенные папки
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    Copy(diSourceSubDir, nextTargetSubDir, stop, count);
                }
            return count;
                       
            
        }

        
    }
}

