using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // State Variables
    EnemyBaseState CurrentState;
    public EnemyMovingState MovingState = new EnemyMovingState();
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyAttackingState AttackingState = new EnemyAttackingState();

    // Game Object Variables
    private Animator _anim;

    // Game Object Getters and Setters
    public Animator Anim { get { return _anim; } set { _anim = value; } }

    // Position Variables
    private bool _atTarget = false;
    [SerializeField] private GameObject _targetPosition;

    //Position Getters and Setters
    public bool AtTarget { get { return _atTarget; } set { _atTarget = value; } }
    public GameObject TargetPosition { get { return _targetPosition; } private set { } }

    // Attack Variables
    private bool _canAttack = true;

    // Attack Getters and Setters
    public bool CanAttack { get { return _canAttack; } set { _canAttack = value; } }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // Starting state for the state machine
        CurrentState = MovingState;
        // Reference to the enemys context (This exact monobehavior script)
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }
}
