using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class RunLevel : MonoBehaviour
{
    public void RunLevelFun(int levelNum){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();

        if(levelNum < 10){
            SceneManager.LoadScene("Level_0" + levelNum.ToString());
        }else{
            SceneManager.LoadScene("Level_" + levelNum.ToString());
        }
    }
}
