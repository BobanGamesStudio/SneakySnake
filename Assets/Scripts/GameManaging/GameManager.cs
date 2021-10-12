using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject leaveGameCanvas;

    public GameObject num3;
    public GameObject num2;
    public GameObject num1;

    private bool startGameCountingRunning = false;

    Coroutine countCoroutine;

    private void Awake() {
        countCoroutine = StartCoroutine(StartGameCounting());
    }

    IEnumerator StartGameCounting(){
        startGameCountingRunning = true;

        Time.timeScale = 0;
        num3.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        num3.SetActive(false);
        num2.SetActive(true);
        yield return new WaitForSecondsRealtime(0.8f);
        num2.SetActive(false);
        num1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        num1.SetActive(false);
        Time.timeScale = 1;

        startGameCountingRunning = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // if((SceneManager.GetActiveScene().buildIndex + 1) < 10){
        //     SceneManager.LoadScene("Level_0" + (SceneManager.GetActiveScene().buildIndex + 1));
        // }else{
        //     SceneManager.LoadScene("Level_" + (SceneManager.GetActiveScene().buildIndex + 1));
        // }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoBackToCampaignScreen(){
        PlayButtonSound();

        SceneManager.LoadScene("Campaign");
        Time.timeScale = 1;
    }

    public void closeLeaveGameCanvas(){
        PlayButtonSound();
        
        leaveGameCanvas.SetActive(false);
        Time.timeScale = 1;
        countCoroutine = StartCoroutine(StartGameCounting());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(startGameCountingRunning){
                StopCoroutine(countCoroutine);
                num3.SetActive(false);
                num2.SetActive(false);
                num1.SetActive(false);
                startGameCountingRunning = false;
            }
            leaveGameCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    public void PlayButtonSound(){
        if(FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySoundContaining("ButtonPressed", SceneManager.GetActiveScene().buildIndex);
    }
}
