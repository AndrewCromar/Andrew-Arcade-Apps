using UnityEngine;
using System.IO;
using System.Diagnostics;

namespace ONYX
{
    public static class AndrewArcadeTools
    {
        public static void ReturnToDriver()
        {
            StartApp("Driver");
        }

        public static void StartApp(string _appName)
        {
            UnityEngine.Debug.Log($"-------------------------\nStarting app: {_appName}.");

            string appNameCamelCase = Function_ToCamelCase.ToCamelCase(_appName);
            string appPath = Path.Combine(Application.dataPath, "..", "..", appNameCamelCase, $"{appNameCamelCase}.x86_64");

            if (File.Exists(appPath))
            {
                string box64Path = "box64";
                string arguments = appPath;

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = box64Path,
                    Arguments = arguments,
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
            else
            {
                UnityEngine.Debug.LogError($"Executable not found at: {appPath}");
            }
            UnityEngine.Debug.Log("App startup process finished.");
        }
    }
}
