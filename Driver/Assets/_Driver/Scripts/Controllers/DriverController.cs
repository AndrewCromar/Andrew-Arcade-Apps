using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ONYX;

public class DriverController : MonoBehaviour
{
    [HideInInspector] public static DriverController instance;

    [Header("References")]
    [SerializeField] private GameObject appPrefab;
    [SerializeField] private Transform appsContainer;

    [Header("Settings")]
    [SerializeField] private float positionSmoothing = 10;
    [SerializeField] private float centerWidth = 2;
    [SerializeField] private float positionSpacing = 1.5f;
    [Space]
    [SerializeField] private float rotationSmoothing = 10;
    [SerializeField] private float rotationMultiplier = 15;
    [Space]
    [SerializeField] private float scaleSmoothing = 10;
    [SerializeField] private float textScaleSmoothing = 20;
    [SerializeField] private float selectedScale = 1.5f;

    [Header("Debug")]
    [SerializeField] private List<Transform> apps = new List<Transform>();
    [SerializeField] private List<AppProfile> appProfiles = new List<AppProfile>();
    [Space]
    [SerializeField] private int selectedIndex = 0;

    [Header("Inputs")]
    [SerializeField] private bool positiveInputQueued;
    [SerializeField] private bool negativeInputQueued;
    [SerializeField] private bool startInputQueued;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadApps();
        GenerateApps();

        Cursor.visible = false;
    }

    private void Update()
    {
        HandleInputs();
        TransformApps();
    }

    private void HandleInputs()
    {
        int prevSelectedIndex = selectedIndex;
        if (positiveInputQueued)
        {
            selectedIndex++;
        }
        if (negativeInputQueued)
        {
            selectedIndex--;
        }
        if (startInputQueued)
        {
            AA_AppManagement.ExecuteLaunch(appProfiles[selectedIndex].launchCommand);
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, apps.Count - 1);

        if (prevSelectedIndex > selectedIndex) CameraEffect_Shake.instance.PunchRotation(new Vector3(0, 0, 5));
        else if (prevSelectedIndex < selectedIndex) CameraEffect_Shake.instance.PunchRotation(new Vector3(0, 0, -5));

        positiveInputQueued = false;
        negativeInputQueued = false;
        startInputQueued = false;
    }

    private void TransformApps()
    {
        for (int i = 0; i < apps.Count; i++)
        {
            Transform app = apps[i];

            float selectionOffset = i - selectedIndex;
            bool selected = selectionOffset == 0;

            float xPosition = selected ? 0 : centerWidth * Mathf.Clamp(selectionOffset, -1, 1) + positionSpacing * selectionOffset;
            float yPosition = 0;
            float zPosition = 0;

            app.position = Vector3.Lerp(apps[i].position, new Vector3(xPosition, yPosition, zPosition), positionSmoothing * Time.deltaTime);

            app.rotation = Quaternion.Lerp(app.rotation, Quaternion.Euler(0, Mathf.Clamp(app.position.x * rotationMultiplier, -90, 90), 0), rotationSmoothing * Time.deltaTime);

            float scale = selected ? selectedScale : 1;
            app.localScale = Vector3.Lerp(app.localScale, new Vector3(scale, scale, 0), scaleSmoothing * Time.deltaTime);

            float textScale = selected ? 1 : 0;
            Transform text = app.Find("Text").transform;
            text.localScale = Vector3.Lerp(text.localScale, new Vector3(textScale, textScale, 1), textScaleSmoothing * Time.deltaTime);
        }
    }

    private void LoadApps()
    {
        string dataPath = "Apps/apps";

        TextAsset jsonFile = Resources.Load<TextAsset>(dataPath);

        if (jsonFile != null)
        {
            AppProfileList appsList = JsonUtility.FromJson<AppProfileList>(jsonFile.text);

            if (appsList != null && appsList.apps != null)
            {
                appProfiles.AddRange(appsList.apps);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Failed to parse apps.json");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Failed to load apps.json");
        }
    }

    private void GenerateApps()
    {
        foreach (AppProfile app in appProfiles)
        {
            GameObject newApp = Instantiate(appPrefab, appsContainer);
            apps.Add(newApp.transform);

            newApp.name = "App - " + app.title;

            newApp.GetComponent<AppController>().SetupApp(app);
        }
    }

    #region Inputs
    public void PositiveInput(InputAction.CallbackContext ctx) { if (ctx.performed) positiveInputQueued = true; }
    public void NegativeInput(InputAction.CallbackContext ctx) { if (ctx.performed) negativeInputQueued = true; }
    public void StartInput(InputAction.CallbackContext ctx) { if (ctx.performed) startInputQueued = true; }
    #endregion
}