using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CubeGenerator))]
public class CubeGeneratorEditor : Editor
{
    int xSize;
    int ySize;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        GUILayout.Label("Buttons", EditorStyles.centeredGreyMiniLabel);
        if(GUILayout.Button("Generate Cube"))
        {
            GenerateCubes();
        }
        if (GUILayout.Button("Clear Cube"))
        {
            ClearCubes((CubeGenerator)target);
        }
        if(GUILayout.Button("Save Grid Data"))
        {
            SaveGridData((CubeGenerator)target);
        }
        if(GUILayout.Button("Generate From SO"))
        {
            GenerateCubesFromLevelData((CubeGenerator)target);
        }


        /*GUILayout.Label("Grid Button Editor", EditorStyles.boldLabel);*/

        /*for(int i=0; i<XSize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j <YSize; j++)
            {
                GUILayout.Button($"{i},{j}");
            }
            EditorGUILayout.EndHorizontal();
        }*/
    }

    /// <summary>
    /// This method generates the grid using an forloop to iterate from
    /// xSize and ySize fields from CubeGenerator 
    /// </summary>
    void GenerateCubes()
    {
        CubeGenerator cg = (CubeGenerator)target;
        ClearCubes(cg);
        xSize = cg.xSize;
        ySize = cg.ySize;
        for(int i=0; i<cg.xSize; i++)
        {
            for (int j = 0; j <cg.ySize; j++)
            {
                var newCube = PrefabUtility.InstantiatePrefab(cg.cube) as GameObject;

                // Sets the newCube as a Child to CubeGenerator GameObject
                newCube.transform.SetParent(cg.transform);

                // Sets the newCube position & name in grid 
                newCube.transform.position = new Vector3(i, 0, j);
                newCube.gameObject.name = $"Cube {i},{j}";

                // Sets isObstacle to false in the Cube component of the newCube
                var cubeComponent = newCube.GetComponent<Cube>();
                if(cubeComponent!= null)
                {
                    cubeComponent.isObstacle = false;
                    cubeComponent.x = i;
                    cubeComponent.y = j;
                }
                
            }
        }
    }
    /// <summary>
    /// Generates the grid based from the SOLevelData passed in the CubeGenerator
    /// </summary>
    /// <param name="cg"></param>
    void GenerateCubesFromLevelData(CubeGenerator cg)
    {
        // Caches the Leveldata from cubegenerator
        SOLevelData levelData = cg.leveldata;
        
        // Null Check
        if (levelData == null)
        {
            Debug.LogError("No SOLevelData assigned in CubeGenerator");
            return;
        }
        Debug.Log("Generating cubes from SOLevelData");
        Debug.Log(levelData.cells);

        //xSize = levelData.rows;
        //ySize = levelData.columns;

        // Clears any old grid if existed in CubeGenerator gameobject
        ClearCubes(cg);


        foreach (var cell in levelData.cells)
        {
            // Instantiate the newCube and sets it position and name and Child
            var newCube = PrefabUtility.InstantiatePrefab(cg.cube) as GameObject;
            newCube.transform.SetParent(cg.transform);
            newCube.transform.position = new Vector3(cell.x, 0, cell.y);
            newCube.gameObject.name = $"Cube {cell.x},{cell.y}";

            // Gets the Cube Component from the newly instantiated newCube
            // to check whether the newCube is an obstacle and accordingly sets
            // the color. and if the spawnObstaclePrefab is enabled then spawns the spherical prefab
            var cubeComponent = newCube.GetComponent<Cube>();
            if (cubeComponent != null)
            {
                cubeComponent.x = cell.x;
                cubeComponent.y = cell.y;
                cubeComponent.isObstacle = cell.isObstacle;
                //Checks if the current cell is an Obstacle
                if (cell.isObstacle)
                {
                    Debug.Log($"{newCube.name} is Obstacle");
                    // Checks to spawn a sperical prefab
                    if(cg.spawnObstaclePrefabs)
                    {
                        // Spawns and sets newCube as parent and sets its position and name
                        var newObstacle =PrefabUtility.InstantiatePrefab(cg.circleObstacle) as GameObject;
                        newObstacle.transform.position = new Vector3(cell.x,0.5f,cell.y);
                        newObstacle.gameObject.name = $"Obstacle {cell.x},{cell.y}";
                        newObstacle.transform.SetParent(newCube.transform);
                    }
                }
                //Sets the isObstacle value to the Cube component from SOLeveldata
                cubeComponent.isObstacle = cell.isObstacle;

                //Changes the color of the cube based on isObstacle Value
                cubeComponent.CheckAndChangeColor();
            }
        } }


    /// <summary>
    /// Clears the child gameobjects from CubeGenerator transform
    /// </summary>
    /// <param name="cg"></param>
    void ClearCubes(CubeGenerator cg)
    {
        for (int i = cg.transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(cg.transform.GetChild(i).gameObject);
        }
    }


    /// <summary>
    /// Saves the current Grid values into a ScriptableObject (SOLeveldata)
    /// and saves it as a asset in SavedLevelData folder in ProjectFiles
    /// </summary>
    /// <param name="cg"></param>
    void SaveGridData(CubeGenerator cg)
    {
        //Creates the instance of SOLeveldata to store current grid row, size
        SOLevelData leveldata = ScriptableObject.CreateInstance<SOLevelData>();
        leveldata.rows = cg.xSize;
        leveldata.columns = cg.ySize;
        leveldata.cells = new List<SOLevelData.GridCell>();

        // Iterates through the childs of CubeGenerator Gameobject,
        // where the instantiate grid cubes are situated
        for(int index= 0;index<cg.transform.childCount;index++)
        {
            //Gets the Child and splits it's name
            Transform child = cg.transform.GetChild(index);
            string[] nameParts = child.name.Split(' ', ',');

            // Checks the splited nameParts and retrieves it X and Y Coords
            // For eg. if | Cube 0,1 | then it's splited values are |Cube| |0| |1|
            // of which namePart[1] and NamePart[2] tells the pos of the cube which can be stored in SOLevelData
            if (nameParts.Length == 3 && int.TryParse(nameParts[1], out int i) && int.TryParse(nameParts[2], out int j))
            {
                // checks if the current i and j values exist within the Grix x and y size
                if(i<cg.xSize && j<cg.ySize)
                {
                    Cube cube = child.GetComponent<Cube>();


                    if(cube!=null)
                    {
                        // Adds the x and y pos of the cell in SOLevelData
                        leveldata.cells.Add(new SOLevelData.GridCell
                        { x = i,
                          y = j,
                          isObstacle = cube.isObstacle
                        
                        });
                    }
                }
            }
        }

        // Path where the SOAsset should store
        string path = "Assets/SavedLevelData/LevelData.asset";
        path = AssetDatabase.GenerateUniqueAssetPath(path);

        // Creates and Save SO Asset
        AssetDatabase.CreateAsset(leveldata, path);
        AssetDatabase.SaveAssets();

        Debug.Log($"Level data asset saved at {path}");
    }
}
