using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ColorSO : ScriptableObject
{
    public Color value = Color.green;

    public void ChangeColor(Material material)
    {
        material.color = value;
    }
}
