using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    public static CubeGenerator Instance;
    public GameObject cube;
    public SOLevelData leveldata;
    public int xSize;
    public int ySize;
    public GameObject circleObstacle;
    public bool spawnObstaclePrefabs=false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
