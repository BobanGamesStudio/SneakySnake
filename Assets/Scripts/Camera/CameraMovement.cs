using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject snake;
    public bool cameraMoveActive = false; // if true then camera is following player at stage  
    Camera cam;

    private void Start() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cameraMoveActive){   
            Vector3 snakePos = cam.WorldToScreenPoint(new Vector3(snake.transform.position.x + snake.GetComponent<BoxCollider2D>().offset.x,
                                                        snake.transform.position.y + snake.GetComponent<BoxCollider2D>().offset.y,
                                                        snake.transform.position.z));
            if(snakePos.x < 3/8f * cam.pixelWidth)
                cam.transform.position -= new Vector3(0.02f, 0, 0);
            if(snakePos.x > 5/8f * cam.pixelWidth)
                cam.transform.position += new Vector3(0.02f, 0, 0);
            if(snakePos.y < 3/8f * cam.pixelHeight)
                cam.transform.position -= new Vector3(0, 0.02f, 0);
            if(snakePos.y > 5/8f * cam.pixelHeight)
                cam.transform.position += new Vector3(0, 0.02f, 0);
        }
    }
}
