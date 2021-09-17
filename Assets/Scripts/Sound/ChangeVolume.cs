using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer musicMixer;

    public void SetMusicVolume(float musicVolume){
        Debug.Log("SetMusicVolume");
        if(musicVolume > 0){
            Debug.Log("ABCDE1");
            musicMixer.SetFloat("MusicVolume", musicVolume/(5/3.0f) - 40);
        }
        else{
            Debug.Log("ABCDE2");
            musicMixer.SetFloat("MusicVolume", -80);
        }
    }

    public void SetVoiceVolume(float voiceVolume){
        Debug.Log("SetVoiceVolume");
        if(voiceVolume > 0){
            musicMixer.SetFloat("VoiceVolume", voiceVolume/(5/3.0f) - 40);
        }
        else{
            musicMixer.SetFloat("VoiceVolume", -80);
        }
    }
}
