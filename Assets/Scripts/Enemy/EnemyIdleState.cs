using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Idle");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.AtTarget == true && enemy.CanAttack == false)
        {
            // Wait for attack cooldown to lower

            // Play idle animation
        }
        else
        {
            enemy.SwitchState(enemy.AttackingState);
        }
    }
}
