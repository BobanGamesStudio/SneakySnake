using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTime : MonoBehaviour
{
    public Text TimeLeftShow;
    public int TimeLeftInt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        TimeLeftInt = FindObjectOfType<QuestTimeUp>().seconds + 60*FindObjectOfType<QuestTimeUp>().minutes;
        TimeLeftInt -=(int)Time.timeSinceLevelLoad;

        TimeLeftShow.text = "Time left: " + (Mathf.Floor(TimeLeftInt/60)).ToString("0") + ":" + 
                        (TimeLeftInt%60).ToString("0");

        if(TimeLeftInt <= 0){
            FindObjectOfType<EndGameManagement>().EndGameTimeUp();
        }

    }
}
