using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    [field: Header("------- Sound Effects Controller -------")]
    [field: SerializeField] private AudioSource soundEffectsAudioSource;
    [field: SerializeField] private AudioSource MusicAudioSource;
    [field: SerializeField] private List<AudioClip> slapSoundEffects;
    [field: SerializeField] private List<AudioClip> grabSoundEffects;
    [field: SerializeField] private List<AudioClip> dodgeSoundEffects;
    [field: SerializeField] private List<AudioClip> clashSoundEffects;

    [field: Header("------- BGM Controller -------")]
    [field: SerializeField] private List<AudioClip> backgroundMusic;
   
    private void Start()
    {
        if(SettingsDataHandler.instance.ReturnSavedValues().soundMuted)
        {
            soundEffectsAudioSource.volume = 0f;
        }
        else
        {
            soundEffectsAudioSource.volume = 1f;
        }

        if (SettingsDataHandler.instance.ReturnSavedValues().musicMuted)
        {
            MusicAudioSource.volume = 0f;
        }
        else
        {
            MusicAudioSource.volume = 1f;
        }
        MusicAudioSource.clip = backgroundMusic[Random.Range(0, backgroundMusic.Count)];
        MusicAudioSource.Play();
    }
    public void PlaySlapSound()
    {
        soundEffectsAudioSource.clip = slapSoundEffects[Random.Range(0, slapSoundEffects.Count)];
        soundEffectsAudioSource.Play();
    }
    public void PlayDodgeSound()
    {
        soundEffectsAudioSource.clip = dodgeSoundEffects[Random.Range(0, dodgeSoundEffects.Count)];
        soundEffectsAudioSource.Play();
    }
    public void PlayGrabSound()
    {
        soundEffectsAudioSource.clip = grabSoundEffects[Random.Range(0, grabSoundEffects.Count)];
        soundEffectsAudioSource.Play();
    }

    public void PlayClashSound()
    {
        soundEffectsAudioSource.clip = clashSoundEffects[Random.Range(0, clashSoundEffects.Count)];
        soundEffectsAudioSource.Play();
    }
}
