using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip levelBGMSourceClip;
    public AudioClip startPanelBGMSourceClip;
    public AudioClip loseBGMSourceClip;
    public AudioClip winBGMSourceClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    public void GamePlayBGM()
    {
        audioSource.Stop();

        audioSource.clip = levelBGMSourceClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StartPanelPlayBGM()
    {
        audioSource.Stop();

        audioSource.clip = startPanelBGMSourceClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void WinPlayBGM()
    {
        audioSource.Stop();

        audioSource.clip = winBGMSourceClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void LosePlayBGM()
    {
        audioSource.Stop();

        audioSource.clip = loseBGMSourceClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
