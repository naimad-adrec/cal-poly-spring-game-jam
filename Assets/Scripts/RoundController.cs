using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public static RoundController Instance;

    // Round Variables
    private int _roundCount = 1;
    private int[] _roundMaxArray = new int[7] {6, 8, 10, 16, 20, 26, 30};
    private int _currentRoundMax;

    // Spawn Point Variables
    [SerializeField] private SpawnPoint leftSpawn;
    [SerializeField] private SpawnPoint rightSpawn;

    private void Awake()
    {
        Instance = this;

        _roundCount = 1;
        _currentRoundMax = _roundMaxArray[0];
        leftSpawn.RoundMax = _currentRoundMax;
        rightSpawn.RoundMax = _currentRoundMax;
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    private void StartNewRound()
    {
        _roundCount++;
        _currentRoundMax = _roundMaxArray[_roundCount - 1];
        leftSpawn.RoundMax = _currentRoundMax;
        rightSpawn.RoundMax = _currentRoundMax;
        if (_roundCount % 2 == 1)
        {
            leftSpawn.SpawnTime -= 1f;
            rightSpawn.SpawnTime -= 1f;
        }
    }
}
