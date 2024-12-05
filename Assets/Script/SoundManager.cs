using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip levelBGMSourceClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = levelBGMSourceClip;

        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        
    }
}
