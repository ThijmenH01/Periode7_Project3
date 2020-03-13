using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemy;
    private int wave = 1;
    private int waveAmount = 1;

    private void Start()
    {
        StartCoroutine(WaitForSpawn());
    }

    private IEnumerator WaitForSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(7);
            wave += 1;
            waveAmount++;
            for (int i = 0; i < waveAmount; i++)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }
    }
}