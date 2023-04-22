using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private float attackCooldown = 3f;
    private float currentCooldown;

    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Idle");
        enemy.Anim.SetBool("targetReached", true);
        currentCooldown = attackCooldown;
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (currentCooldown > 0f)
        {
            Debug.Log(currentCooldown);
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            enemy.StartCoroutine(Attack(enemy));
            currentCooldown = attackCooldown;
        }
    }

    private IEnumerator Attack(EnemyStateMachine enemy)
    {
        enemy.Anim.SetBool("canAttack", true);
        yield return new WaitForSeconds(.5f);
        enemy.Anim.SetBool("canAttack", false);
    }
}
