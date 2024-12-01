using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject normalZombiePrefab;
    public GameObject eliteZombiePrefab;
    public GameObject bossZombiePrefab;

    public float spawnDelay;
    public int zombieSpawnCount;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f);

    void Start()
    {
        SetSpawnParameters();
        StartCoroutine(SpawnZombies());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnZombies()
    {
        while (!GameController.instance.isGameOver)
        {
            for (int i = 0; i < zombieSpawnCount; i++)
            {
                Vector2 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), 0);
                Instantiate(normalZombiePrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnDelay);

            SetSpawnParameters();
        }
    }

    private void SetSpawnParameters()
    {
        int currentLevel = GameController.instance.level;

        switch (currentLevel)
        {
            case 1:
                zombieSpawnCount = 2;
                spawnDelay = 5f;
                break;
            case 2:
                zombieSpawnCount = 4;
                spawnDelay = 4f;
                break;
            case 3:
                zombieSpawnCount = 8;
                spawnDelay = 6f;
                break;
            case 4:
                zombieSpawnCount = 1;
                spawnDelay = 10f;
                break;
            case 5:
                zombieSpawnCount = 2;
                spawnDelay = 8f;
                break;
            case 6:
                zombieSpawnCount = 3;
                spawnDelay = 6f;
                break;
            case 7:
                zombieSpawnCount = 1;
                spawnDelay = 10f;
                break;
            case 8:
                zombieSpawnCount = 2;
                spawnDelay = 8f;
                break;
            case 9:
                zombieSpawnCount = 3;
                spawnDelay = 6f;
                break;
            case 10:
                zombieSpawnCount = 1;
                spawnDelay = 10f;
                break;
            case 11:
                zombieSpawnCount = 2;
                spawnDelay = 8f;
                break;
            case 12:
                zombieSpawnCount = 12;
                spawnDelay = 0.1f;
                break;
            default:
                zombieSpawnCount = 0;
                spawnDelay = 0f;
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}