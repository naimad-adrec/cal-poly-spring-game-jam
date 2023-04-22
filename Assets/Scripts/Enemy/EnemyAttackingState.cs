using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Attacking");

        // Send projectile

        // Play attack animation

        // Set can attack to false
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.AtTarget == true && enemy.CanAttack == true)
        {

        }
        else
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }
}
