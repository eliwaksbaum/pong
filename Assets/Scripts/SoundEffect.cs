using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    new AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D()
    {
        audio.Play();
    }

    void OnTriggerEnter2D()
    {
        audio.Play();
    }
}
