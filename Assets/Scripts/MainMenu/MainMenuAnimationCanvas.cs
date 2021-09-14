using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuAnimationCanvas : MonoBehaviour
{
    private GameObject MainMenuBackground;
    //private int i = 0;

    private int childNum = 0;

    private void Start() {
        InvokeRepeating("ShowCanvas", 9.5f, 0.2f);
    }

    private void ShowCanvas(){
        if(childNum < 4){
            transform.GetChild(childNum).gameObject.SetActive(true);
            childNum += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > 10.5f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        // We can skip animation with some actions
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0) )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
