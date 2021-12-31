using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderEnableDisable : MonoBehaviour
{
    public void Collideroff()
    
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    
    public void ColliderOn()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
