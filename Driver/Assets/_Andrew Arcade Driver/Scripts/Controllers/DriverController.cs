using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using ONYX;
using UnityEngine.EventSystems;

public class DriverController : MonoBehaviour
{
    [HideInInspector] public static DriverController instance;

    [Header("References")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Transform appButtonContainer;
    [SerializeField] private Text outputText;
    [SerializeField] private GameObject appButtonPrefab;
    [SerializeField] private App[] apps;

    private void Awake() { instance = this; }

    private void Start()
    {
        // Load apps
        bool first = true;
        foreach (App app in apps)
        {
            GameObject newAppButton = Instantiate(appButtonPrefab, appButtonContainer);
            newAppButton.GetComponent<AppButtonController>().SetApp(app);
            if(first){
                first = false;
                eventSystem.SetSelectedGameObject(newAppButton);
            }
            Output("Created app: " + app.appName);
        }
    }

    public void Output(string _text){
        outputText.text = $"{outputText.text}\n{_text}";
    }

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
