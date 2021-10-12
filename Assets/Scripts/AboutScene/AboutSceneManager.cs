using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutSceneManager : MonoBehaviour
{
    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            BackToMenu();
        }
    }

    public void BackToMenu(){
        PlayButtonSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }
}
