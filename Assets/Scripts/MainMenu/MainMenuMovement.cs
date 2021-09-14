using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMovement : MonoBehaviour
{
    public Vector2 endPos = new Vector2(0, 0);
    
    public List<string> directions = new List<string>();
    public List<Vector2> bentPos = new List<Vector2>();

    private int movementNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(directions.Count != bentPos.Count){
            Debug.LogError("Directions and possitions are not equal length " + gameObject.name);
        }
    }

    private void Update() {
        if(movementNum < bentPos.Count){
            if(gameObject.GetComponent<BoxCollider2D>().offset.x > bentPos[movementNum].x - 0.05 && gameObject.GetComponent<BoxCollider2D>().offset.x < bentPos[movementNum].x + 0.05){
                if(gameObject.GetComponent<BoxCollider2D>().offset.y > bentPos[movementNum].y - 0.05 && gameObject.GetComponent<BoxCollider2D>().offset.y < bentPos[movementNum].y + 0.05){
                    gameObject.GetComponent<SnakeMovement>().snakeMovesInput.Add(directions[movementNum]);
                    movementNum += 1;
                }
            }
        }

        if(gameObject.GetComponent<BoxCollider2D>().offset.x > endPos.x - 0.05 && gameObject.GetComponent<BoxCollider2D>().offset.x < endPos.x + 0.05){
            if(gameObject.GetComponent<BoxCollider2D>().offset.y > endPos.y - 0.05 && gameObject.GetComponent<BoxCollider2D>().offset.y < endPos.y + 0.05){
                gameObject.GetComponent<SnakeMovement>().CancelInvoke();
            }
        }
    }
        
}
