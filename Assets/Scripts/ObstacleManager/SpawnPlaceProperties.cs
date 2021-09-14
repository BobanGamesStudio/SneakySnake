using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlaceProperties : MonoBehaviour
{
    public bool isFree;
    private int elementsOn = 0;

    void OnTriggerEnter2D(Collider2D collisionInfo){
        elementsOn++;
        isFree = false;
    }

    private void OnTriggerExit2D(Collider2D collisionInfo) {
        elementsOn--;
        if(elementsOn == 0)
            isFree = true;
    }
}
