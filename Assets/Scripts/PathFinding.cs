using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform playerTransform;
    public SOLevelData levelData;
    public int startX, startY, endX, endY;
    private List<Cube> openList;
    private List<Cube> closedList;
    public Transform cg;
    public static event Action<List<Cube>> onPathReturn;


    private void Awake()
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData is not assigned.");
        }
    }

    public List<Cube> FindPath(int startX, int startY, int endX, int endY)
    {
        if (levelData == null)
        {
            Debug.LogError("LevelData is null in FindPath.");
            return null;
        }

        if (openList == null) openList = new List<Cube>();
        if (closedList == null) closedList = new List<Cube>();

        openList.Clear();
        closedList.Clear();

        Cube startCube = GetCube(startX, startY);
        Cube endCube = GetCube(endX, endY);

        if (startCube == null || endCube == null)
        {
            Debug.LogError("Start or End Cube is null.");
            return null;
        }

        openList.Add(startCube);

        while (openList.Count > 0)
        {
            Cube currentCube = GetLowestFCostCube(openList);

            if (currentCube == endCube)
            {
                List<Cube> path = CalculatePath(endCube);
                onPathReturn?.Invoke(path);
                return path;
            }

            openList.Remove(currentCube);
            closedList.Add(currentCube);

            foreach (Cube neighbor in GetNeighbors(currentCube))
            {
                if (closedList.Contains(neighbor) || neighbor.isObstacle)
                {
                    continue;
                }

                int tentativeGCost = currentCube.gCost + GetDistance(currentCube, neighbor);
                if (tentativeGCost < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = GetDistance(neighbor, endCube);
                    neighbor.cameFromCube = currentCube;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private Cube GetCube(int x, int y)
    {
        foreach (var cell in levelData.cells)
        {
            if (cell.x == x && cell.y == y)
            {
                for (int index = 0; index < cg.transform.childCount; index++)
                {
                    Transform child = cg.transform.GetChild(index);
                    if (child.name == $"Cube {x},{y}")
                    {
                        return child.GetComponent<Cube>();
                    }
                    #region OldCOde
                    /* Transform child = cg.transform.GetChild(index);
                     string[] nameParts = child.name.Split(' ', ',');

                     if (nameParts.Length == 3 && int.TryParse(nameParts[1], out int i) && int.TryParse(nameParts[2], out int j))
                     {
                         if (i == x && j == y)
                         {
                             Cube cube = child.GetComponent<Cube>();
                             if (cube != null)
                             {
                                 return cube;
                             }
                         }
                     }*/
                    #endregion
                }

                Debug.LogError($"Cube not found for coordinates ({x},{y}).");
                return null;
            }
        }
        Debug.LogError($"Grid cell not found for coordinates ({x},{y}).");
        return null;
    }

    private Cube GetLowestFCostCube(List<Cube> cubeList)
    {
        Cube lowestFCostCube = cubeList[0];
        foreach (var cube in cubeList)
        {
            if (cube.fCost < lowestFCostCube.fCost)
            {
                lowestFCostCube = cube;
            }
        }
        return lowestFCostCube;
    }

    private List<Cube> GetNeighbors(Cube cube)
    {
        List<Cube> neighbors = new List<Cube>();

        AddNeighbor(cube.x - 1, cube.y, neighbors);
        AddNeighbor(cube.x + 1, cube.y, neighbors);
        AddNeighbor(cube.x, cube.y - 1, neighbors);
        AddNeighbor(cube.x, cube.y + 1, neighbors);

        return neighbors;
    }

    private void AddNeighbor(int x, int y, List<Cube> neighbors)
    {
        if (x >= 0 && y >= 0 && x < levelData.columns && y < levelData.rows)
        {
            Cube neighbor = GetCube(x, y);
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
    }

    private int GetDistance(Cube a, Cube b)
    {
        int distanceX = Mathf.Abs(a.x - b.x);
        int distanceY = Mathf.Abs(a.y - b.y);
        return distanceX + distanceY;
    }

    private List<Cube> CalculatePath(Cube endCube)
    {
        List<Cube> path = new List<Cube>();
        Cube currentCube = endCube;
        while (currentCube != null)
        {
            path.Add(currentCube);
            currentCube = currentCube.cameFromCube;
        }
        path.Reverse();
        return path;
    }
}
