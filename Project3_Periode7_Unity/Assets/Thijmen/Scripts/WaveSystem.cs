using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Text waveText;
    [SerializeField] private Text enemycountText;
    public List<GameObject> enemiesAlive;
    public static WaveSystem Instance { get; private set; }

    private float spawnInterval = 0.25f;
    private int amountOfEnemies = 10;
    private int currentWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (enemiesAlive.Count == 0)
        {
            StartCoroutine(SetNewWave());
        }
        waveText.text = currentWave.ToString();
        enemycountText.text = enemiesAlive.Count.ToString();
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
        SpawnWave();
        amountOfEnemies += 1;
        yield return new WaitForSeconds(5);
    }
}