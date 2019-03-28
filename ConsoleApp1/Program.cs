using System;
using System.IO;
using System.Threading;
//TODO: таймер, потоки

namespace Test
{
    class FileMover
    {
        string sourcePath= @"C:\", destinationPath= @"D:\";
        int interval=0;
        bool delete=false, showNames=false,copySubDir=false;
        long size=0;

        static void Main(string[] args)
        {
            FileMover fm1 = new FileMover();//создаём копир
            
            
            Console.WriteLine("Добро пожаловать!\nДля копирования файлов введите путь" +
                " к исходному и конечному каталогу и выберите необходимые параметры.");
            Console.WriteLine("Путь к исходной папке: ");
            fm1.sourcePath = string.Copy(Console.ReadLine());
            Console.WriteLine("Путь к конечной папке: ");
            fm1.destinationPath = string.Copy(Console.ReadLine());
            Console.WriteLine("Введите интервал в секундах:");            
            fm1.interval = int.Parse(Console.ReadLine());
            Console.WriteLine("Если нужно выводить имена файлов при копировнии, введите 1, иначе 0");
            if (int.Parse(Console.ReadLine()) == 1) fm1.showNames = true;
            else fm1.showNames = false;
            Console.WriteLine("Если нужно удалять файлы из исходной папки, после копирования, введите 1, иначе 0");
            if (int.Parse(Console.ReadLine()) == 1) fm1.delete = true;
            else fm1.delete = false;
            Console.WriteLine("Если нужно копировать вложеные папки и файлы в них, после копирования, введите 1, иначе 0");
            if (int.Parse(Console.ReadLine()) == 1) fm1.copySubDir = true;
            else fm1.copySubDir = false;
            


            DirectoryInfo diSource = new DirectoryInfo(fm1.sourcePath);
            DirectoryInfo diTarget = new DirectoryInfo(fm1.destinationPath);


            
            
            Console.WriteLine("Готово. скопировано " + Copy(diSource, diTarget, fm1.interval, 0,fm1.delete, fm1.showNames,fm1.copySubDir)/1024 + " кб");
            Console.ReadKey();
        }
        public static long Copy(DirectoryInfo source, DirectoryInfo target, int interval, long volume,bool delete, bool showNames, bool copySubDir)
        {
            

            Console.WriteLine("скопировано" + volume);
            if (source.FullName.ToLower() == target.FullName.ToLower())//проверяем папки
            {
                Console.WriteLine("Начальная и конечная папки совпадают");
                return volume;
            }

            
            if (Directory.Exists(target.FullName) == false) //если конечной папки нет, создаем
            {
                Directory.CreateDirectory(target.FullName);
            }

            foreach (FileInfo fi in source.GetFiles())//копируем файлы
            {
                if(showNames)
                    Console.WriteLine(@"Копирую {0}\{1}", source.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                volume+=fi.Length;
                if (delete)
                    fi.Delete();
                //Console.WriteLine("Скопировано " + count + " файлов.");
                Thread.Sleep(interval*1000);
                Console.WriteLine("Нажмите \"Enter\" для продолжения или введите \"Stop\" для завершения.");
                if (Console.ReadLine() == "Stop")
                    return volume;
            }


            if (copySubDir) foreach (DirectoryInfo diSourceSubDir in source.GetDirectories()) //копируем вложенные папки
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                Copy(diSourceSubDir, nextTargetSubDir, interval, volume,delete,showNames,copySubDir);
                    if(delete) diSourceSubDir.Delete();
            }
            return volume;
                       
            
        }

       
    }
}

