using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemy;

    private void Start()
    {
        StartCoroutine(WaitForSpawn());
    }

    private void Update()
    {
    }

    private IEnumerator WaitForSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}