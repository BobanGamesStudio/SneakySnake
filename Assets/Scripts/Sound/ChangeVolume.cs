using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer musicMixer;

    public void SetMusicVolume(float musicVolume){
        if(musicVolume > 0){
            musicMixer.SetFloat("MusicVolume", musicVolume/(5/3.0f) - 40);
        }
        else{
            musicMixer.SetFloat("MusicVolume", -80);
        }
    }

    public void SetVoiceVolume(float voiceVolume){
        if(voiceVolume > 0){
            musicMixer.SetFloat("VoiceVolume", voiceVolume/(5/3.0f) - 40);
        }
        else{
            musicMixer.SetFloat("VoiceVolume", -80);
        }
    }
}
