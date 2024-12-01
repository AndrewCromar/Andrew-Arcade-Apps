using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ONYX
{
    public class GridPlacer_window : EditorWindow
    {
        #region Variables
        static GridPlacer_window win;
        private List<GameObject> prefabs = new List<GameObject>();
        private GameObject selectedPrefab;
        private GameObject previewInstance; // Preview object
        private float gridSize = 5;
        private float workingYPosition = 0;
        private float rotation; // Current rotation in degrees
        private float rotationIncrements = 90; // Rotation increment value
        private Vector3 mouseSnappedPosition; // Snapped mouse position
        private bool isActive = true; // Variable to toggle active state
        private bool isBuildMode = true; // True for build mode, false for destroy mode
        #endregion

        #region Main Methods
        public static void InitWindow()
        {
            win = EditorWindow.GetWindow<GridPlacer_window>("Grid Placer Tool");
            win.minSize = new Vector2(400, 300);
            win.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("ONYX Grid Placement Tool", new GUIStyle(GUI.skin.label)
            {
                fontSize = 24,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            });

            DrawHorizontalLine();

            GUILayout.Label("Controlls", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.LabelField("Left Click: Build/Destroy.");
            EditorGUILayout.LabelField("\"R\" Key: Rotate (only when scene view is focused).");

            DrawHorizontalLine();

            // Toggle button to enable/disable the tool
            if (GUILayout.Button(isActive ? "Deactivate Tool" : "Activate Tool", new GUIStyle(GUI.skin.button)))
            {
                isActive = !isActive; // Toggle the active state
            }

            DrawHorizontalLine();

            // Disable the rest of the GUI if the tool is inactive
            if (!isActive) return;

            // Mode selection buttons
            GUILayout.Label("Mode Selection", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Build Mode", new GUIStyle(GUI.skin.button) { normal = { textColor = isBuildMode ? Color.white : Color.gray } }))
            {
                isBuildMode = true;
            }
            if (GUILayout.Button("Destroy Mode", new GUIStyle(GUI.skin.button) { normal = { textColor = !isBuildMode ? Color.white : Color.gray } }))
            {
                isBuildMode = false;
            }
            EditorGUILayout.EndHorizontal();

            DrawHorizontalLine();

            if (!isBuildMode) return;

            GUILayout.Label("Prefabs", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });

            // Add prefabs
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Drag prefabs here to add it:");
            GameObject prefabToAdd = (GameObject)EditorGUILayout.ObjectField(null, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            if (prefabToAdd != null)
            {
                // Check if the prefab is already in the list
                if (!prefabs.Contains(prefabToAdd))
                {
                    prefabs.Add(prefabToAdd);
                }
            }

            // Draw prefab names
            foreach (GameObject prefab in prefabs)
            {
                bool isSelected = selectedPrefab == prefab;
                if (GUILayout.Button(prefab.name, new GUIStyle(GUI.skin.button) { normal = { textColor = isSelected ? Color.white : Color.gray } }))
                {
                    selectedPrefab = prefab;
                    UpdatePreviewInstance(); // Update preview instance when a prefab is selected
                }
            }

            DrawHorizontalLine();

            GUILayout.Label("Settings", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Grid size:");
            gridSize = EditorGUILayout.FloatField(gridSize);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Working Y position:");
            workingYPosition = EditorGUILayout.FloatField(workingYPosition);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Rotation increments:");
            rotationIncrements = EditorGUILayout.FloatField(rotationIncrements);
            EditorGUILayout.EndHorizontal();

            DrawHorizontalLine();

            // Rotation Controls
            GUILayout.Label("Rotate", new GUIStyle(GUI.skin.label) { fontSize = 20, alignment = TextAnchor.MiddleCenter });
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Rotate Left"))
            {
                rotation -= rotationIncrements; // Rotate left
            }
            if (GUILayout.Button("Rotate Right"))
            {
                rotation += rotationIncrements; // Rotate right
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("Or press \"R\".");

            DrawHorizontalLine();

            if (win != null)
            {
                win.Repaint();
            }
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            DestroyPreviewInstance(); // Clean up the preview instance when the window is closed
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            // Skip if the tool is not active
            if (!isActive || gridSize == 0) return;

            // Update the snapped position on mouse move
            UpdateMouseSnappedPosition();

            // Handle object placement or deletion based on selected mode
            Event currentEvent = Event.current;

            // Draw gizmos only in build mode
            if (isBuildMode)
            {
                // Draw the grid lines
                DrawGridLines();

                // Draw a grid of spheres based on the snapped mouse position
                for (float x = -gridSize; x <= gridSize; x += gridSize)
                {
                    for (float z = -gridSize; z <= gridSize; z += gridSize)
                    {
                        Vector3 position = mouseSnappedPosition + new Vector3(x, 0, z);
                        Handles.color = (position == mouseSnappedPosition) ? Color.green : Color.yellow;
                        Handles.SphereHandleCap(0, position, Quaternion.identity, 0.5f, EventType.Repaint);
                    }
                }

                // Update the preview instance position and rotation
                if (previewInstance != null)
                {
                    previewInstance.transform.position = mouseSnappedPosition;
                    previewInstance.transform.rotation = Quaternion.Euler(0, rotation, 0); // Apply rotation
                }
            }
            else // If in destroy mode
            {
                DestroyPreviewInstance(); // Destroy the preview instance
            }

            // Handle object placement or deletion with a left-click
            if (currentEvent.type == EventType.MouseDown && currentEvent.button == 0) // Left-click
            {
                if (isBuildMode && selectedPrefab != null)
                {
                    GameObject prefabInstance = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
                    prefabInstance.transform.position = mouseSnappedPosition;
                    prefabInstance.transform.rotation = Quaternion.Euler(0, rotation, 0); // Apply rotation
                }
                else if (!isBuildMode) // Only allow deletion in destroy mode
                {
                    Ray mouseRay = HandleUtility.GUIPointToWorldRay(currentEvent.mousePosition);
                    if (Physics.Raycast(mouseRay, out RaycastHit hit))
                    {
                        GameObject hitObject = hit.collider.gameObject;

                        // Ignore the preview instance
                        if (hitObject != previewInstance)
                        {
                            // Check if the hit object is a prefab instance
                            if (PrefabUtility.IsPartOfPrefabInstance(hitObject))
                            {
                                // Destroy the entire prefab instance (root object)
                                GameObject prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(hitObject);
                                DestroyImmediate(prefabRoot); // Destroy the root prefab instance
                            }
                            else
                            {
                                DestroyImmediate(hitObject); // Destroy non-prefab object
                            }
                        }
                    }
                }
                currentEvent.Use(); // Mark as used
            }

            // Check for key input to rotate
            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.R)
                {
                    rotation += rotationIncrements; // Rotate right on "R" key press
                    Event.current.Use(); // Mark as used
                }
            }

            // Force a repaint of the Scene view for constant updates
            sceneView.Repaint();
        }


        private void DrawGridLines()
        {
            Handles.color = Color.black; // Set the line color to black

            // Get half of the grid size to determine the square's extents
            float halfGridSize = gridSize / 2;

            // Draw squares around the mouse snapped position
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Vector3 center = mouseSnappedPosition + new Vector3(x * gridSize, 0, z * gridSize);

                    // Define the corners of the square
                    Vector3 topLeft = center + new Vector3(-halfGridSize, 0, halfGridSize);
                    Vector3 topRight = center + new Vector3(halfGridSize, 0, halfGridSize);
                    Vector3 bottomLeft = center + new Vector3(-halfGridSize, 0, -halfGridSize);
                    Vector3 bottomRight = center + new Vector3(halfGridSize, 0, -halfGridSize);

                    // Draw the outline of the square
                    Handles.DrawLine(topLeft, topRight); // Top edge
                    Handles.DrawLine(topRight, bottomRight); // Right edge
                    Handles.DrawLine(bottomRight, bottomLeft); // Bottom edge
                    Handles.DrawLine(bottomLeft, topLeft);   // Left edge
                }
            }
        }

        private void UpdateMouseSnappedPosition()
        {
            Ray mouseRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            // Create a plane at the specified Y position
            Plane yPlane = new Plane(Vector3.up, new Vector3(0, workingYPosition, 0));

            // Calculate the intersection of the ray and the plane
            if (yPlane.Raycast(mouseRay, out float distance))
            {
                Vector3 intersectionPoint = mouseRay.GetPoint(distance);
                mouseSnappedPosition = SnapToGrid(intersectionPoint); // Return the snapped position
            }
        }

        private Vector3 SnapToGrid(Vector3 position)
        {
            // Snap the position to the nearest grid size increments
            position.x = Mathf.Round(position.x / gridSize) * gridSize;
            position.z = Mathf.Round(position.z / gridSize) * gridSize;
            position.y = workingYPosition; // Keep the Y at the working position
            return position;
        }

        void DrawHorizontalLine()
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 2); // 2 is the height of the line
            EditorGUI.DrawRect(rect, Color.gray); // Change the color as needed
        }

        void CloseWindow()
        {
            if (win)
            {
                win.Close();
            }
        }

        private void UpdatePreviewInstance()
        {
            if (selectedPrefab != null)
            {
                DestroyPreviewInstance(); // Destroy existing preview instance
                previewInstance = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
                previewInstance.hideFlags = HideFlags.HideAndDontSave; // Prevent the preview from being saved
                RemoveColliders(previewInstance); // Remove colliders from the preview instance
            }
        }

        private void RemoveColliders(GameObject obj)
        {
            // Recursively remove colliders from the GameObject and its children
            Collider[] colliders = obj.GetComponentsInChildren<Collider>(true); // Get all colliders, including inactive ones
            foreach (Collider collider in colliders)
            {
                DestroyImmediate(collider); // Destroy the collider
            }
        }

        private void DestroyPreviewInstance()
        {
            if (previewInstance != null)
            {
                DestroyImmediate(previewInstance); // Destroy the preview instance
                previewInstance = null;
            }
        }
        #endregion
    }
}
