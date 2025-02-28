using UnityEngine;
using System.IO;
using System.Diagnostics;

namespace ONYX
{
    public static class AA_AppManagement
    {
        public static void ExecuteLaunch(string _launchCommand)
        {
            // Get the path to the root apps folder.
            string appsFolder = Path.Combine(Application.dataPath, "..", "..");

            // Modify the launch command.
            string moddedLaunchCommand = _launchCommand.Replace("{appsdur}", appsFolder);

            // Prepare the final command.
            string command = moddedLaunchCommand + " && box64 " + appsFolder + "/driver/driver.x86_64";

            // Run the moddedLaunchCommand in the terminal (lunix).
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                Process process = Process.Start(startInfo);
                Application.Quit();
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Failed to start app: {ex.Message}");
            }
        }
    }
}
