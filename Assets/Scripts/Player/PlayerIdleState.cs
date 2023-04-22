using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    // Movement Variables
    private float moveSpeed = 200f;
    private Vector3 moveDir;

    public override void EnterState(PlayerStateMachine player)
    {
        player.CanDodge = false;
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.DirX == 0f && player.IsInteracting == false)
        {
            ApplyPlayerMovement(player);
        }
        else if (player.DirX != 0f && player.IsInteracting == false)
        {
            player.SwitchState(player.MovingState);
        }
        else
        {
            player.SwitchState(player.InteractingState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player)
    {

    }

    private void ApplyPlayerMovement(PlayerStateMachine player)
    {
        moveDir = new Vector3(player.DirX * moveSpeed * Time.fixedDeltaTime, player.transform.position.y, player.transform.position.z);
        player.Rb.velocity = moveDir;
    }
}
