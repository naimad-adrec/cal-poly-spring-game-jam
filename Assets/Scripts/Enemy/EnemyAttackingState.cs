using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Attacking");

        enemy.Anim.SetBool("canAttack", true);

        // Send projectile

        // Set can attack to false
        enemy.StartCoroutine(WaitForAttack(enemy));
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

    private IEnumerator WaitForAttack(EnemyStateMachine enemy)
    {
        yield return new WaitForSeconds(.4f);
        enemy.Anim.SetBool("canAttack", false);
        enemy.CanAttack = false;
    }
}
