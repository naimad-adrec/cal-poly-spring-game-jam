using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractingState : PlayerBaseState
{
    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("I am Interacting");
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.IsInteracting == true)
        {

        }
        else
        {
            player.SwitchState(player.IdleState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine player)
    {

    }
}
