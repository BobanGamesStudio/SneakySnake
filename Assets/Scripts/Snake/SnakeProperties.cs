using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DiffLevel;

public class SnakeProperties : MonoBehaviour
{
    private GameObject snakeHead; // Snake graphics(sprites)
    private GameObject snakeMid;
    private GameObject snakeTail;

    private GameObject lastSnakePart; // variable to save last added snake part

    [Header("Snake Inity On/Off")]
    public bool snakeInit = true;// If init is true snake will be initialize with script if inity is false you have to set your own snake at scene
    public bool getSpeedFromFile = true;
    [Space]

    [Header("Snake Properties")]

    [HideInInspector]
    public int speed; // speed of snake
    public int speedEasy = 3;
    public int speedMedium = 5;
    public int speedHard = 7;
    public string startDirection = "top"; // top, bottom, right, left. Works only when "snake inity is enabled"
    public Vector2 startPosition; // start pos of snake

    [HideInInspector]
    public List<GameObject> snakeParts = new List<GameObject>(); // List of all snake parts

    public int gridSize = 0; // variable to save grid size from gound properties
    [HideInInspector]
    public float pixelsPerUnite;

    [HideInInspector]
    public int tailStop; // after adding new mid part of snake tail have to stop for some time

    // Init of snake
    void Start()
    {
        if(getSpeedFromFile){
            switch(FindObjectOfType<LevelDifficulty>().difficultyLevel){
                case CampaignDataClass.DifficultyLevel.Easy:
                    speed = speedEasy;
                    break;
                case CampaignDataClass.DifficultyLevel.Medium:
                    speed = speedMedium; 
                    break;
                case CampaignDataClass.DifficultyLevel.Hard:
                    speed = speedHard;
                    break;
            }
        }

        if(gridSize == 0)
            gridSize = FindObjectOfType<GroundProperties>().gridSize;
        pixelsPerUnite = FindObjectOfType<GroundProperties>().pixelsPerUnite;

        gameObject.GetComponent<BoxCollider2D>().offset = startPosition;

        if(snakeInit){

            snakeHead = GetComponent<DictStrGameObj>().dict["Head"];
            snakeMid = GetComponent<DictStrGameObj>().dict["Neck"];
            snakeTail = GetComponent<DictStrGameObj>().dict["Tail"];

            GameObject[] initialArray = new GameObject[3] { snakeHead, snakeMid, snakeTail};
            foreach(GameObject initElem in initialArray)
            {
                lastSnakePart = Instantiate(initElem, new Vector3(0, 0, 0), Quaternion.identity);// initiate snake part
                if(lastSnakePart.GetComponent<Animator>() != null)// set animation speed of all snake parts
                {
                    lastSnakePart.GetComponent<Animator>().SetFloat("Speed", (float)speed);
                }
                lastSnakePart.transform.parent = gameObject.transform;// add snake part as a child
                snakeParts.Add(lastSnakePart);// add snake part to 'snakeParts' list of snake parts
                
                lastSnakePart.GetComponent<SnakePartProperties>().direction = startDirection;
            }

            switch (startDirection)
            {
                case "top":
                    for(int i = 0; i<3; i++){
                        //Debug.Log(startPosition.x + " " + startPosition.y + " " + gridSize + "  " +pixelsPerUnite);
                        snakeParts[i].transform.position = new Vector3(startPosition.x, startPosition.y -gridSize/pixelsPerUnite* i, 0);
                    }
                    break;
                case "bottom":
                    for(int i = 0; i<3; i++){
                        snakeParts[i].transform.Rotate(new Vector3(0, 0, 180));
                        snakeParts[i].transform.position = new Vector3(startPosition.x, startPosition.y + gridSize/pixelsPerUnite* i, 0);
                    }
                    break;
                case "right":
                    for(int i = 0; i<3; i++){
                        snakeParts[i].transform.Rotate(new Vector3(0, 0, 270));
                        snakeParts[i].transform.position = new Vector3(startPosition.x - gridSize/pixelsPerUnite* i, startPosition.y, 0);
                    }
                    break;
                case "left":
                    for(int i = 0; i<3; i++){
                        snakeParts[i].transform.Rotate(new Vector3(0, 0, 90));
                        snakeParts[i].transform.position = new Vector3(startPosition.x + gridSize/pixelsPerUnite* i, startPosition.y, 0);
                    }
                    break;
                default:
                    Debug.Log("Wrong snake direction(start)!!!!");
                    break;
            }
        }
        else{
            //Debug.Log(gameObject.transform.childCount);
            for (int i = 0; i< gameObject.transform.childCount; i++)
            {
                if(gameObject.transform.GetChild(i).GetComponent<Animator>() != null)// set animation speed of all snake parts
                {
                    gameObject.transform.GetChild(i).GetComponent<Animator>().SetFloat("Speed", (float)speed);
                }
                snakeParts.Add(gameObject.transform.GetChild(i).gameObject);// add snake part to 'snakeParts' list of snake parts
            }
        }
    }
}
