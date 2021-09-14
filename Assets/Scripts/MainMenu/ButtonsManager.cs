using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public void CampaignPressed(){
        PlayButtonSound();
        SceneManager.LoadScene(3);
    }

    public void CustomPressed(){
        PlayButtonSound();
        SceneManager.LoadScene(4);
    }

    public void SettingsPressed(){
        PlayButtonSound();
        SceneManager.LoadScene(5);
    }

    public void AboutPressed(){
        PlayButtonSound();
        SceneManager.LoadScene(6);
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