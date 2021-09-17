using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiffLevel;

public class ResetProgress : MonoBehaviour
{
    ProgressData progressData;

    public GameObject unactivityPanel;
    public GameObject resetPanel;
    public GameObject resetCompletedPanel;

    CampaignDataClass difLvlClassObj = new CampaignDataClass();

    public bool isResetPanelOpen = false;

    public void OpenResetPanel(){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();

        isResetPanelOpen = true;

        FindObjectOfType<Canvas>().sortingLayerName = "Canvas";

        unactivityPanel.SetActive(true);
        resetPanel.SetActive(true);
    }

    public void CloseResetPanel(){
        isResetPanelOpen = false;

        FindObjectOfType<Canvas>().sortingLayerName = "Default";

        unactivityPanel.SetActive(false);
        resetPanel.SetActive(false);
    }

    public void ResetCompletedPanelOpen(){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();
        if(!resetCompletedPanel.activeSelf)
            resetCompletedPanel.SetActive(true);
    }

    public void ResetCompletedPanelClose(){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();
        resetCompletedPanel.SetActive(false);
    }

    public void ResetEasy(){
        progressData = PorgressSaveSystem.LoadProgressData();
        progressData.levelsUnlocked[0] = 1;

        progressData.deathsNumberEasy = new int[12];
        for(int j = 0; j < 12; j++){
            progressData.deathsNumberEasy[j] = 0;
        }
        DataSave();
    }

    public void ResetMedium(){
        progressData = PorgressSaveSystem.LoadProgressData();
        progressData.levelsUnlocked[1] = 1;

        progressData.deathsNumberMedium = new int[12];
        for(int j = 0; j < 12; j++){
            progressData.deathsNumberMedium[j] = 0;
        }
        DataSave();
    }

    public void ResetHard(){
        progressData = PorgressSaveSystem.LoadProgressData();
        progressData.levelsUnlocked[2] = 1;

        progressData.deathsNumberHard = new int[12];
        for(int j = 0; j < 12; j++){
            progressData.deathsNumberHard[j] = 0;
        }
        DataSave();
    }

    public void ResetAll(){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();
        
        progressData = PorgressSaveSystem.LoadProgressData();
        progressData.levelsUnlocked = new int[3]{1, 1, 1};
        
        progressData.deathsNumberEasy = new int[12];
        progressData.deathsNumberMedium = new int[12];
        progressData.deathsNumberHard = new int[12];
        for(int j = 0; j < 12; j++){
            progressData.deathsNumberEasy[j] = 0;
            progressData.deathsNumberMedium[j] = 0;
            progressData.deathsNumberHard[j] = 0;
        }
        
        DataSave();
    }

    public void DataSave(){
        Debug.Log(progressData.difficultyLevel);
        PorgressSaveSystem.SaveProgressData(progressData);
        FindObjectOfType<LevelsPadlock>().PadlockLevels();
        ResetCompletedPanelOpen();
    }
    
}
