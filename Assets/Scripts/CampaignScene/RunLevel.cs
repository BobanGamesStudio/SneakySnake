using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunLevel : MonoBehaviour
{
    public void RunLevelFun(int levelNum){
        FindObjectOfType<CampaignSceneManager>().PlayButtonSound();
        SceneManager.LoadScene(levelNum + 6);
    }
}
