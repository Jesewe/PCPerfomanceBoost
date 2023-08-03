using System;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;

namespace WindowsOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = "PCPerfomanceBoost";
            Console.Title = $"{title}";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Добро пожаловать в PCPerfomanceBoost!");
            Console.WriteLine("Эта программа оптимизирует производительность вашего компьютера.\n");
            CheckForUpdates();

            CleanCache();
            CleanTempFiles();
            CleanRegistry();
            CleanCrashDumps();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n[*] Оптимизация завершена.");
            Console.ResetColor();
            Console.WriteLine("\nНажмите Enter для выхода.");
            Console.ReadLine();
        }

        static void CheckForUpdates()
        {
            string version = "1.0.0.1";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string latestVersion = client.DownloadString("https://raw.githubusercontent.com/Jesewe/PCPerfomanceBoost/main/latest_version.txt");

                    if (latestVersion.Trim() != version)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"[*] Доступна новая версия: {latestVersion}");
                        Console.WriteLine("[*] Пожалуйста, обновитесь для получения последних исправлений и функций.\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[*] У вас установлена последняя версия.\n");
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при проверке обновлений: " + ex.Message + "\n");
                Console.ResetColor();
            }
        }
        
        static void CleanCache()
        {
            try
            {
                Process.Start("cleanmgr.exe", "/autoclean");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка кэша завершена.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке кэша: " + ex.Message);
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка временных файлов завершена.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке временных файлов: " + ex.Message);
                Console.ResetColor();
            }
        }

        static void CleanRegistry()
        {
            try
            {
                Process.Start("regedit.exe", "/s cleanup.reg");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка реестра завершена.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке реестра: " + ex.Message);
                Console.ResetColor();
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[+] Очистка папки CrashDumps завершена.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[?] Папка CrashDumps не найдена.");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке папки CrashDumps: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}
