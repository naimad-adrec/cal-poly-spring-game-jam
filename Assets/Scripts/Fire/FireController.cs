using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    // Singleton
    public static FireController Instance;

    // Game Component Variables
    private Animator anim;

    // Health Variables
    [SerializeField] private int _fireHealth;

    // Health Getters and Setters
    public int FireHealth { get { return _fireHealth; } set { _fireHealth = value; } }

    private void Awake()
    {
        Instance = this;

        anim = GetComponent<Animator>();
    }

    public void TakeResources()
    {
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.WoodCount--;
            _fireHealth += 20;

            // Play flame noise
            PlayerAudioController audioController = PlayerStateMachine.Instance
                .gameObject.transform.GetChild(0).GetComponent<PlayerAudioController>();
            audioController.PlayTreeBreakSound();
            audioController.PlayFlameSound();
        }
        else
        {
            PlayerStateMachine.Instance.IsInteracting = false;

            // Show that there is no wood
        }
    }
}
