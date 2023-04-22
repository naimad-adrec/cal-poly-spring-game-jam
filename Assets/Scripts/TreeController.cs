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
            StartCoroutine(DropWood());
        }
        else
        {
            Debug.Log("Taken Damage");
            treeHealth--;
            PlayerStateMachine.Instance.IsInteracting = false;
        }
    }

    private IEnumerator DropWood()
    {
        Instantiate(wood, transform.position, transform.rotation);
        Instantiate(wood, transform.position, transform.rotation);
        Instantiate(wood, transform.position, transform.rotation);

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
