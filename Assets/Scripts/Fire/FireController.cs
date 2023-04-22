using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Game Component Variables
    private Animator anim;

    // Health Variables
    [SerializeField] private int fireHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeResources()
    {
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.WoodCount--;
            fireHealth += 20;

            // Play flame noise
        }
        else
        {
            PlayerStateMachine.Instance.IsInteracting = false;

            // Show that there is no wood
        }
    }
}
