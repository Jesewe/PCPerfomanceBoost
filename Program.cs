using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace WindowsOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string version = "1.0.0.0";
            string title = "PCPerfomanceBooost";
            Console.Title = $"{title} {version}";
            CleanCache();
            CleanTempFiles();
            CleanRegistry();
            CleanCrashDumps();

            Console.WriteLine("Оптимизация завершена.");
            PrintSystemInformation();
            Console.ReadLine();
        }

        static void CleanCache()
        {
            try
            {
                Process.Start("cleanmgr.exe", "/autoclean");
                Console.WriteLine("Очистка кэша завершена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при очистке кэша: " + ex.Message);
            }
        }

        static void CleanTempFiles()
        {
            try
            {
                string tempFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                System.IO.DirectoryInfo tempFolder = new System.IO.DirectoryInfo(tempFolderPath);

                foreach (var file in tempFolder.GetFiles())
                {
                    file.Delete();
                }

                foreach (var folder in tempFolder.GetDirectories())
                {
                    folder.Delete(true);
                }

                Console.WriteLine("Очистка временных файлов завершена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при очистке временных файлов: " + ex.Message);
            }
        }

        static void CleanRegistry()
        {
            try
            {
                Process.Start("regedit.exe", "/s cleanup.reg");
                Console.WriteLine("Очистка реестра завершена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при очистке реестра: " + ex.Message);
            }
        }

        static void CleanCrashDumps()
        {
            try
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string crashDumpsFolder = Path.Combine(localAppDataPath, "CrashDumps");

                if (Directory.Exists(crashDumpsFolder))
                {
                    Directory.Delete(crashDumpsFolder, true);
                    Console.WriteLine("Очистка папки CrashDumps завершена.");
                }
                else
                {
                    Console.WriteLine("Папка CrashDumps не найдена.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при очистке папки CrashDumps: " + ex.Message);
            }
        }
        static void PrintSystemInformation()
        {
            Console.WriteLine("\nИнформация о системе:");
            Console.WriteLine("--------------------------------------------------------");
            string computerName = Environment.MachineName;
            string osVersion = Environment.OSVersion.VersionString;
            string currentUser = Environment.UserName;
            string systemDirectory = Environment.SystemDirectory;
            Console.WriteLine($"Имя компьютера: {computerName}");
            Console.WriteLine($"Версия ОС: {osVersion}");
            Console.WriteLine($"Текущий пользователь: {currentUser}");
            Console.WriteLine($"Системная директория: {systemDirectory}");
            Console.WriteLine("--------------------------------------------------------");
        }
    }
}
