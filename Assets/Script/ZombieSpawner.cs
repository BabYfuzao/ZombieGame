using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZombieType
{
    public GameObject prefab;
    public float spawnProbability;
}

[System.Serializable]
public class LevelZombieConfig
{
    public ZombieType[] availableZombies;
    public int zombieSpawnCount;
    public float spawnDelay;
}

public class ZombieSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public int levelNumber;

    public LevelZombieConfig[] levelConfigs;
    private LevelZombieConfig currentLevelConfig;

    public Vector2 spawnAreaSize = new Vector2(5f, 5f);

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        while (!GameController.instance.isGameOver)
        {
            currentLevelConfig = levelConfigs[GameController.instance.level - 1];

            if (GameController.instance.isBossLevel && GameController.instance.level >= levelNumber)
            {
                Vector2 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), 0);
                Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                for (int i = 0; i < currentLevelConfig.zombieSpawnCount; i++)
                {
                    Vector2 spawnPosition = transform.position + new Vector3(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2), Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2), 0);
                    GameObject zombiePrefab = GetZombiePrefab(currentLevelConfig.availableZombies);
                    Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(currentLevelConfig.spawnDelay);
        }
    }

    private GameObject GetZombiePrefab(ZombieType[] availableZombies)
    {
        float totalProbability = 0f;
        foreach (var zombie in availableZombies)
        {
            totalProbability += zombie.spawnProbability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (var zombie in availableZombies)
        {
            cumulativeProbability += zombie.spawnProbability;
            if (randomValue < cumulativeProbability)
            {
                return zombie.prefab;
            }
        }

        return null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1));
    }
}