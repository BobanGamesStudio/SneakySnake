using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IntroManager : MonoBehaviour
{
    public GameObject videoPlayerObj;

    private void Start() {
        SettingsData data = SettingsSaveSystem.LoadSettingsData();
        
        //Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, Screen.fullScreen);

        GetComponent<ChangeVolume>().SetMusicVolume(data.musicVolume);
        GetComponent<ChangeVolume>().SetVoiceVolume(data.voiceVolume);

        var videoPlayer = videoPlayerObj.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath,"BGS_intro_voice.mp4"); 
        videoPlayer.Play();
    }
}
