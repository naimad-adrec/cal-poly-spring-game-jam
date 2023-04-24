using System.Collections;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    // Movement Variables
    private Vector3 moveDir;

    public override void EnterState(PlayerStateMachine player)
    {
        player.CanDodge = true;
        player.Animator.SetBool("IsMoving", true);
        ChangeCurrentAnimation(player);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.DirX != 0f && player.IsInteracting == false && player.IsDodging == false)
        {
            ApplyPlayerMovement(player);
            ChangeCurrentAnimation(player);
        }
        else if (player.DirX == 0f && player.IsInteracting == false && player.IsDodging == false)
        {
            player.SwitchState(player.IdleState);
        }
        else if (player.DirX != 0f && player.IsInteracting == false && player.IsDodging == true)
        {
            player.SwitchState(player.DodgeState);
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
        moveDir = new Vector3(player.DirX * player.MoveSpeed * Time.fixedDeltaTime, player.transform.position.y, player.transform.position.z);
        player.Rb.velocity = moveDir;
    }

    private void ChangeCurrentAnimation(PlayerStateMachine player)
    {
        if(player.DirX < 0)
        {
            player.Sp.flipX = true;
        }
        else if (player.DirX > 0)
        {
            player.Sp.flipX = false;
        }
    }
}