using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    // Health Variables
    private int treeHealth = 3;

    // Death Variables
    [SerializeField] private GameObject wood;

    public void TakeDamage()
    {
        if (treeHealth == 1)
        {
            Debug.Log("Dead");
            PlayerStateMachine.Instance.IsInteracting = false;
            DropWood();
        }
        else
        {
            Debug.Log("Taken Damage");
            PlayerStateMachine.Instance.IsInteracting = false;
            treeHealth--;
        }
    }

    private void DropWood()
    {
        Instantiate(wood, transform.position, transform.rotation);
        Instantiate(wood, transform.position, transform.rotation);
        Instantiate(wood, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
