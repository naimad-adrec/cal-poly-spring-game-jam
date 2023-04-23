using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]  private Canvas inGameCanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas upgradeCanvas;

    private void Awake()
    {
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
        upgradeCanvas.enabled = false;
    }

    private void Update()
    {
        if (RoundController.Instance.RoundInProgress == false && RoundController.Instance.RoundCount > 0)
        {
            upgradeCanvas.enabled = true;
        }
        else
        {
            upgradeCanvas.enabled = false;
        }
    }

    public void SwitchCanvas()
    {
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
    }
}
