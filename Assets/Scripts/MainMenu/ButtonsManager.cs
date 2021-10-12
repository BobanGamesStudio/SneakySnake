using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public void CampaignPressed(){
        PlayButtonSound();
        SceneManager.LoadScene("Campaign");
    }

    // public void CustomPressed(){
    //     PlayButtonSound();
    //     SceneManager.LoadScene("Custom");
    // }

    public void SettingsPressed(){
        PlayButtonSound();
        SceneManager.LoadScene("Settings");
    }

    public void AboutPressed(){
        PlayButtonSound();
        SceneManager.LoadScene("About");
    }

    public void ExitPressed(){
        PlayButtonSound();
        Application.Quit();
    }

    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }
}