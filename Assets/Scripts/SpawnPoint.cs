using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Enemy Variables
    [SerializeField] private GameObject enemy;
    private float _roundMax;
    private int spawnedEnemies = 0;
    private int _liveEnemies = 0;

    // Enemy Getters and Setters
    public float RoundMax {  get { return _roundMax; } set { _roundMax = value; } }
    public int LiveEnemies { get { return _liveEnemies; } private set {  } }

    // Timer Variables
    [SerializeField] private float _spawnTime;
    private float currentSpawnTime;

    // Timer Getters and Setters
    public float SpawnTime { get { return _spawnTime; } set { _spawnTime = value; } }

    private void Start()
    {
        currentSpawnTime = _spawnTime;
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
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies < _roundMax)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnedEnemies++;
            _liveEnemies++;
            currentSpawnTime = _spawnTime;
        }
    }
}
