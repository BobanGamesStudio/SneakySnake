using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanelData : MonoBehaviour
{

    public int levelNum = 0;
    public Text levelNumText;
    public Text deathsNumText;
    
    public ProgressData progressData;
    
    private void Awake() {
        progressData = ProgressSaveSystem.LoadProgressData();
    }

    public void PanelUpdate()
    {
        progressData = ProgressSaveSystem.LoadProgressData();

        levelNumText.text = levelNum.ToString();

        switch(progressData.difficultyLevel){
            case "Easy":
                deathsNumText.text = progressData.deathsNumberEasy[levelNum - 1].ToString();
                break;
            case "Medium":
                deathsNumText.text = progressData.deathsNumberMedium[levelNum - 1].ToString();
                break;
            case "Hard":
                deathsNumText.text = progressData.deathsNumberHard[levelNum - 1].ToString();
                break;
        }
    }
}
