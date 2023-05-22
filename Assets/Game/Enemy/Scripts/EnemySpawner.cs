using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies = 3; // ���������� ������ ��� ������
    public float spawnRadius = 10f; // ������ ������ �� ������

    private Transform player; // ������ �� ������

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = player.position + randomOffset;
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
