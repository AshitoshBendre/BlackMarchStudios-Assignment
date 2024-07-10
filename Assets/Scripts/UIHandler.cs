using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI textHolder;
    private void OnEnable()
    {
        cubeHover.onObjectHover += UpdateText;
    }

    private void OnDisable()
    {
        cubeHover.onObjectHover -= UpdateText;
    }

    private void UpdateText(Vector3 vec)
    {
        textHolder.text = ($"Cube Coords : {vec.x},{vec.z}");
    }
}
