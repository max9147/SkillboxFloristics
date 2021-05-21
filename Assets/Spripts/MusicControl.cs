using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public GameObject soundButton;

    private bool isPlaying = true;

    public void SwitchMusic()
    {
        soundButton.GetComponent<AudioSource>().Play();
        if (isPlaying)
        {
            isPlaying = false;
            GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            isPlaying = true;
            GetComponent<AudioSource>().enabled = true;
        }
    }
}