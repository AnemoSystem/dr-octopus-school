using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip DiskCollision;
    public AudioClip Goal;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDiskCollision()
    {
        audioSource.PlayOneShot(DiskCollision);
    }

    public void PlayGoal()
    {
        audioSource.PlayOneShot(Goal);
    }
}