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
            Console.WriteLine("Welcome to PCPerfomanceBoost!");
            Console.WriteLine("This program optimizes the performance of your computer.\n");
            CheckForUpdates();

            OptimizeMemory();
            CleanCache();
            CleanTempFiles();
            CleanRegistry();
            CleanCrashDumps();
            ClearDNSCache();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n[*] Optimization is complete.");
            Console.ResetColor();
            Console.WriteLine("[*] Press Enter to exit.");
            Console.ReadLine();
        }

        static void CheckForUpdates()
        {
            string version = "1.0.0.3";
            try
            {
                using (WebClient client = new WebClient())
                {
                    string latestVersion = client.DownloadString("https://raw.githubusercontent.com/Jesewe/PCPerfomanceBoost/main/latest_version.txt");

                    if (latestVersion.Trim() != version)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"[*] New version available: {latestVersion}");
                        Console.WriteLine("[*] Please update for the latest fixes and features.\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[*] You have the latest version installed.\n");
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Error checking for updates: " + ex.Message + "\n");
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
                Console.WriteLine("[+] Memory optimization is complete.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Memory optimization error: " + ex.Message);
            }
        }

        static void CleanCache()
        {
            try
            {
                Process.Start("cleanmgr.exe", "/autoclean");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Cache cleanup is complete.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Error clearing the cache: " + ex.Message);
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
                Console.WriteLine("[+] Cleaning of temporary files is completed.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[-] Error cleaning temporary files: " + ex.Message);
            }
        }

        static void CleanRegistry()
        {
            try
            {
                Process.Start("regedit.exe", "/s cleanup.reg");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Registry cleanup is complete.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Error when clearing the registry: " + ex.Message);
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
                    Console.WriteLine("[+] Clearing the CrashDumps folder is complete.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("[?] CrashDumps folder not found.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Error when clearing the CrashDumps folder: " + ex.Message);
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
                Console.WriteLine("[+] DNS cache cleanup is complete.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error clearing DNS cache: " + ex.Message);
            }
        }
    }
}
