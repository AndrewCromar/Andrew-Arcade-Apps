using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.Networking;
using UnityEditor.PackageManager;

namespace ONYX
{
    public class ProjectSetup_window : EditorWindow
    {
        #region Variables
        static ProjectSetup_window win;

        private string gameName = "Game";
        private bool importJPC = true;
        private bool importIS = true;
        #endregion

        #region Main Methods
        public static void InitWindow()
        {
            win = EditorWindow.GetWindow<ProjectSetup_window>("Project Setup");
            win.Show();
        }

        void OnGUI()
        {
            gameName = EditorGUILayout.TextField("Game Name:", gameName);

            importJPC = EditorGUILayout.ToggleLeft("Import: Juicy Player Controller", importJPC);
            importIS = EditorGUILayout.ToggleLeft("Import: Unity Input System", importIS);

            if (GUILayout.Button("Create Project Structure", GUILayout.Height(35), GUILayout.ExpandWidth(true)))
            {
                CreateProjectFolders();
            }

            if (win != null)
            {
                win.Repaint();
            }
        }
        #endregion

        #region Custom Methods
        void CreateProjectFolders()
        {
            if (string.IsNullOrEmpty(gameName))
            {
                return;
            }

            if (gameName == "Game")
            {
                if (!EditorUtility.DisplayDialog("Project Setup Warning", "Do you really want to name your project \"Game\"?", "Yes", "No"))
                {
                    CloseWindow();
                    return;
                }
            }

            string assetPath = Application.dataPath;
            string rootPath = assetPath + "/_" + gameName;

            DirectoryInfo rootInfo = Directory.CreateDirectory(rootPath);

            if (!rootInfo.Exists)
            {
                return;
            }

            CreateSubFolders(rootPath);

            EditorSceneManager.SaveScene(EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single), rootPath + "/Scenes/Test Site.unity", true);

            if (importJPC)
            {
                ImportJuicyPlayerController();
            }

            if (importIS)
            {
                ImportInputSystem();
            }

            AssetDatabase.Refresh();

            CloseWindow();
        }



        void CreateSubFolders(string rootPath)
        {
            Directory.CreateDirectory(rootPath + "/Animating");
            Directory.CreateDirectory(rootPath + "/Fonts");
            Directory.CreateDirectory(rootPath + "/Images");
            Directory.CreateDirectory(rootPath + "/Images/Logos");
            Directory.CreateDirectory(rootPath + "/Images/Sprites");
            Directory.CreateDirectory(rootPath + "/Materials");
            Directory.CreateDirectory(rootPath + "/Objects");
            Directory.CreateDirectory(rootPath + "/Objects/Sound Effect");
            Directory.CreateDirectory(rootPath + "/Rendering");
            Directory.CreateDirectory(rootPath + "/Scenes");
            Directory.CreateDirectory(rootPath + "/Scripts");
            Directory.CreateDirectory(rootPath + "/Scripts/Controllers");
            Directory.CreateDirectory(rootPath + "/Scripts/Editor");
            Directory.CreateDirectory(rootPath + "/Scripts/Managers");
            Directory.CreateDirectory(rootPath + "/Sounds");
        }

        void CloseWindow()
        {
            if (win)
            {
                win.Close();
            }
        }

        #region Import Methods
        void ImportJuicyPlayerController()
        {
            string url = "https://github.com/AndrewCromar/Juicy-Player-Controller/releases/download/juicy-pc_v5/Juicy.Player.Controller.unitypackage";
            string localPath = Path.Combine(Application.dataPath, "ONYX Development/JuicyPlayerController.unitypackage");

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                webRequest.SendWebRequest();

                while (!webRequest.isDone) { }

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Failed to download Juicy Player Controller: " + webRequest.error);
                    return;
                }

                File.WriteAllBytes(localPath, webRequest.downloadHandler.data);

                AssetDatabase.ImportPackage(localPath, true);
            }
        }

        void ImportInputSystem()
        {
            Debug.Log("Installing Unity Input System package...");
            Client.Add("com.unity.inputsystem");  // Add the Input System package

            // Wait for completion
            while (Client.Add("com.unity.inputsystem").Status == StatusCode.InProgress)
            {
                // Optionally, add a timeout condition here
            }

            if (Client.Add("com.unity.inputsystem").Status == StatusCode.Failure)
            {
                Debug.LogError("Failed to install the Unity Input System.");
            }
            else
            {
                Debug.Log("Unity Input System package installed successfully.");
            }
        }
        #endregion
        #endregion
    }
}
