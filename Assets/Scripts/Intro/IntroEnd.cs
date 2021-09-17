using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

 
public class IntroEnd : MonoBehaviour {

    private VideoPlayer vid;

    private void Awake() {
        SettingsData data = SettingsSaveSystem.LoadSettingsData();
        Screen.SetResolution(data.resolutionWidth, data.resolutionHeight, Screen.fullScreen);
    }
    
    void Start(){
        vid = gameObject.GetComponent<VideoPlayer>();
        vid.loopPointReached += CheckOver;
    }
    
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}