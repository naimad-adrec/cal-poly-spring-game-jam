using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovingState : PlayerBaseState
{
    // Movement Variables
    [SerializeField] private float moveSpeed = 200f;
    private Vector3 moveDir;

    public override void EnterState(PlayerStateMachine player)
    {
        Debug.Log("I am Moving");
        ChangeCurrentAnimation(player);
    }

    public override void UpdateState(PlayerStateMachine player)
    {
        if (player.DirX != 0f && player.IsInteracting == false)
        {
            ApplyPlayerMovement(player);
            ChangeCurrentAnimation(player);
        }
        else if (player.DirX == 0f && player.IsInteracting == false)
        {
            player.SwitchState(player.IdleState);
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

    private void ChangeCurrentAnimation(PlayerStateMachine player)
    {

    }
}