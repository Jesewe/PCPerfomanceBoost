using System;
using System.Diagnostics;
using System.Net;

namespace WindowsOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = "PCPerfomanceBoost";
            Console.Title = $"{title} | JeseweScience";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Добро пожаловать в PCPerfomanceBoost!");
            Console.WriteLine("Данная программа оптимизирует производительность вашего компьютера.\n");
            CheckForUpdates();

            OptimizeMemory();
            CleanCache();
            CleanTempFiles();
            CleanRegistry();
            CleanCrashDumps();
            ClearDNSCache();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n[*] Оптимизация завершена.");
            Console.ResetColor();
            Console.WriteLine("[*] Нажмите Enter для выхода.");
            Console.ReadLine();
        }

        static void CheckForUpdates()
        {
            string version = "1.0.0.2";
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

        public static void OptimizeMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Оптимизация памяти завершена.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при оптимизации памяти: " + ex.Message);
            }
        }

        static void CleanCache()
        {
            try
            {
                Process.Start("cleanmgr.exe", "/autoclean");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка кэша завершена.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке кэша: " + ex.Message);
            }
        }

        static void CleanTempFiles()
        {
            string tempFolderPath = Path.GetTempPath();

            try
            {
                DirectoryInfo tempDir = new DirectoryInfo(tempFolderPath);

                foreach (FileInfo file in tempDir.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                    }
                }

                foreach (DirectoryInfo subDir in tempDir.GetDirectories())
                {
                    try
                    {
                        subDir.Delete(true);
                    }
                    catch (Exception)
                    {
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка временных файлов завершена.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[-] Ошибка при очистке временных файлов: " + ex.Message);
            }
        }

        static void CleanRegistry()
        {
            try
            {
                Process.Start("regedit.exe", "/s cleanup.reg");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка реестра завершена.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке реестра: " + ex.Message);
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
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Ошибка при очистке папки CrashDumps: " + ex.Message);
            }
        }

        static void ClearDNSCache()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "ipconfig";
                process.StartInfo.Arguments = "/flushdns";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                process.StandardOutput.ReadToEnd();
                process.StandardError.ReadToEnd();

                process.WaitForExit();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Очистка кэша DNS завершена.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка при очистке DNS кэша: " + ex.Message);
            }
        }
    }
}
