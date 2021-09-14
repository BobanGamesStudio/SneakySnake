using UnityEngine.UI;
using UnityEngine;


[System.Serializable]
public class SettingsData{

    public float musicVolume = 0;
    public float voiceVolume = 0;

    public float bloomPower = 0;

    public int qualityIndex;

    public int resolutionWidth; 
    public int resolutionHeight; 
 
    public SettingsData (SettingsSceneManager settingsData){
        musicVolume = settingsData.musicSlider.GetComponent<Slider>().value;
        voiceVolume = settingsData.voiceSlider.GetComponent<Slider>().value;

        bloomPower = settingsData.bloomSlider.GetComponent<Slider>().value;

        qualityIndex = QualitySettings.GetQualityLevel();

        resolutionWidth = Screen.currentResolution.width;
        resolutionHeight = Screen.currentResolution.height;
        Debug.Log("settings data " + resolutionWidth + "   " + resolutionHeight);
    }
}
