using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstaclesData
{
    public GameObject[] obstaclesPrefabs;
    [Range(0, 1)]
    public float[] chanceToSpawn;
    [Range(0, 10)]
    public int[] maxNumber;
    [Range(0,20)]
    public float timeBetweenShow;
    [HideInInspector]
    public int[] actualNumber;
}