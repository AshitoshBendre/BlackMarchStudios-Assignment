using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOLevelData : ScriptableObject
{
    [System.Serializable]
    public class GridCell
    {
        public int x;
        public int y;
        public bool isObstacle;
    }
    public int rows;
    public int columns;
    public List<GridCell> cells = new List<GridCell>();
}
