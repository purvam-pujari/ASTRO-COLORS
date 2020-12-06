using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MusicP : MonoBehaviour
{
    public AudioSource levelMusic;
    public AudioSource deathMusic;

    //added 01-12
    public AudioSource coinMusic;
    public AudioSource halMusic;

    //31-10
    private int firstPlayInt;
    public Slider volumeSlider;
    private static readonly string FirstPlay="FirstPlay";
    private static readonly string MusicSliderPref="MusicSliderPref";
    private static readonly string DeathSliderPref="DeathSliderPref";


    public bool levelSong=true;
    public bool deathSong=false;
    


    //added 25-10
    private float musicVolume=1f;
    private float deathVolume=1f;

    // Start is called before the first frame update
    void Start()
    {
        //31-10
        firstPlayInt=PlayerPrefs.GetInt(FirstPlay);
        if(firstPlayInt==0)
        {
            // musicVolume=1f;
            // deathVolume=1f;
            volumeSlider.value=musicVolume;
            PlayerPrefs.SetFloat(MusicSliderPref,musicVolume);
            PlayerPrefs.SetFloat(DeathSliderPref,deathVolume);
            PlayerPrefs.SetInt(FirstPlay,-1);

            //added 01-12
            //PlayerPrefs.SetFloat(CoinSliderPref,musicVolume);
            //Debug.Log(FirstPlay);
        }
        else
        {
            musicVolume=PlayerPrefs.GetFloat(MusicSliderPref);
            deathVolume=PlayerPrefs.GetFloat(DeathSliderPref);
            volumeSlider.value=musicVolume;
            //Debug.Log("hello");
            //Console.WriteLine("welcome",FirstPlay);
        }
        //added 01-12
        coinMusic.Pause();
        halMusic.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        //added 25-10
        levelMusic.volume=musicVolume;
        deathMusic.volume=deathVolume;

        //added 01-12
        coinMusic.volume=musicVolume;
        halMusic.volume=musicVolume;
    }
    
    public void LevelSound()
    {
        levelSong=true;
        deathSong=false;
        levelMusic.Play();
    }
    public void DeathSound()
    {
        if(levelMusic.isPlaying)
            levelSong=false;
        {
            levelMusic.Stop();
        }
        if(!deathMusic.isPlaying && deathSong==false)
        {
            deathMusic.Play();
            deathSong=true;
        }
    }
    //added
    public void SoundControlPause()
    {
        levelMusic.Pause();     
    }
    public void SoundControlResume()
    {
        levelMusic.Play();     
    }

    //added 25-10
    public void SetVolume(float vol)
    {
        musicVolume=vol;
        deathVolume=vol;

        //31-10
        volumeSlider.value=musicVolume;
        PlayerPrefs.SetFloat(MusicSliderPref,musicVolume);
        PlayerPrefs.SetFloat(DeathSliderPref,deathVolume);
    }

    //31-10
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicSliderPref,volumeSlider.value);
        PlayerPrefs.SetFloat(DeathSliderPref,volumeSlider.value);   
    }
    //31-10
    private void OnApplicationFocus(bool focusStatus) {
        if(!focusStatus)
        {
            SaveSoundSettings();
        }
    }

    //added 01-12
    public void PlayCoinSound()
    {
        coinMusic.Play();
    }

    public void PlayHalSound()
    {
        halMusic.Play();
    }
}
