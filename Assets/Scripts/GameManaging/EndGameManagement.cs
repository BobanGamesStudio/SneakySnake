using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiffLevel;
using UnityEngine.SceneManagement;

public class EndGameManagement : MonoBehaviour
{
    [HideInInspector]
    public bool gameHasEnded = false;

    [Tooltip("Delay before game restart/new level")]
    public float RestartDelay = 0;

    private List<GameObject> snakeParts;

    public ProgressData progressData;

    private void Awake() {
        progressData = PorgressSaveSystem.LoadProgressData();
    }

    public void EndGameDeath()
    {
        snakeParts = FindObjectOfType<SnakeProperties>().snakeParts;
        //Debug.Log("KONIEC LOSE");
        
        FindObjectOfType<SnakeMovement>().CancelInvoke();
        snakeParts[0].GetComponent<Animator>().SetBool("Dead", true);// set dead on head 
        snakeParts[1].GetComponent<Animator>().SetBool("Dead", true);// set dead on neck 
        snakeParts[snakeParts.Count - 1].GetComponent<Animator>().SetBool("Dead", true);// set dead on tail
        
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            if(FindObjectOfType<QuestBonus>().enabled == true){
                EndGameBonus();
                
                return;
            }
            AddDeath();
            GetComponent<GameManager>().Invoke("Restart", RestartDelay);
        }   
    }

    public void EndGameCollectApples()
    {
        
        if(gameHasEnded == false){
            gameHasEnded = true;

            
            SaveLevelCompletion();
            GetComponent<GameManager>().Invoke("NextLevel", RestartDelay);
        }
    }

    public void EndGameCollectPills()
    {
        if(gameHasEnded == false){
            gameHasEnded = true;

            SaveLevelCompletion();
            GetComponent<GameManager>().Invoke("NextLevel", RestartDelay);
        }
    }

    public void EndGameTimeUp()
    {
        if(gameHasEnded == false){
            gameHasEnded = true;

            AddDeath();
            GetComponent<GameManager>().Invoke("Restart", RestartDelay);
        }
    }

    public void EndGameBonus()
    {
        SaveLevelCompletion();
        GetComponent<GameManager>().Invoke("NextLevel", RestartDelay);
    }

    public void AddDeath(){
        switch(progressData.difficultyLevel){
            case "Easy":
                if(progressData.levelsUnlocked[0] == SceneManager.GetActiveScene().buildIndex - 6){//Check if that's the highest lvl unlocked
                    progressData.deathsNumberEasy[SceneManager.GetActiveScene().buildIndex - 7] += 1;
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
            case "Medium":
                if(progressData.levelsUnlocked[1] == SceneManager.GetActiveScene().buildIndex - 6){//Check if that's the highest lvl unlocked
                    progressData.deathsNumberMedium[SceneManager.GetActiveScene().buildIndex - 7] += 1;
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
            case "Hard":
                if(progressData.levelsUnlocked[2] == SceneManager.GetActiveScene().buildIndex - 6){//Check if that's the highest lvl unlocked
                    progressData.deathsNumberHard[SceneManager.GetActiveScene().buildIndex - 7] += 1;
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
        }
    }
    public void Update() {
        //Debug.Log(progressData.difficultyLevel);
    }
    public void SaveLevelCompletion(){
        switch(progressData.difficultyLevel){
            case "Easy":
                Debug.Log("WTF");
                Debug.Log(progressData.levelsUnlocked[0] < SceneManager.GetActiveScene().buildIndex - 5);
                if(progressData.levelsUnlocked[0] < SceneManager.GetActiveScene().buildIndex - 5){
                    progressData.levelsUnlocked[0] = SceneManager.GetActiveScene().buildIndex - 5;
                    Debug.Log(progressData.levelsUnlocked[0]);
                    Debug.Log(progressData.difficultyLevel);
                    //PorgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Easy, progressData.levelsUnlocked);
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
            case "Medium":
                if(progressData.levelsUnlocked[1] < SceneManager.GetActiveScene().buildIndex - 5){
                    progressData.levelsUnlocked[1] = SceneManager.GetActiveScene().buildIndex - 5;
                    //PorgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Medium, progressData.levelsUnlocked);
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
            case "Hard":
                if(progressData.levelsUnlocked[2] < SceneManager.GetActiveScene().buildIndex - 5){
                    progressData.levelsUnlocked[2] = SceneManager.GetActiveScene().buildIndex - 5;
                    //PorgressSaveSystem.SaveProgressData(CampaignDataClass.DifficultyLevel.Hard, progressData.levelsUnlocked);
                    PorgressSaveSystem.SaveProgressData(progressData);
                }
                break;
        }
    }
}
