    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PathFinding))]
    public class PathFindingEditor : Editor
    {
        public List<Cube> resultPath;

    public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.Label("Pathfinding Controls", EditorStyles.boldLabel);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            if (GUILayout.Button("Start Pathfinding"))
            {
                StartPathFinding();
            }

            GUILayout.Space(EditorGUIUtility.singleLineHeight);

            if (resultPath != null && resultPath.Count > 0)
            {
                GUILayout.Label("Path Result:", EditorStyles.boldLabel);
                foreach (var cube in resultPath)
                {
                    GUILayout.Label(cube.ToString());
                }
            }
            else
            {
                GUILayout.Label("No path found.", EditorStyles.boldLabel);
            }
        }

        private void StartPathFinding()
        {
            PathFinding pf = (PathFinding)target;

            if (pf == null)
            {
                Debug.LogError("PathFinding component is null.");
                return;
            }

            // Ensure start and end coordinates are set correctly
            resultPath = pf.FindPath(pf.startX, pf.startY, pf.endX, pf.endY);

            if (resultPath == null || resultPath.Count == 0)
            {
                Debug.LogError("Path could not be found.");
            }
            else
            {
                Debug.Log("Path found successfully.");
                
            }
        }

        void OnSceneGUI()
        {
            // Optionally, you can visualize the path in the Scene view
            if (resultPath != null && resultPath.Count > 0)
            {
                Handles.color = Color.green;
                for (int i = 0; i < resultPath.Count - 1; i++)
                {
                    Handles.DrawLine(resultPath[i].transform.position, resultPath[i + 1].transform.position);
                }
            }
        }
    }
