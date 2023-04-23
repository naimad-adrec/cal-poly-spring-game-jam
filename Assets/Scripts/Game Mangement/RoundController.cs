using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundController : MonoBehaviour
{
    public static RoundController Instance;

    // Round Variables
    private bool roundInProgress = false;
    private int _roundCount = 0;
    private int[] _roundMaxArray = new int[7] {6, 8, 10, 16, 20, 26, 30};
    private int _currentRoundMax;
    private float roundRestTimer = 11f;

    // Round Getters and Setters
    public int RoundCount { get { return _roundCount; } private set { } }

    // Enemy Variables
    private int _liveEnemies = 0;

    // Enemy Getters and Setters
    public int LiveEnemies { get { return _liveEnemies; } set { _liveEnemies = value; }}
   

    // Spawn Point Variables
    [SerializeField] private SpawnPoint leftSpawn;
    [SerializeField] private SpawnPoint rightSpawn;

    private void Awake()
    {
        Instance = this;

        _roundCount = 0;
        _currentRoundMax = _roundMaxArray[0];
        leftSpawn.RoundMax = _currentRoundMax;
        rightSpawn.RoundMax = _currentRoundMax;
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        if (roundInProgress == false)
        {
            if (roundRestTimer > 0)
            {
                roundRestTimer -= Time.deltaTime;
            }
            else
            {
                roundInProgress = true;
                StartNewRound();
                roundRestTimer = leftSpawn.SpawnTime;
            }
        }
        else
        {
            if (LiveEnemies == 0)
            {
                roundInProgress = false;
            }
        }

    }

    private void StartNewRound()
    {
        _roundCount++;
        _currentRoundMax = _roundMaxArray[_roundCount - 1];
        leftSpawn.RoundMax = _currentRoundMax;
        rightSpawn.RoundMax = _currentRoundMax;
        if (_roundCount % 2 == 1 && leftSpawn.SpawnTime > 5)
        {
            leftSpawn.SpawnTime -= 1f;
            rightSpawn.SpawnTime -= 1f;
        }
    }
}
