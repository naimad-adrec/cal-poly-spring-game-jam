using System.Collections;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovingState : PlayerBaseState
{
    // Movement Variables
    private Vector3 moveDir;

    public override void EnterState(PlayerStateMachine player)
    {
        player.CanDodge = true;
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.DirX != 0f && player.IsInteracting == false && player.IsDodging == false)
        {
            ApplyPlayerMovement(player);
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

    }
}