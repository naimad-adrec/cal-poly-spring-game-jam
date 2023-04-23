using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip woodHitSound;
    [SerializeField] private AudioClip treeBreakSound;
    [SerializeField] private AudioClip woodCollectSound;
    [SerializeField] private AudioClip buildSound;

    private AudioSource PlayerAudioSource { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        PlayerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayWoodHitSound()
    {
        PlayerAudioSource.PlayOneShot(woodHitSound);
    }

    public void PlayTreeBreakSound()
    {
        PlayerAudioSource.PlayOneShot(treeBreakSound);
    }

    public void PlayWoodCollectSound()
    {
        PlayerAudioSource.PlayOneShot(woodCollectSound);
    }

    public void PlayBuildSound()
    {
        PlayerAudioSource.PlayOneShot(buildSound);
    }

    public bool IsPlayingSound()
    {
        return PlayerAudioSource.isPlaying;
    }
}
