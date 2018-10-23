using System.Collections.Generic;
using UnityEngine;

public class VoiceAudioController : MonoBehaviour
{
    public List<AudioClip> hurtClips;
    public AudioClip deathClip;
    public AudioSource audioSource;

    private void Awake()
    {
        PlayerHealth.OnPlayerHurt += PlayDamageAudioClip;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHurt -= PlayDamageAudioClip;
    }

    private void PlayDamageAudioClip(float currentHealthPercentage)
    {
        if (currentHealthPercentage > 0)
        {
            PlayHurtAudioClip();
        }
        else
        {
            PlayDeathClip();
        }
    }

    private  void PlayHurtAudioClip()
    {
        audioSource.clip = hurtClips[Random.Range(0, hurtClips.Count - 1)];
        audioSource.Play();
    }

    private void PlayDeathClip()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
    }
}