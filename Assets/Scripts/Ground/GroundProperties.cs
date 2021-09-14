using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProperties : MonoBehaviour
{
    public int gridSize = 30;
    public GameObject groundSprite;
    [HideInInspector]
    public float pixelsPerUnite;

    public Vector2 groundSize = new Vector2(5, 5); // Must be odd
    
    private void Awake() {
        pixelsPerUnite = groundSprite.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    }
}
