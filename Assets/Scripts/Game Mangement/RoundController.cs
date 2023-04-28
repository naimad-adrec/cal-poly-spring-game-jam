using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    public static RoundController Instance;

    // Game Variables
    private bool _gameInProgress = true;

    // Game Getters and Setters
    public bool GameInProgress { get { return _gameInProgress; } private set { } }

    // Round Variables
    private bool roundInProgress = false;
    private int _roundCount = 0;
    private int _finalRoundCount;
    private int _currentRoundMax;
    private float roundRestTimer = 11f;

    // Round Getters and Setters
    public int RoundCount { get { return _roundCount; } private set { } }
    public bool RoundInProgress { get { return roundInProgress; } private set { } }

    // Enemy Variables
    private int _liveEnemies = 0;

    // Enemy Getters and Setters
    public int LiveEnemies { get { return _liveEnemies; } set { _liveEnemies = value; }}

    private double tempLastLogTime = 0.0;
   

    // Spawn Point Variables
    [SerializeField] private SpawnPoint leftSpawn;
    [SerializeField] private SpawnPoint rightSpawn;

    private void Awake()
    {
        Instance = this;

        _roundCount = 0;
        _currentRoundMax = 6;
        leftSpawn.RoundMax = _currentRoundMax;
        rightSpawn.RoundMax = _currentRoundMax;
    }

    private void Update()
    {
        if (Time.timeAsDouble > tempLastLogTime + 1.0)
        {
            Debug.Log($"Game in progress: {_gameInProgress}, Round in progress: {roundInProgress}, Live enemies: {LiveEnemies}");
            tempLastLogTime = Time.timeAsDouble;
        }
        if (_gameInProgress)
        {
            if (roundInProgress == false)
            {
                if (roundRestTimer > 0)
                {
                    roundRestTimer -= Time.deltaTime;
                }
                else
                {
                    StartNewRound();
                    roundRestTimer = 11.0f;
                }
            }
            else
            {
                if (LiveEnemies <= 0)
                {
                    StartCoroutine(EndRoundWithDelay());
                }
            }
        }
    }

    private IEnumerator EndRoundWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        roundInProgress = false;
        LiveEnemies = 0;
    }

    private void StartNewRound()
    {
        Debug.Log("Starting new round");
        roundInProgress = true;
        _roundCount++;
        _currentRoundMax = _roundCount * 2 + 2;
        leftSpawn.RoundMax = _currentRoundMax;
        leftSpawn.ResetSpawnedEnemies();
        rightSpawn.RoundMax = _currentRoundMax;
        rightSpawn.ResetSpawnedEnemies();
        if (_roundCount > 1)
            spawnTrees.Instance.SpawnTrees();
        if (_roundCount % 2 == 1 && leftSpawn.SpawnTime > 2)
        {
            leftSpawn.SpawnTime -= 1f;
            rightSpawn.SpawnTime -= 1f;
        }
    }

    public void GameOver()
    {
        _gameInProgress = false;
        _finalRoundCount = _roundCount;
        FireAttacks.Instance.KillSprite();
    }
}
