using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    #region Dependencies
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicAudioSource, sfxAudioSource;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button sfxBtn;
    [SerializeField] private Sprite muteImage,unmuteImage, musicOffImage,musicOnImage;
    #endregion
    #region MonoBehaviour
   
    private void OnEnable()
    {
        musicBtn.onClick.AddListener(ToggleMusic);
        sfxBtn.onClick.AddListener(ToggleSFx);
    }
    private void Start()
    {
        PlayMusic("BGMusic");
    }
    #endregion
    #region Functionality methods
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);
        if (sound != null)
        {
            musicAudioSource.clip = sound.clip;
            musicAudioSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);
        if (sound != null)
        {
            sfxAudioSource.PlayOneShot(sound.clip);
        }
    }
    public void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
        musicBtn.GetComponent<Image>().sprite = musicAudioSource.mute ? muteImage : unmuteImage;
    }
    public void ToggleSFx()
    {
        sfxAudioSource.mute = !sfxAudioSource.mute;
        sfxBtn.GetComponent<Image>().sprite = sfxAudioSource.mute ? musicOffImage : musicOnImage;
    }
    public void MusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
        
    }
    public void SFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
    }
    #endregion
}
