using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DiffLevel;

public class LevelDifficulty : MonoBehaviour
{   
    //[HideInInspector]
    public CampaignDataClass.DifficultyLevel difficultyLevel;
    
    ProgressData progressData;
    CampaignDataClass difLvlClassObj = new CampaignDataClass();

    private void Awake() {
        progressData = ProgressSaveSystem.LoadProgressData();
        
        difficultyLevel = difLvlClassObj.GiveDiffLvlFromStr(progressData.difficultyLevel);
    }

    
}
