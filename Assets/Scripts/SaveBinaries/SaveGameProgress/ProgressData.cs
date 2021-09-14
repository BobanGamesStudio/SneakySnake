using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiffLevel;

[System.Serializable]
public class ProgressData{

    public string difficultyLevel = "Easy";

    public int[] levelsUnlocked = new int[3]{1, 1, 1};

    public int[] deathsNumberEasy = new int[12]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int[] deathsNumberMedium = new int[12]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public int[] deathsNumberHard = new int[12]{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
 
    public ProgressData (ProgressData campaignData){
        difficultyLevel = campaignData.difficultyLevel;

        levelsUnlocked[0] = campaignData.levelsUnlocked[0];
        levelsUnlocked[1] = campaignData.levelsUnlocked[1];
        levelsUnlocked[2] = campaignData.levelsUnlocked[2];


        for(int j = 0; j < 12; j++){
            deathsNumberEasy[j] = campaignData.deathsNumberEasy[j];
            deathsNumberMedium[j] = campaignData.deathsNumberMedium[j];
            deathsNumberHard[j] = campaignData.deathsNumberHard[j];
        }
    }
}
