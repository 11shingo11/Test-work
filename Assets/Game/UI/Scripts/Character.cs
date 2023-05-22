using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float hitpoint = 0;
    public int maxHitpoint = 0;
    public float moveSpeed = 0f;
    protected BoxCollider2D coll;

    protected virtual void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    protected virtual void RecieveDamage()
    {
        
    }

    protected virtual void DealDamage()
    {
        
    }

    protected virtual void Death()
    {

    }
}
