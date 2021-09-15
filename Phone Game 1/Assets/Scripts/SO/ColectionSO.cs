using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColectionSO : ScriptableObject
{
    public List<ColectableSO> collection;

    public void Collect(ColectableSO obj)
    {
        collection.Add(obj);
    }
}
