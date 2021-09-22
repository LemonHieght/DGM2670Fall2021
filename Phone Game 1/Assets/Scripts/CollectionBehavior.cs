using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBehavior : MonoBehaviour
{
    public ColectableSO colletable;
    public ColectionSO collection;
    void Start()
    {
        EnableCollectable(!colletable.collected);
    }

    private void OnTriggerEnter(Collider other)
    {
        collection.Collect(colletable);
        EnableCollectable(false);
    }

    private void EnableCollectable(bool value)
    {
        GetComponent<MeshRenderer>().enabled = value;
        GetComponent<Collider>().enabled = value;
    }
}
