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

    private void Start()
    {
        // Starting state for the state machine
        CurrentState = IdleState;
        // Reference to the apples context (This exact monobehavior script)
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }
}
