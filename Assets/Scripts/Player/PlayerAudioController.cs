using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip woodHitSound;
    [SerializeField] private AudioClip treeBreakSound;
    [SerializeField] private AudioClip woodCollectSound;
    [SerializeField] private AudioClip coalCollectSound;
    [SerializeField] private AudioClip buildSound;
    [SerializeField] private AudioClip dashSound;

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

    public void PlayCoalCollectSound()
    {
        PlayerAudioSource.PlayOneShot(coalCollectSound);
    }

    public void PlayBuildSound()
    {
        PlayerAudioSource.PlayOneShot(buildSound);
    }

    public void PlayDashSound()
    {
        PlayerAudioSource.PlayOneShot(dashSound, 0.5f);
    }

    public bool IsPlayingSound()
    {
        return PlayerAudioSource.isPlaying;
    }
}
