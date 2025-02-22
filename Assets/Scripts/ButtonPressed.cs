using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [field: SerializeField] private AudioSource m_AudioSource;
    
    public void ButtonPressSound(AudioClip clip)
    {
        m_AudioSource.clip = clip;
        m_AudioSource.Play();
    }
}
