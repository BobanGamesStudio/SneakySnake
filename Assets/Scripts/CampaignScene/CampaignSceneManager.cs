using UnityEngine;
using UnityEngine.SceneManagement;

public class CampaignSceneManager : MonoBehaviour
{


    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(FindObjectOfType<ResetProgress>().isResetPanelOpen) // Can go back to menu only if reset panel is closed
                FindObjectOfType<ResetProgress>().CloseResetPanel();
            else
                BackToMenu();
        }
    }

    public void BackToMenu(){
        PlayButtonSound();
        SceneManager.LoadScene(2);
    }

    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }
}
