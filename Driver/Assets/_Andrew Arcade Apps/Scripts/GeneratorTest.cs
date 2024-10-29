using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GeneratorTest : MonoBehaviour
{
    private class AppProfile
    {
        public string title;
        public string developer;
        public string icon;
    }

    private void Start()
    {
        LoadApps();
    }

    private void LoadApps()
    {
        string dataPath = "Assets/Apps";

        if (Directory.Exists(dataPath))
        {
            string[] jsonFiles = Directory.GetFiles(dataPath, "*.json");
            List<AppProfile> apps = new List<AppProfile>();

            foreach (string file in jsonFiles)
            {
                Debug.Log("Found JSON file: " + file);

                // Load and deserialize JSON content
                string jsonContent = File.ReadAllText(file);
                AppProfile app = JsonUtility.FromJson<AppProfile>(jsonContent);
                
                if (app != null)
                {
                    apps.Add(app);
                    Debug.Log($"Loaded App: {app.title}, Developer: {app.developer}, Icon Path: {app.icon}");
                }
                else
                {
                    Debug.LogWarning($"Failed to load JSON file: {file}");
                }
            }
        }
        else
        {
            Debug.LogWarning("Folder not found: " + dataPath);
        }
    }
}
