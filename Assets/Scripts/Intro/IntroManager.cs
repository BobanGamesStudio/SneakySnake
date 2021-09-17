using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IntroManager : MonoBehaviour
{
    private void Start() {
        SettingsData data = SettingsSaveSystem.LoadSettingsData();
        Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, Screen.fullScreen);

        GetComponent<ChangeVolume>().SetMusicVolume(data.musicVolume);
        GetComponent<ChangeVolume>().SetVoiceVolume(data.voiceVolume);
    }
}
