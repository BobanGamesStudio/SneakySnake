using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOutlineColor : MonoBehaviour
{

    float timeLeft;
    Color targetColor;
    
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            GetComponent<Outline>().effectColor = targetColor;
        
            // start a new transition
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 2.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            GetComponent<Outline>().effectColor = Color.Lerp(GetComponent<Outline>().effectColor, targetColor, Time.deltaTime / timeLeft);
        
            // update the timer
            timeLeft -= Time.deltaTime;
        }
    }
}
