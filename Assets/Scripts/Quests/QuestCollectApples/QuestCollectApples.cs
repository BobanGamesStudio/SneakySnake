using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectApples : MonoBehaviour
{
    [Tooltip("Number of apples that left player to finish game")]
    public int applesLeft;
    //public GameObject canvasCollect;
    [HideInInspector]
    public bool questEnded = false;

    private void Start() {
        applesLeft = GameObject.FindGameObjectsWithTag("Apple").Length;
        if(applesLeft == 0){
            Debug.LogError("No apples at the start of scene!");
        }
        if(this.enabled == true)
            gameObject.GetChild("CollectApplesPanel").SetActive(true);
        else
            gameObject.GetChild("CollectApplesPanel").SetActive(false);
    }

    private void Update() {
        //Debug.Log(applesLeft);
        // if(applesLeft == 0)
        //     FindObjectOfType<EndGameManagement>().EndGameCollectApples();
        if(applesLeft == 0){
            questEnded = true;
            if(GetComponent<QuestCollectPills>().enabled){
                if(GetComponent<QuestCollectPills>().questEnded == true)
                    FindObjectOfType<EndGameManagement>().EndGameCollectApples();
            }
            else
                FindObjectOfType<EndGameManagement>().EndGameCollectApples();
        }
    }
}
