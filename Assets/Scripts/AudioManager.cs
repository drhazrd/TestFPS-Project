using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource bgm;
    public AudioMixer aMixer;

    public AudioSource[] soundEffect;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void StopBGM()
    {
        bgm.Stop();
    }
    public void PlaySFX(int sfxNum)
    {
        soundEffect[sfxNum].Stop();
        soundEffect[sfxNum].Play();
    }
    public void StopSFX(int sfxNum)
    {

    }
    public void SetMasterVolume(float volumeLevel)
    {
        aMixer.SetFloat("MasterVol", volumeLevel);
    }
}
