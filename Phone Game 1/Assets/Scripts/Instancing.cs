using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instancing : MonoBehaviour
{
    public void OnInstance(GameObject obj)
    {
        Instantiate(obj);
    }
}
