using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    // Dash Variables
    private Vector3 moveDir;
    private float dodgeTime = 0.2f;
    private float currentDodgeTime;

    public override void EnterState(PlayerStateMachine player)
    {
        ChangeCurrentAnimation(player);
        FireController.Instance.FireHealth -= 5;
        if (FireController.Instance.FireHealth <= 0)
        {
            FireController.Instance.FireDies();
        }
        player.Animator.SetTrigger("Dash");

        currentDodgeTime = dodgeTime;
        player.Coll.enabled = false;
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.IsDodging == true)
        {
            ChangeCurrentAnimation(player);
            if (currentDodgeTime <= 0)
            {
                player.Coll.enabled = true;
                player.IsDodging = false;
            }
            else
            {
                currentDodgeTime -= Time.deltaTime;
                ApplyDodgeMovement(player);
            }
        }
        else if (player.DirX != 0f && player.IsDodging == false)
        {
            player.SwitchState(player.MovingState);
        }
        else
        {
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player)
    {

    }

    private void ApplyDodgeMovement(PlayerStateMachine player)
    {
        moveDir = new Vector3(player.DirX * player.DodgeSpeed * Time.fixedDeltaTime, player.transform.position.y, player.transform.position.z);
        player.Rb.velocity = moveDir;
    }

    private void ChangeCurrentAnimation(PlayerStateMachine player)
    {
        if (player.LastDirX < 0)
        {
            player.Sp.flipX = true;
        }
        else if (player.LastDirX > 0)
        {
            player.Sp.flipX = false;
        }
    }
}
