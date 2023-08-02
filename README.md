# Windows Optimizer - PC Performance Boost

This C# console application, called "WindowsOptimizer," is designed to optimize your Windows system by performing several cleanup operations. It helps improve PC performance by clearing cache, deleting temporary files, cleaning the registry, and removing crash dumps. This can lead to a smoother and faster system performance.

## How to Use

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio (or any compatible C# IDE).
3. Build the solution to create the executable file.
4. Run the generated executable to start the optimization process.

## Features

### Clean Cache

The `CleanCache` method uses the built-in Windows utility `cleanmgr.exe` to automatically clean the cache, freeing up disk space and potentially enhancing system performance.

### Clean Temporary Files

The `CleanTempFiles` method deletes all temporary files located in the Internet Cache folder (`Environment.SpecialFolder.InternetCache`). This can help free up storage space and remove unnecessary files from your system.

### Clean Registry

The `CleanRegistry` method initiates the Registry Editor (`regedit.exe`) with a cleanup script (`cleanup.reg`) to remove unnecessary or invalid entries from the Windows Registry. This can help improve system stability and performance.

### Clean Crash Dumps

The `CleanCrashDumps` method removes the CrashDumps folder located in the Local Application Data directory (`Environment.SpecialFolder.LocalApplicationData`). This folder typically contains crash dump files generated when an application encounters an error. Deleting old crash dumps can save disk space.

## System Information

After the optimization process is complete, the application will display system information, including:

- Computer Name
- Operating System Version
- Current User
- System Directory

This information can be helpful in understanding your system's configuration.

## Disclaimer

Please note that this application modifies system settings and files. While it aims to enhance system performance, there is always a risk associated with making changes to the system. Make sure to back up important data and create a system restore point before running this application. The author and contributors are not responsible for any data loss or damage caused by using this tool.

---
**Note**: Before running the application, ensure you have administrative privileges as some operations may require elevated permissions to make changes to the system.