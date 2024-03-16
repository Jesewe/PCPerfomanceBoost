using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WindowsOptimizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "PCPerformanceBoost | Release";
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to PCPerformanceBoost!");
            Console.WriteLine("This program optimizes the performance of your computer.\n");

            await CheckForUpdatesAsync();

            await OptimizeMemoryAsync();
            await CleanCacheAsync();
            await CleanTempFilesAsync();
            await CleanRegistryAsync();
            await CleanCrashDumpsAsync();
            await ClearDNSCacheAsync();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n[*] Optimization is complete.");
            Console.ResetColor();
            Console.WriteLine("[*] Press Enter to exit.");
            Console.ReadLine();
        }

        static async Task CheckForUpdatesAsync()
        {
            string version = "1.0.0.4";
            using var client = new HttpClient();
            try
            {
                string latestVersion = await client.GetStringAsync("https://raw.githubusercontent.com/Jesewe/PCPerfomanceBoost/main/latest_version.txt");
                if (latestVersion.Trim() != version)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[*] New version available: {latestVersion} | Please update for the latest fixes and features.\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[*] You have the latest version installed.\n");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[!] Error checking for updates: " + ex.Message + "\n");
                Console.ResetColor();
            }
        }

        static async Task OptimizeMemoryAsync()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Memory optimization is complete.");
        }

        static async Task CleanCacheAsync()
        {
            Process.Start("cleanmgr.exe", "/autoclean");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[+] Cache cleanup is complete.");
        }

        static async Task CleanTempFilesAsync()
        {
            string tempFolderPath = Path.GetTempPath();
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

        static async Task CleanRegistryAsync()
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

        static async Task CleanCrashDumpsAsync()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string crashDumpsFolder = Path.Combine(localAppDataPath, "CrashDumps");

            if (Directory.Exists(crashDumpsFolder))
            {
                Directory.Delete(crashDumpsFolder, true);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[+] Clearing the CrashDumps folder is complete.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[?] CrashDumps folder not found.");
            }
        }

        static async Task ClearDNSCacheAsync()
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