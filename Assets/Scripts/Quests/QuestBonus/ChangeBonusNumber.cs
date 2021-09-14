using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBonusNumber : MonoBehaviour
{
    public Text applesCollected;

    // Update is called once per frame
    void Update()
    {
        applesCollected.text = FindObjectOfType<QuestBonus>().applesCollected.ToString();
    }
}
