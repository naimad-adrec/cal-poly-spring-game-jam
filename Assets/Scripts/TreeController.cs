using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private int treeHealth = 3;

    public void TakeDamage()
    {
        if (treeHealth == 1)
        {
            Debug.Log("Dead");
            PlayerStateMachine.Instance.IsInteracting = false;

            // Drop Resources

            // Destroy Itself
        }
        else
        {
            Debug.Log("Taken Damage");
            treeHealth--;
            PlayerStateMachine.Instance.IsInteracting = false;

        }
    }
}
