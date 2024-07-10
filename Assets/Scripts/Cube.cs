using UnityEditor;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool isObstacle;
    public int x;
    public int y;
    public Cube parent;
    public bool isStart;
    public bool isEnd;

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public Cube cameFromCube;

    // Initialization method
    public void Initialize(int x, int y, bool isObstacle)
    {
        this.x = x;
        this.y = y;
        this.isObstacle = isObstacle;
        this.parent = null;
        this.isStart = false;
        this.isEnd = false;
    }

    public override string ToString()
    {
        return x + "," + y;
    }

    /// <summary>
    /// Checks if the Current Gameobject is Clicked and Based on which the isObstacle bool is toggle
    /// </summary>
    private void OnMouseDown()
    {
        if (!isObstacle)
        {
            // Checks if spawnObstaclePrefab is true and intansiate Spherical Gameobject
            if (CubeGenerator.Instance.spawnObstaclePrefabs)
            {
                var newObstacle = PrefabUtility.InstantiatePrefab(CubeGenerator.Instance.circleObstacle) as GameObject;
                newObstacle.transform.SetParent(this.transform);
                newObstacle.transform.localPosition = new Vector3(0, 0.5f, 0);
                newObstacle.gameObject.name = $"Obstacle";
            }

            // toggles isObstacle to true and changes it's color to red
            isObstacle = true;
            ChangeObjectColor(Color.red);
            Debug.Log($"{this.gameObject.name} has turned into Obstacle");
        }
        else
        {
            // toggles isObstacle to false and changes it's color to white and destorys Spherical Gameobject (if exists!)
            isObstacle = false;
            ChangeObjectColor(Color.white);
            Debug.Log($"{this.gameObject.name} has turned into Normal Block");
            var g = this.transform.GetChild(0);
            Destroy(g.gameObject);
        }
    }


    /// <summary>
    /// Used in CubeGeneratorEditor to check and change values of generated grid from SOLeveldata
    /// </summary>
    public void CheckAndChangeColor()
    {
        if (!isObstacle)
        {
            ChangeObjectColor(Color.white);
            Debug.Log($"{this.gameObject.name} has turned into Normal Block");

        }
        else
        {
            ChangeObjectColor(Color.red);
            Debug.Log($"{this.gameObject.name} has turned into Obstacle");
        }
    }

    private void ChangeObjectColor(Color col)
    {
        if (isObstacle)
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = col;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = col;
        }
    }
}