using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [Header("Audios")]
    public AudioClip buttons;
    public AudioClip roullet;
    public AudioClip vento;


    AudioSource AudioSource;

    public static SoundsManager instance;

    private void Start()
    {
        instance = this;
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audio, float pitch = 1, float vol = 1)
    {
        AudioSource.clip = audio;
        AudioSource.volume = vol;
        AudioSource.pitch = pitch;
        AudioSource.PlayOneShot(audio);
    }

    public void buttonsSound()
    {
        PlaySound(buttons, Random.Range(1.2f, 1.4f));
    }

    public void winPopUp()
    {
        PlaySound(vento, Random.Range(1.2f, 1.6f), 0.5f);
    }
}
