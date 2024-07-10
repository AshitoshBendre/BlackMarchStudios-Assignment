using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public List<Cube> movementPath;
    public void OnEnable()
    {
        PathFinding.onPathReturn += StartMovement;
    }

    private void StartMovement(List<Cube> list)
    {
        movementPath = list;
        StartCoroutine("Move");
    }

    public IEnumerator Move()
    {
        foreach (Cube c in movementPath)
        {
            transform.DOMove( new Vector3(c.x, 1, c.y), 0.5f);
            yield return new WaitForSeconds(0.7f);  

        }
    }
}
