using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;


public class SettingsSceneManager : MonoBehaviour
{
    [Space]

    [Header("Sliders")]
    public GameObject musicSlider;
    public GameObject voiceSlider;
    public GameObject bloomSlider;

    public GameObject volumeSettings;

    public GameObject qualityDropdown;
    public Dropdown resolutionoDropdown;

    Resolution[] resolutions;

    private void Start() {
        
        LoadSettings();

        resolutions = Screen.resolutions; 
        resolutionoDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = resolutions.Length-1; i >= 0; i--){
            if(resolutions[i].width > 800){
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if(!options.Contains(option)){
                    options.Add(option);
                }

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height){
                    
                    currentResolutionIndex = i;
                }
            }
        }

        resolutionoDropdown.AddOptions(options);
        resolutionoDropdown.value = resolutions.Length - 1 - currentResolutionIndex;
        
        resolutionoDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(2);
        }
    }

    public void BackToMenu(){
        PlayButtonSound();
        LoadSettings();
        SceneManager.LoadScene(2);
    }

    public void SetBloomPower(float bloomPower){
        volumeSettings.GetComponent<SetBloomIntensity>().SetBloom(bloomPower);
    }

    public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutions.Length - 1 - resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Set resolution " + resolution.width + "   " + resolution.height);
    }

    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveSettings(){
        SettingsSaveSystem.SaveSettingsData(this);
    }

    public void LoadSettings(){
        SettingsData data = SettingsSaveSystem.LoadSettingsData();

        // Load music volume
        musicSlider.GetComponent<Slider>().value = data.musicVolume;

        // Load voice volume
        voiceSlider.GetComponent<Slider>().value = data.voiceVolume;

        // Load bloom power
        bloomSlider.GetComponent<Slider>().value = data.bloomPower;

        qualityDropdown.GetComponent<Dropdown>().value = data.qualityIndex;
        Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, Screen.fullScreen);
    }
}
