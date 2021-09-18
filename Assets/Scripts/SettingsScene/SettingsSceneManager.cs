using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

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

    public int currentWidth;
    public int currentHeight;

    private void Start() {
        LoadSettings();
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(2);
        }
    }

    public void BackToMenu(){
        PlayButtonSound();
        //LoadSettings();
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

        currentWidth = resolution.width;
        currentHeight = resolution.height;
    }

    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveSettings(){
        PlayButtonSound();
        SettingsSaveSystem.SaveSettingsData(this);
    }
    
    public void SetResolutionDropdown(SettingsData data){
        //resolutions = Screen.resolutions; 
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        
        resolutionoDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = resolutions.Length-1; i >= 0; i--){
            if(resolutions[i].width > 800){
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if(!options.Contains(option)){
                    options.Add(option);
                }

                // if (resolutions[i].width == Screen.currentResolution.width &&
                //     resolutions[i].height == Screen.currentResolution.height){
                if (resolutions[i].width == data.resolutionWidth &&
                    resolutions[i].height == data.resolutionHeight){

                    currentResolutionIndex = i;

                    currentWidth = data.resolutionWidth;
                    currentHeight = data.resolutionHeight;
                }
            }
        }
        resolutionoDropdown.AddOptions(options);
        resolutionoDropdown.value = resolutions.Length - 1 - currentResolutionIndex;
        resolutionoDropdown.RefreshShownValue();
    }

    public void LoadSettings(){
        SettingsData data = SettingsSaveSystem.LoadSettingsData();

        // Load music volume
        musicSlider.GetComponent<Slider>().value = data.musicVolume;

        // Load voice volume
        voiceSlider.GetComponent<Slider>().value = data.voiceVolume;

        // Load bloom power
        bloomSlider.GetComponent<Slider>().value = data.bloomPower;

        // Load quality
        qualityDropdown.GetComponent<Dropdown>().value = data.qualityIndex;

        // Load resolution
        SetResolutionDropdown(data);


        //Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, Screen.fullScreen);
    }
}
