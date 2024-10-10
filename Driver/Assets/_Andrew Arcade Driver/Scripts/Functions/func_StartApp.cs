using System.Collections.Generic;

namespace ONYX
{
    public class func_StartApp
    {
        
    public void StartApp(App _app)
    {
        Output($"Starting app: {_app.appName}.");

        string appNameCamelCase = new func_ToCamelCase().ToCamelCase(_app.appName);
        string appPath = Path.Combine(Application.dataPath, "..", "..", appNameCamelCase, $"{appNameCamelCase}.x86_64");
        
        if (File.Exists(appPath))
        {
            // Prepare the command to run the executable with Box64
            string box64Path = "box64"; // Make sure 'box64' is in your PATH or provide the full path to the Box64 binary
            string arguments = appPath;

            // Create a new process start info
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
                // Start the Box64 process
                Process process = Process.Start(startInfo);
                Application.Quit();
            }
            catch (System.Exception ex)
            {
                Output($"Failed to start app: {ex.Message}");
            }
        }
        else
        {
            Output($"Executable not found at: {appPath}");
        }
        Output("App start finished.");
    }
    }
}
