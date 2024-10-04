using System.IO;
using System.Diagnostics;
using UnityEngine;
using ONYX;

public class DriverController : MonoBehaviour
{
    [HideInInspector] public static DriverController instance;

    [Header("References")]
    [SerializeField] private Transform appButtonContainer;
    [SerializeField] private GameObject appButtonPrefab;
    [SerializeField] private App[] apps;

    private void Awake() { instance = this; }

    private void Start()
    {
        // Load apps
        foreach (App app in apps)
        {
            Instantiate(appButtonPrefab, appButtonContainer).GetComponent<AppButtonController>().SetApp(app);
        }
    }

    public void StartApp(App _app)
    {
        UnityEngine.Debug.Log($"Starting app: {_app.appName}.");

        string appNameCamelCase = new func_ToCamelCase().ToCamelCase(_app.appName);
        string appPath = Path.Combine(Application.dataPath, "..", appNameCamelCase, $"{appNameCamelCase}.x86_64");

        if (File.Exists(appPath))
            Process.Start(appPath);
        else
            UnityEngine.Debug.LogError($"Executable not found at: {appPath}");
    }
}
