using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    private AudioSource Audio;
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }


    public void GameQuit()
    {
        Application.Quit();
    }

    public void SoundPlay()
    {
        Audio.Play();
    }
}
