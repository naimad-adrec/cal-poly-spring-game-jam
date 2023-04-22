using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    // Movement Variables
    private float moveSpeed = 1f;

    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Moving");
        MoveToTarget(enemy);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.AtTarget == false)
        {
            MoveToTarget(enemy);

            // Attack damage player if collision detected
        }
        else
        {
            enemy.SwitchState(enemy.IdleState);
        }
    }

    private void MoveToTarget(EnemyStateMachine enemy)
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.TargetPosition.transform.position, moveSpeed * Time.deltaTime);

        if (enemy.transform.position == enemy.TargetPosition.transform.position)
        {
            enemy.AtTarget = true;
        }
    }
}
