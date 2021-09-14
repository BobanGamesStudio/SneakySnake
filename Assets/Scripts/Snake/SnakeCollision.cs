using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;

public class SnakeCollision : MonoBehaviour
{
    private BoxCollider2D snakeCollider;
    public bool ignoreSnake = false;

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {   
        //Debug.Log("Collision    " + gameObject.tag + "   " +  collisionInfo.tag + "  " + "   " + gameObject.name + "  " +collisionInfo.name);
        switch (collisionInfo.tag)
        {
            case "Apple":
                if(FindObjectOfType<AudioManager>() != null)
                    if(SceneManager.GetActiveScene().buildIndex != 1)
                        FindObjectOfType<AudioManager>().PlaySoundContaining("EatApple", SceneManager.GetActiveScene().buildIndex);
                GetComponent<SnakeGrow>().IncreaseTheSnake();
                break;
            case "Pill":
                if(FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().PlaySoundContaining("EatPill", SceneManager.GetActiveScene().buildIndex);
                GetComponent<SnakeGrow>().ShrinkTheSnake();
                break;
            case "PlayerPart":
                if(!ignoreSnake){
                    if(FindObjectOfType<AudioManager>() != null)
                        FindObjectOfType<AudioManager>().PlaySoundContaining("SnakeCrash", SceneManager.GetActiveScene().buildIndex);
                    FindObjectOfType<EndGameManagement>().EndGameDeath();
                }
                break; 
            case "Wall":
                if(FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().PlaySoundContaining("WallCrash", SceneManager.GetActiveScene().buildIndex);
                FindObjectOfType<EndGameManagement>().EndGameDeath();
                break;
            case "SpawnPlace":
                break;
            case "Player":
                break;
            default:
                Debug.Log("Wrong collision tag(snake collision)!!!! " + collisionInfo.tag);
                break;
        }
    }
}
