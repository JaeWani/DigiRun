using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audio;

    public AudioClip click;
    public AudioClip playerhit;
    public AudioClip ghostsound;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void clickSoundPlay()
    {
        audio.PlayOneShot(click);
    }
    public void PlayerHitSoundPlay()
    {
        audio.PlayOneShot(playerhit);
    }

    public void GhostShoutingSoundPlay()
    {
        audio.PlayOneShot(ghostsound);
    }
}
