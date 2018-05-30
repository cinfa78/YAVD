using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "NewAudioEffectPlayer", menuName = "YAVD/Audio Effect")]
public class SAudio : ASAudioEvent
{
    public AudioClip[] audioClips;
    public float pitchVariation;
    public float volumeVariation;
    public float pitchBase;
    public float volumeBase;

    public override void Play(AudioSource audioSource)
    {
        Play(audioSource, Random.Range(0, audioClips.Length));
    }

    public override void Play(AudioSource audioSource, int clipNumber = 0)
    {
        if (audioClips.Length == 0) return;
        float basePitch = audioSource.pitch;
        float baseVolume = audioSource.volume;

        audioSource.clip = audioClips[clipNumber % audioClips.Length];
        audioSource.pitch = pitchBase + Random.Range(-pitchVariation, +pitchVariation);
        audioSource.volume = volumeBase + Random.Range(-volumeVariation, +volumeVariation);

        if(audioSource.isActiveAndEnabled)
            audioSource.Play();
        /*        audioSource.pitch = basePitch;
                audioSource.volume = baseVolume;*/
    }
}
