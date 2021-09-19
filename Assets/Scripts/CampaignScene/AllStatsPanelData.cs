using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AllStatsPanelData : MonoBehaviour
{
    public Text deathsNumText;

    public ProgressData progressData;

    private int sum;

    private void Awake() {
        progressData = ProgressSaveSystem.LoadProgressData();
    }
    
    public void UpdateDifficultyLevel(){
        progressData = ProgressSaveSystem.LoadProgressData();

        sum = 0;

        switch(progressData.difficultyLevel){
            case "Easy":
                for(int i = 0; i < progressData.deathsNumberEasy.Length; i++)
                    sum += progressData.deathsNumberEasy[i];
                if(progressData.levelsUnlocked[0] > progressData.deathsNumberEasy.Length){
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 0);
                }
                else{
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 255);
                }
                break;
            case "Medium":
                for(int i = 0; i < progressData.deathsNumberMedium.Length; i++)
                    sum += progressData.deathsNumberMedium[i];
                if(progressData.levelsUnlocked[1] > progressData.deathsNumberMedium.Length){
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 0);
                }
                else{
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 255);
                }
                break;
            case "Hard":
                for(int i = 0; i < progressData.deathsNumberHard.Length; i++)
                    sum += progressData.deathsNumberHard[i];
                if(progressData.levelsUnlocked[2] > progressData.deathsNumberHard.Length){
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 0);
                }
                else{
                    deathsNumText.GetComponent<Text>().color = new Color(0, 255, 255);
                }
                break;
        }
        

        deathsNumText.text = sum.ToString(); 
    }
}
