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

    private void TakeResources()
    {
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.WoodCount--;
            fireHealth += 20;

            // Play flame noise
        }
    }
}
