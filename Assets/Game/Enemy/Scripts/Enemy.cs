using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public float detectionRadius = 3f; // Радиус обнаружения игрока
    public Transform initialPoint; // Исходная точка врага

    private Transform player; // Ссылка на игрока
    private bool isPlayerDetected; // Флаг обнаружения игрока


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        if (distanceToPlayer <= detectionRadius)
        {
            isPlayerDetected = true;
        }
        else
        {
            isPlayerDetected = false;
        }
        if (isPlayerDetected)
        {
            MoveTowardsPlayer();
        }
        else
        {
            MoveToInitialPoint();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (GameManager.instance.player.transform.position - transform.position).normalized;
        Vector3 newPosition = transform.position + directionToPlayer * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void MoveToInitialPoint()
    {
        Vector3 directionToInitialPoint = (initialPoint.position - transform.position).normalized;
        Vector3 newPosition = transform.position + directionToInitialPoint * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            RecieveDamage();
            Destroy(collision.gameObject);
        }       
    }

    protected override void RecieveDamage()
    {
        hitpoint -= 5;
        if (hitpoint <= 0)
            Death();
    }



    protected override void Death()
    {
        Destroy(coll.gameObject);
        int randomIndex = Random.Range(0, GameManager.instance.itemsPrefab.Count);
        Instantiate(GameManager.instance.itemsPrefab[randomIndex], transform.position, Quaternion.identity); 
    }
}
