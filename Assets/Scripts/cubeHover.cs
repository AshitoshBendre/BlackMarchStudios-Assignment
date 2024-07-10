using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable, ExecuteAlways]
public class cubeHover : MonoBehaviour
{
    bool isMoved = false;
    public static event Action<Vector3> onObjectHover;

    
    /// <summary>
    /// On Hover changes the ganeobject color and invokes the onObjectHover event
    /// </summary>
    private void OnMouseOver()
    {
        if (!isMoved)
        {
            // Moves the Current Gameobject Slightly up and changes it's material color to green
            this.transform.DOMove(this.transform.position + new Vector3(0, 1, 0), 0.5f);
            this.GetComponent<MeshRenderer>().material.color = Color.green;

            //Invoes the onObjectHover event => used in UIHandler to show current gameobject coords on Canvas
            onObjectHover?.Invoke(this.gameObject.transform.position);
            isMoved= true;
        }
    }

    /// <summary>
    /// Resets the curent gameObject to it's initial position
    /// </summary>
    private void OnMouseExit()
    {
        Vector3 targetPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        this.transform.DOMove(targetPosition, 0.5f);
        
        if (this.gameObject.GetComponent<Cube>().isObstacle)
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        isMoved = false;
    }
}
