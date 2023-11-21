/*****************************************************************************
// File Name : AudioManager.cs
// Author : Isa Luluquisin
// Creation Date : November 20, 2023
//
// Brief Description : This manages the audio sources and clips that will be played.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Sources ----------")]
    [Tooltip("Background music")]
    [SerializeField] private AudioSource musicSource;
    [Tooltip("SFX object")]
    [SerializeField] private AudioSource sfxSource;

    [Header("--------- SFX Clips ----------")]
    [Tooltip("Played when player shoots a bullet")]
    public AudioClip BulletFired;
    [Tooltip("Played when a player gets hit by a meteor")]
    public AudioClip PlayerHit;
    [Tooltip("Played when game is over")]
    public AudioClip GameOver;

    // Start is called before the first frame update
    void Start()
    {
        //plays background music when game starts
        musicSource.Play();
    }

    /// <summary>
    /// Plays the specific clip included in parameter once for every time it is called
    /// </summary>
    /// <param name="clip">name of audioclip that must be played</param>
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
