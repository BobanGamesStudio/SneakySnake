using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class SnakeMovement : MonoBehaviour
{
    public int gridSize = 0;
    private float pixelsPerUnite;
    private List<GameObject> snakeParts;
    private int speed;
    private int tailStop;

    [HideInInspector]
    public List<GameObject> hiddenParts;

    private string[] clockwiseTab = {"top", "right", "bottom", "left", "top"};

    [HideInInspector]
    public List<string> snakeMovesInput = new List<string>(); // Moves from input(from player)

    // Start is called before the first frame update
    void Start()
    {   
        if(gridSize == 0)
            gridSize = FindObjectOfType<GroundProperties>().gridSize; // Copy important things
        
        pixelsPerUnite = GetComponent<SnakeProperties>().pixelsPerUnite;
        snakeParts = GetComponent<SnakeProperties>().snakeParts;
        speed = GetComponent<SnakeProperties>().speed;

        InvokeRepeating("MoveHandler", 0, 1f/speed); //calls MoveHandler() every x secs
    }

    void FixedUpdate() // Take player input and save it at list
    {
        string[,] sides = new string[4, 2]{ {"right", "left"}, {"left", "right"},{"top", "bottom"},{"bottom", "top"} };
        KeyCode[,] codes = new KeyCode[4, 2]{ {KeyCode.D, KeyCode.RightArrow}, {KeyCode.A, KeyCode.LeftArrow}, {KeyCode.W, KeyCode.UpArrow}, {KeyCode.S, KeyCode.DownArrow} }; 
        
        int keyCounter = 0;
        foreach (KeyCode code in codes)
        {
            if (Input.GetKey(code) ) keyCounter++;
        }

        for(int i = 0; i < sides.Length/2; i++)
        {   
            if (keyCounter == 1) {
                if(Input.GetKey(codes[i, 0]) | Input.GetKey(codes[i, 1]))
                {   
                    if (snakeMovesInput.Count > 0){
                        if(snakeMovesInput[snakeMovesInput.Count-1] != sides[i, 0] & snakeMovesInput[snakeMovesInput.Count-1] != sides[i, 1]){
                            snakeMovesInput.Add(sides[i, 0]);
                        }
                    }
                    else{
                        string headDir = snakeParts[0].GetComponent<SnakePartProperties>().direction;
                        if(headDir != sides[i, 0] & headDir != sides[i, 1]){
                            snakeMovesInput.Add(sides[i, 0]);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Function that handles all movment things
    /// </summary>
    void MoveHandler() // Changing position of snake over time
    {   

        tailStop = GetComponent<SnakeProperties>().tailStop;

        UnhidePart();
        RotateParts(); 
        PlayAnimations();
        ChangePositions();
        
        GetComponent<SnakeProperties>().tailStop = tailStop;
        
    }

    /// <summary>
    /// Unhides hidden parts of snake
    /// </summary>
    void UnhidePart(){
        if(hiddenParts.Count > 0){
            hiddenParts[0].GetComponent<SpriteRenderer>().enabled = true;
            hiddenParts.RemoveAt(0);
            snakeParts[snakeParts.Count - 1].GetComponent<Animator>().enabled = true; 
        }
    }

    /// <summary>
    /// Rotates all parts in right direction.
    /// </summary>
    void RotateParts()
    {
        for(int i = snakeParts.Count -1; i > -1; i--){ // go through all snake parts
            if(i == 0){ // changing direction of head
                if (snakeMovesInput.Count > 0){ // if there is smth on input
                    
                    MakeRotation(snakeParts[i], snakeMovesInput[0]);
                    
                    snakeParts[0].GetComponent<SnakePartProperties>().direction = snakeMovesInput[0];//ABC
                    snakeMovesInput.RemoveAt(0);
                }
            }else // all parts except head
            {
                if(snakeParts[i-1].GetComponent<SnakePartProperties>().direction != snakeParts[i].GetComponent<SnakePartProperties>().direction){
                    if(tailStop == 0 | i < (snakeParts.Count - 2)){
                        MakeRotation(snakeParts[i], snakeParts[i-1].GetComponent<SnakePartProperties>().direction);

                        snakeParts[i].GetComponent<SnakePartProperties>().direction = snakeParts[i-1].GetComponent<SnakePartProperties>().direction;
                    }
                }
            }
        }
        for(int i = snakeParts.Count -1; i > -1; i--){
            if(snakeParts[i].GetComponent<Animator>() != null)
            {
                if(i > 0 & (i <  snakeParts.Count - 1)) // for mid parts
                {   
                    if(snakeParts[i-1].GetComponent<SnakePartProperties>().direction != snakeParts[i].GetComponent<SnakePartProperties>().direction){// Activating the Bent
                        snakeParts[i].GetComponent<Animator>().SetBool("Bent", true);

                        if( clockwiseTab[Array.IndexOf(clockwiseTab, snakeParts[i].GetComponent<SnakePartProperties>().direction) + 1] == snakeParts[i-1].GetComponent<SnakePartProperties>().direction ){ // check if movement is clockwise
                            if(i==1){
                                snakeParts[i].GetComponent<SpriteRenderer>().flipX = true;
                                snakeParts[i].transform.eulerAngles = new Vector3(0, 0, snakeParts[i].transform.eulerAngles.z - 90); // clockwise
                            }
                            else{
                                snakeParts[i].transform.eulerAngles = new Vector3(0, 0, snakeParts[i].transform.eulerAngles.z + 180); // clockwise
                            }
                        }
                        else{ // not clockwise
                            if(i == 1){ // Change direction of neck
                                snakeParts[i].GetComponent<SpriteRenderer>().flipX = false;
                            }
                            snakeParts[i].transform.eulerAngles = new Vector3(0, 0, snakeParts[i].transform.eulerAngles.z + 90);
                        }
                    }
                    else{
                        if(i==1){
                            snakeParts[i].GetComponent<SpriteRenderer>().flipX = false;
                        }
                        snakeParts[i].GetComponent<Animator>().SetBool("Bent", false);
                    }
                }
                else{
                    if(i == snakeParts.Count - 1)
                    {
                        MakeRotation(snakeParts[i], snakeParts[i-1].GetComponent<SnakePartProperties>().direction);
                    }
                }
            }
            else{
                Debug.LogError("Snake part " + snakeParts[i].name + " dont have animator!!!");
            }
        }
    }

    /// <summary>
    /// Rotates given snake part in given direction
    /// </summary>
    public void MakeRotation(GameObject partToRotate, string direction)
    {
        switch (direction){ // rotate snake part
            case "top":
                partToRotate.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case "bottom":
                partToRotate.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case "right":
                partToRotate.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case "left":
                partToRotate.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            default:
                Debug.Log("Wrong snake direction !!!!" + partToRotate.name);
                break;
        }
    }

    /// <summary>
    /// Plays animations for all parts
    /// </summary>
    void PlayAnimations()
    {
        for(int i = 0; i < snakeParts.Count; i++) // Moving parts forward
        {
            snakeParts[i].GetComponent<Animator>().Play(0, 0, 0f); // Run snake part animation
        }
    }

    /// <summary>
    /// Moves all snake parts forward in the right directions
    /// </summary>
    void ChangePositions()
    {
        for(int i = 0; i < snakeParts.Count; i++){
            if(tailStop == 0 | i < (snakeParts.Count - 2)){
                switch(snakeParts[i].GetComponent<SnakePartProperties>().direction){
                    case "top":
                        snakeParts[i].transform.position += new Vector3(0, gridSize/pixelsPerUnite, 0);
                        break;
                    case "bottom":
                        snakeParts[i].transform.position += new Vector3(0, -gridSize/pixelsPerUnite, 0);
                        break;
                    case "right":
                        snakeParts[i].transform.position += new Vector3(gridSize/pixelsPerUnite, 0, 0);
                        break;
                    case "left":
                        snakeParts[i].transform.position += new Vector3(-gridSize/pixelsPerUnite, 0, 0);
                        break;
                    default:
                        Debug.Log("Wrong snake direction(update)!!!!");
                        break;
                }
            }
            else{ // Decrease counter of 'tail stop'
                tailStop -= 1;
            }

            if(i == 0){ // Move player collider, head collider is not used, instead player got it's own collider.
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(snakeParts[0].transform.position.x, snakeParts[0].transform.position.y);
            }
        }
    }

}
