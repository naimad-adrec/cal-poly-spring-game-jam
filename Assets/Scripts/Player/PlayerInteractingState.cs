using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractingState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        player.Animator.SetInteger("Tool", 0);
        player.CanDodge = false;
        player.Animator.SetTrigger("Interact");
        ChangeCurrentAnimation(player);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.IsInteracting == true)
        {
            ChangeCurrentAnimation(player);
        }
        else
        {
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player)
    {

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
