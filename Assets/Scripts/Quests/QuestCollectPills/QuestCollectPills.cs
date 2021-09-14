using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollectPills : MonoBehaviour
{
    [Tooltip("Number of pills that left player to finish game")]
    public int pillsLeft;
    //public GameObject canvasCollect;
    [HideInInspector]
    public bool questEnded = false;

    private void Start() {
        if(pillsLeft == 0)
            pillsLeft = GameObject.FindGameObjectsWithTag("Pill").Length;

        if(pillsLeft == 0){
            Debug.LogError("No pills at the start of scene!");
        }
        if(this.enabled == true)
            gameObject.GetChild("CollectPillsPanel").SetActive(true);
        else
            gameObject.GetChild("CollectPillsPanel").SetActive(false);
    }

    private void Update() {
        //Debug.Log(pillsLeft);
        if(pillsLeft == 0){
            questEnded = true;
            if(GetComponent<QuestCollectApples>().enabled){
                if(GetComponent<QuestCollectApples>().questEnded == true)
                    FindObjectOfType<EndGameManagement>().EndGameCollectPills();
            }
            else
                FindObjectOfType<EndGameManagement>().EndGameCollectPills();
        }
    }
}
