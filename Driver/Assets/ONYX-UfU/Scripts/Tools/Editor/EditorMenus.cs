using UnityEngine;
using UnityEditor;

namespace ONYX
{
    public class EditorMenus
    {
        [MenuItem("ONYX/Project/Project Setup Tool")]
        public static void InitProjectSetupTool()
        {
            ProjectSetup_window.InitWindow();
        }

        [MenuItem("ONYX/Grid Placer Tool")]
        public static void InitGridPlacerTool()
        {
            GridPlacer_window.InitWindow();
        }
    }
}