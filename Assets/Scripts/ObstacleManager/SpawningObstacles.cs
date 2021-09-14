using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawningObstacles : MonoBehaviour
{

    private Vector2 groundSize;
    private int gridSize;
    private float pixelsPerUnite;
    private Vector3 spawnPos;

    private ObstaclesData spawningObst;
    private GameObject lastAddedObst;

    public bool rectSpawn;
    private GameObject[] spawnPlaces;

    // Start is called before the first frame update
    void Start()
    {
        spawningObst = FindObjectOfType<ObstacleProperties>().spawningObst;
        spawnPlaces = GameObject.FindGameObjectsWithTag("SpawnPlace");

        if(spawningObst.obstaclesPrefabs.Length != spawningObst.chanceToSpawn.Length | // Check if obst data got apropriate size
            spawningObst.obstaclesPrefabs.Length != spawningObst.maxNumber.Length) 
        {
            Debug.LogError("arrays at 'spawningObst' are not equal length");
            Application.Quit();
        }
        else
        {
            spawningObst.actualNumber = new int[spawningObst.obstaclesPrefabs.Length]; // create vector for counting how many obstacles is already in game
        }

        if(rectSpawn){
            groundSize = FindObjectOfType<GroundProperties>().groundSize; // Copy ground size
            gridSize = FindObjectOfType<GroundProperties>().gridSize; // Copy grid size
            pixelsPerUnite = FindObjectOfType<GroundProperties>().pixelsPerUnite;

            InvokeRepeating("SpawnObstacleRect", 1, spawningObst.timeBetweenShow); //calls ChangePosition() every x secs
        }
        else
            InvokeRepeating("SpawnObstacle", 1, spawningObst.timeBetweenShow);
    }


    /// <summary>
    /// Function that spawns obstacles. Works only for rectangle ground that size is odd
    /// </summary>
    void SpawnObstacleRect()
    {
        if(GameObject.FindGameObjectWithTag("SpawnPlace") != null){
            Debug.LogError("You choose 'Rect spawn' option while there are 'Spawn Places' at scene");
        }
        for( int i = 0; i < spawningObst.obstaclesPrefabs.Length; i++)
        {
            if(spawningObst.actualNumber[i] < spawningObst.maxNumber[i])
            {
                if(UnityEngine.Random.Range(0f, 1f) < spawningObst.chanceToSpawn[i])
                {
                    bool added = false;
                    //while checks if there is free space at board
                    //while(added == false & groundSize.x * groundSize.y > FindObjectOfType<SnakeProperties>().snakeParts.Count + GetComponent<ObstacleProperties>().spawningObst.actualNumber.Sum()){
                    while(added == false & groundSize.x * groundSize.y > FindObjectOfType<SnakeProperties>().snakeParts.Count + GetComponent<ObstacleProperties>().transform.childCount){    
                        
                        spawnPos = new Vector3(UnityEngine.Random.Range( -(int)(groundSize.x-1)/2, (int)(groundSize.x-1)/2 + 1 ), 
                                                UnityEngine.Random.Range( -(int)(groundSize.x-1)/2, (int)(groundSize.x-1)/2 + 1 ), 0) * gridSize/pixelsPerUnite;

                        if( !gameObject.transform.GetAllChildren().Exists(obst => obst.transform.position == spawnPos) ) // Check if any object is at spawnPos
                        {
                            if( !FindObjectOfType<SnakeProperties>().snakeParts.Exists(part => part.transform.position == spawnPos)) // Check if there is any snake part at choosen pos
                            {
                                lastAddedObst = Instantiate(spawningObst.obstaclesPrefabs[i], spawnPos, Quaternion.identity);
                                lastAddedObst.transform.parent = gameObject.transform;

                                PlaySpawnSound(spawningObst.obstaclesPrefabs[i].tag);

                                spawningObst.actualNumber[i] += 1;
                                added = true;
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Function that spawns obstacles. Works only for rectangle ground that size is odd
    /// </summary>
    void SpawnObstacle()
    {
        if(GameObject.FindGameObjectWithTag("SpawnPlace") == null){
            Debug.LogError("You unchoose 'Rect spawn' option while there are NO 'Spawn Places' at scene");
        }
        if(GetAllFreeSpawnPlaces() != null){
            List<GameObject> allFreeSpawnPlaces;
            int choosenPlace;
            for( int i = 0; i < spawningObst.obstaclesPrefabs.Length; i++)
            {
                allFreeSpawnPlaces = GetAllFreeSpawnPlaces();
                if(allFreeSpawnPlaces != null){
                    if(spawningObst.actualNumber[i] < spawningObst.maxNumber[i])
                    {
                        if(UnityEngine.Random.Range(0f, 1f) < spawningObst.chanceToSpawn[i])
                        {
                            choosenPlace = UnityEngine.Random.Range(0, allFreeSpawnPlaces.Count - 1);

                            spawnPos = new Vector3(allFreeSpawnPlaces[choosenPlace].transform.position.x, 
                                                    allFreeSpawnPlaces[choosenPlace].transform.position.y, 0);

                            lastAddedObst = Instantiate(spawningObst.obstaclesPrefabs[i], spawnPos, Quaternion.identity);
                            lastAddedObst.transform.parent = gameObject.transform;

                            PlaySpawnSound(spawningObst.obstaclesPrefabs[i].tag);
                            
                            spawningObst.actualNumber[i] += 1;

                            allFreeSpawnPlaces[choosenPlace].GetComponent<SpawnPlaceProperties>().isFree = false;
                        }
                    }
                }
            }
        }
    }

    public void PlaySpawnSound(string obstacleTag){
        if(FindObjectOfType<AudioManager>() != null){
            if(obstacleTag == "Apple"){
                FindObjectOfType<AudioManager>().PlaySoundContaining("AppleShowUp", SceneManager.GetActiveScene().buildIndex);
            }
            else{
                if(obstacleTag == "Pill"){
                    FindObjectOfType<AudioManager>().PlaySoundContaining("PillShowUp", SceneManager.GetActiveScene().buildIndex);
                }
                else{
                    Debug.Log("Uknown tag while spawning: " + obstacleTag);
                }
            }
        }
        else{
            Debug.Log("Audio Manager not found in scene: " + SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    List<GameObject> GetAllFreeSpawnPlaces(){
        List<GameObject> freeSpawnPlaces = new List<GameObject>();

        GameObject[] allChilderen = GameObject.FindGameObjectsWithTag("SpawnPlace");
        //List<Transform> allChilderen = gameObject.transform.GetAllChildren();

        for(int i = 0; i< allChilderen.Length; i++){
            if(allChilderen[i].tag == "SpawnPlace"){
                if(allChilderen[i].activeInHierarchy){
                    if(allChilderen[i].GetComponent<SpawnPlaceProperties>().isFree){
                        freeSpawnPlaces.Add(allChilderen[i]);
                    }
                }
            }
        }
        if(freeSpawnPlaces.Count == 0)
            return null;
        else
            return freeSpawnPlaces;
    }
    
    

}
