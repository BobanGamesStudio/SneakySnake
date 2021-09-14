using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DestroyOnCollision : MonoBehaviour
{

    public string[] appleSounds = new string[0];
    public string[] pillSounds = new string[] {"AppleDestroySound", "PillDestroySound"};
    int choosenPlace = 0;

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.tag == "Player"){ 
            if( FindObjectOfType<SpawningObstacles>().enabled == true ){// managing destroy when there is "obstacle manager in game"
                int subtractionIndex = Array.FindIndex( FindObjectOfType<ObstacleProperties>().spawningObst.obstaclesPrefabs, ele => ele.tag == gameObject.tag); // Find number of obst type
                FindObjectOfType<ObstacleProperties>().spawningObst.actualNumber[subtractionIndex] -= 1;//Change saved number of obstacles
            }
            Destroy(gameObject);
        }
        if (gameObject.tag == "Apple"){

            choosenPlace = UnityEngine.Random.Range(0, appleSounds.Length);
            
            if(FindObjectOfType<QuestCollectApples>().enabled == true){
                if(FindObjectOfType<QuestCollectApples>().applesLeft > 0)
                    FindObjectOfType<QuestCollectApples>().applesLeft -= 1;
            }
            if(FindObjectOfType<QuestBonus>().enabled== true){
                FindObjectOfType<QuestBonus>().applesCollected += 1;
            }
        }
        if (gameObject.tag == "Pill"){

            choosenPlace = UnityEngine.Random.Range(0, pillSounds.Length);

            if(FindObjectOfType<QuestCollectPills>().enabled == true){
                if(FindObjectOfType<QuestCollectPills>().pillsLeft > 0)
                    FindObjectOfType<QuestCollectPills>().pillsLeft -= 1;
            }
            if(FindObjectOfType<QuestBonus>().enabled== true){
                FindObjectOfType<QuestBonus>().pillsCollected += 1;
            }
        }
    }
}
