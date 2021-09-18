using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeGrow : MonoBehaviour
{
    private List<GameObject> snakeParts;
    private GameObject snakeMid;
    private int tailStop;
    
    private GameObject addedPart;

    public void IncreaseTheSnake(){ // Increasing snake by one mid part at the end
        snakeParts = GetComponent<SnakeProperties>().snakeParts;
        snakeMid = GetComponent<DictStrGameObj>().dict["Mid"];

        GetComponent<SnakeProperties>().tailStop = 2; // sets counter for stop the snake tail (*2 cause there are two elements that must stop)
        addedPart = Instantiate(snakeMid, snakeParts[snakeParts.Count-2].transform.position, Quaternion.identity);
        addedPart.GetComponent<Animator>().SetFloat("Speed", (float)GetComponent<SnakeProperties>().speed); // Set animator speed
        addedPart.transform.parent = gameObject.transform; // set as child of "Player"
        addedPart.GetComponent<SpriteRenderer>().enabled = false; // hide part
        addedPart.GetComponent<SnakePartProperties>().direction = snakeParts[snakeParts.Count - 2].GetComponent<SnakePartProperties>().direction;
        
        GetComponent<SnakeMovement>().hiddenParts.Add(addedPart);
        snakeParts.Insert(snakeParts.Count-1, addedPart); // add part to list of snake parts
        
        GetComponent<SnakeMovement>().MakeRotation(addedPart, snakeParts[snakeParts.Count - 2].GetComponent<SnakePartProperties>().direction);

        snakeParts[snakeParts.Count - 1].GetComponent<Animator>().enabled = false;  
    }

    public void ShrinkTheSnake(){ // Decreasing snake by one mid part at the end
        snakeParts = GetComponent<SnakeProperties>().snakeParts;

        if(snakeParts.Count > 3)
        {
            snakeParts[snakeParts.Count-1].transform.position = snakeParts[snakeParts.Count-2].transform.position;
            Destroy(snakeParts[snakeParts.Count-2]);
            snakeParts.RemoveAt(snakeParts.Count-2);
            snakeParts[snakeParts.Count - 1].GetComponent<SnakePartProperties>().direction = snakeParts[snakeParts.Count-2].GetComponent<SnakePartProperties>().direction;
            GetComponent<SnakeMovement>().MakeRotation(snakeParts[snakeParts.Count - 1], snakeParts[snakeParts.Count-2].GetComponent<SnakePartProperties>().direction);
        }
    }
}
