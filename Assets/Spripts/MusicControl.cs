using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour
{
    public GameObject soundButton;
    public AudioMixer mixer;

    private bool isPlaying = true;

    public void SwitchMusic()
    {
        soundButton.GetComponent<AudioSource>().Play();
        if (isPlaying)
        {
            isPlaying = false;
            mixer.SetFloat("Volume", -80);
        }
        else
        {
            isPlaying = true;
            mixer.SetFloat("Volume", 0);
        }
    }
}