using System.Collections.Generic;
using UnityEngine;

public class StepAudioController : MonoBehaviour
{
    public List<AudioClip> footstepClips;
    public AudioClip landingClip;
    public AudioSource audioSource;

    public void PlayStepAudioClip()
    {
        audioSource.clip = footstepClips[Random.Range(0, footstepClips.Count - 1)];
        audioSource.Play();
    }

    public void PlayLandingClip()
    {
        audioSource.clip = landingClip;
        audioSource.Play();
    }
}