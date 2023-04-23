using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]  private Canvas inGameCanvas;
    [SerializeField] private Canvas gameOverCanvas;

    private void Awake()
    {
        inGameCanvas.enabled = true;
        gameOverCanvas.enabled = false;
    }

    public void SwitchCanvas()
    {
        inGameCanvas.enabled = false;
        gameOverCanvas.enabled = true;
    }
}
