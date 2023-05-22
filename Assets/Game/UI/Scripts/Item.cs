using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Collider coll;
    public int amount = 1;

    protected virtual void Start()
    {
        coll = GetComponent<Collider>();
    }
}
