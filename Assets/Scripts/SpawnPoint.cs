using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Enemy Variables
    [SerializeField] private GameObject enemy;
    private float roundMax;
    private int currentNumOfEnemies = 0;

    // Timer Variables
    [SerializeField] private float spawnTime;
    private float currentSpawnTime;

    private void Start()
    {
        currentSpawnTime = spawnTime;
    }

    private void Update()
    {
        if (currentSpawnTime > 0f)
        {
            currentSpawnTime -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
        }
        // Timer for Spawn Countdown or round

        //
    }

    private void SpawnEnemy()
    {
        if (currentNumOfEnemies < roundMax)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            currentNumOfEnemies++;
        }
    }
}
