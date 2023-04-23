using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    [SerializeField] private bool _isBuilt = false;
    private int buildProgress = 0;

    public bool IsBuilt { get { return _isBuilt; } set { _isBuilt = value; } }

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isBuilt)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        // Check if build progress is met
    }

    public void TakeResources()
    {
        if (PlayerStateMachine.Instance.WoodCount >= 1)
        {
            PlayerStateMachine.Instance.IsInteracting = false;
            PlayerStateMachine.Instance.WoodCount--;

            if (buildProgress == 2)
            {
                _isBuilt = true;
            }
            else
            {
                buildProgress++;
            }

            // Play hammer noise
            PlayerAudioController playerAudio =
                PlayerStateMachine.Instance.gameObject.transform.GetChild(0)
                .GetComponent<PlayerAudioController>();
            playerAudio.PlayBuildSound();
        }
        else
        {
            PlayerStateMachine.Instance.IsInteracting = false;

            // Show that there is no wood
        }
    }
}
