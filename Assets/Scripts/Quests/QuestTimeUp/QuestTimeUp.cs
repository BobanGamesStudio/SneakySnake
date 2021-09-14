using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiffLevel;

public class QuestTimeUp : MonoBehaviour
{
    [Header("Easy")]
    [Tooltip("Time that lef player for finish the level at EASY difficulty level")]
    public int minutesEasy;
    public int secondsEasy;

    [Header("Medium")]
    [Tooltip("Time that lef player for finish the level at EASY difficulty level")]
    public int minutesMedium;
    public int secondsMedium;

    [Header("Hard")]
    [Tooltip("Time that lef player for finish the level at EASY difficulty level")]
    public int minutesHard;
    public int secondsHard;

    [HideInInspector]
    public int minutes;
    [HideInInspector]
    public int seconds;

    private void Start() {
        switch(FindObjectOfType<LevelDifficulty>().difficultyLevel){
            case CampaignDataClass.DifficultyLevel.Easy:
                minutes = minutesEasy;
                seconds = secondsEasy;
                break;
            case CampaignDataClass.DifficultyLevel.Medium:
                minutes = minutesMedium;
                seconds = secondsMedium;
                break;
            case CampaignDataClass.DifficultyLevel.Hard:
                minutes = minutesHard;
                seconds = secondsHard;
                break;
        }

        if(this.enabled == true)
            gameObject.GetChild("TimeUpPanel").SetActive(true);
        else
            gameObject.GetChild("TimeUpPanel").SetActive(false);
    }
}
