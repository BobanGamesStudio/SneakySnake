using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameSceneManager : MonoBehaviour
{
    public Text LevelFinishedText;

    public ProgressData progressData;

    private void Start() {
        progressData = ProgressSaveSystem.LoadProgressData();


        LevelFinishedText.text = "You have finished all campaing content at " + progressData.difficultyLevel + " difficulty level";

        if(progressData.difficultyLevel != "Hard"){
            LevelFinishedText.text = LevelFinishedText.text + "\n If you think you are brave enough you can try to finish campaign at higher difficulty level (if you haven't already).";
        }else{
            LevelFinishedText.text = LevelFinishedText.text + "\n You did this at 'hard' difficulty level what is freaking impressive. But you can try to do it again with less deaths (if you haven't finish campaign not dying).";
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
