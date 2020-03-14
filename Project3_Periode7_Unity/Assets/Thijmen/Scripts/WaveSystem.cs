using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnpoint;
    public List<GameObject> enemiesAlive;
    public static WaveSystem Instance { get; private set; }

    private float spawnInterval = 0.25f;
    private int amountOfEnemies = 10;
    private int currentWave;

    private bool isAllowedToSpawn = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (enemiesAlive.Count == 0 && isAllowedToSpawn)
        {
            StartCoroutine(SetNewWave());
            isAllowedToSpawn = false;
        }
    }

    private void SpawnWave()
    {
        StartCoroutine(SpawnSingleEnemy());
        currentWave++;
    }

    private IEnumerator SpawnSingleEnemy()
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemyAddToList = Instantiate(enemy, spawnpoint.position, Quaternion.identity);
            enemiesAlive.Add(enemyAddToList);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator SetNewWave()
    {
        isAllowedToSpawn = true;
        SpawnWave();
        yield return new WaitForSeconds(5);
    }
}