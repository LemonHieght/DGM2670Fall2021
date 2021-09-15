using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Vector3SO : ScriptableObject
{
    public Vector3 value;

    public void Posistion(Vector3 number)
    {
        value = number;
    }

    public void SetPosistion(Transform obj)
    {
        value = obj.position;
    }

    public void SetPosistionFromValue(Transform obj)
    {
        obj.position = value;
    }
}
