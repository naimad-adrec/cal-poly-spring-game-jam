using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
    [SerializeField]  private Canvas inGameCanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas upgradeCanvas;

    private AudioSource Audio;

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

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlaySound()
    {
        Audio.Play();
    }
}
