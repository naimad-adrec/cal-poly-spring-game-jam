using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Enemy Variables
    [SerializeField] private GameObject enemy;
    private float _roundMax;
    private int spawnedEnemies = 0;

    // Enemy Getters and Setters
    public float RoundMax {  get { return _roundMax; } set { _roundMax = value; } }

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
        if (RoundController.Instance.GameInProgress == true)
        {
            if (currentSpawnTime > 0f)
            {
                currentSpawnTime -= Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemies < _roundMax)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            spawnedEnemies++;
            RoundController.Instance.LiveEnemies++;
            currentSpawnTime = _spawnTime;
        }
    }

    public void ResetSpawnedEnemies()
    {
        spawnedEnemies = 0;
    }
}
