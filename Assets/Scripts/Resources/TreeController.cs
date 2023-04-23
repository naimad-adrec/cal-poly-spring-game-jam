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
        PlayerStateMachine.Instance.gameObject.transform.GetChild(0)
            .GetComponent<PlayerAudioController>().PlayWoodHitSound();
        if (treeHealth == 1)
        {
            Debug.Log("Dead");
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.gameObject.transform.GetChild(0)
                .GetComponent<PlayerAudioController>().PlayTreeBreakSound();
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
        if (FireController.Instance.DropRateIncreased == false)
        {
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
            Instantiate(wood, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
