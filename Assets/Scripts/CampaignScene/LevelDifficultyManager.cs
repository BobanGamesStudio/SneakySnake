using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DiffLevel;

public class LevelDifficultyManager : MonoBehaviour
{
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private bool easyPressed = false;
    private bool mediumPressed = false;
    private bool hardPressed = false;

    public ProgressData progressData;

    public void Start() {
        progressData = ProgressSaveSystem.LoadProgressData();
        
        switch(progressData.difficultyLevel){
            case "Easy":
                easyPressed = true;
                break;
            case "Medium":
                mediumPressed = true;
                break;
            case "Hard":
                hardPressed = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(easyPressed){
            easyButton.Select();
        }

        if(mediumPressed){
            mediumButton.Select();
        }

        if(hardPressed){
            hardButton.Select();
        }
        
    }

    public void EasyPressed(){
        if(!easyPressed){
            FindObjectOfType<CampaignSceneManager>().PlayButtonSound();

            progressData = ProgressSaveSystem.LoadProgressData();
            progressData.difficultyLevel = "Easy";
            //Debug.Log(progressData.difficultyLevel);
            //ProgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Easy, progressData.levelsUnlocked);
            ProgressSaveSystem.SaveProgressData(progressData);
            easyPressed = true;
            mediumPressed = false;
            hardPressed = false;
            
            FindObjectOfType<LevelsPadlock>().PadlockLevels();
        }
    }

    public void MediumPressed(){
        if(!mediumPressed){
            FindObjectOfType<CampaignSceneManager>().PlayButtonSound();

            progressData = ProgressSaveSystem.LoadProgressData();
            progressData.difficultyLevel = "Medium";
            //ProgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Medium, progressData.levelsUnlocked);
            ProgressSaveSystem.SaveProgressData(progressData);
            easyPressed = false;
            mediumPressed = true;
            hardPressed = false;

            FindObjectOfType<LevelsPadlock>().PadlockLevels();
        }
    }

    public void HardPressed(){
        if(!hardPressed){
            FindObjectOfType<CampaignSceneManager>().PlayButtonSound();
            
            progressData = ProgressSaveSystem.LoadProgressData();
            progressData.difficultyLevel = "Hard";
            //ProgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Hard, progressData.levelsUnlocked);
            ProgressSaveSystem.SaveProgressData(progressData);
            easyPressed = false;
            mediumPressed = false;
            hardPressed = true;

            FindObjectOfType<LevelsPadlock>().PadlockLevels();
        }
    }
}
