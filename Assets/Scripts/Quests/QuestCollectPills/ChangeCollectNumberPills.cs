using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCollectNumberPills : MonoBehaviour
{
    public Text collectedNumber;

    // Start is called before the first frame update
    void Update()
    {   
        collectedNumber.text = FindObjectOfType<QuestCollectPills>().pillsLeft.ToString();
    }
}
