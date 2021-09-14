using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBonus : MonoBehaviour
{
    [HideInInspector]
    public int applesCollected = 0;
    [HideInInspector]
    public int pillsCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(this.enabled == true)
            gameObject.GetChild("BonusPanel").SetActive(true);
        else
            gameObject.GetChild("BonusPanel").SetActive(false);
    }
}
