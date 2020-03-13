﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour {
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private List<GameObject> enemiesAlive;

    private float spawnInterval = 0.25f;
    private int amountOfEnemies;
    private int currentWave;

    private void Update() {
        if(Input.GetKeyDown( KeyCode.F )) {
            amountOfEnemies = 10;
            SpawnWave();
        }
    }

    private void SpawnWave() {
        StartCoroutine( SpawnSingleEnemy() );
        currentWave++;
    }

    private IEnumerator SpawnSingleEnemy() {
        for(int i = 0; i < amountOfEnemies; i++) {
            GameObject enemyAddToList = Instantiate( enemy , spawnpoint.position , Quaternion.identity );
            enemiesAlive.Add( enemyAddToList );
            yield return new WaitForSeconds( spawnInterval );
        }
    }
}
