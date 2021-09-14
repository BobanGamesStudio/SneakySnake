using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsPadlock : MonoBehaviour
{
    private List<Transform> aList;

    public ProgressData progressData;
    private int levelDiff;

    public GameObject padlockPanel;
    public GameObject outlinePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        PadlockLevels();
    }

    public void PadlockLevels(){
        //progressData = FindObjectOfType<CampaignSceneManager>().progressData;
        progressData = SaveSystem.LoadProgressData();

        switch(progressData.difficultyLevel){
            case "Easy":
                levelDiff = 0;
                break;
            case "Medium":
                levelDiff = 1;
                break;
            case "Hard":
                levelDiff = 2;
                break;
        }

        //Debug.Log("LEVELS PADLOCK: " + progressData.levelsUnlocked[0] + "  " + progressData.levelsUnlocked[1] + "  " + progressData.levelsUnlocked[2]);

        aList = gameObject.transform.GetAllChildren();
        foreach(Transform child in aList){
            if(child.gameObject.name.Contains("Button")){
                if(child.gameObject.GetComponent<ShowLevelStats>().levelNum > progressData.levelsUnlocked[levelDiff]){
                    if(child.gameObject.GetChild("PadlockPanel") == null){
                        // Add padlock panel
                        GameObject addedPadlockPanel = Instantiate(padlockPanel, child.gameObject.transform.position , Quaternion.identity);
                        addedPadlockPanel.name = "PadlockPanel";
                        addedPadlockPanel.transform.SetParent(child.gameObject.transform, false); // set as child of "Player"
                        addedPadlockPanel.transform.position = child.gameObject.transform.position;

                        if(child.gameObject.GetChild("Pipes") != null){
                            child.gameObject.GetChild("Pipes").SetActive(false);
                        }

                        child.gameObject.GetComponent<Button>().interactable = false;
                    }
                    if(gameObject.GetChild("LVL" + child.GetComponent<ShowLevelStats>().levelNum.ToString() +"Outline") != null){
                        // Destroy outline panel
                        Destroy( gameObject.GetChild("LVL" + child.GetComponent<ShowLevelStats>().levelNum.ToString() +"Outline") );
                    }
                }
                else{
                    if(child.gameObject.GetChild("PadlockPanel") != null){
                        //Destroy Padlock panel
                        Destroy( child.gameObject.GetChild("PadlockPanel") );

                        if(child.gameObject.GetChild("Pipes") != null){
                            child.gameObject.GetChild("Pipes").SetActive(true);
                        }

                        child.gameObject.GetComponent<Button>().interactable = true;
                    }
                    if(gameObject.GetChild("LVL" + child.GetComponent<ShowLevelStats>().levelNum.ToString() +"Outline") == null){
                        // Add outline panel
                        GameObject addedOutlinePanel = Instantiate(outlinePanel, child.gameObject.transform.position , Quaternion.identity);
                        addedOutlinePanel.name = "LVL" + child.GetComponent<ShowLevelStats>().levelNum.ToString() +"Outline";
                        addedOutlinePanel.transform.SetParent(gameObject.transform, false); // set as child of "Player"
                        addedOutlinePanel.transform.position = child.gameObject.transform.position;
                        addedOutlinePanel.transform.SetSiblingIndex(child.transform.GetSiblingIndex());
                    }
                }
            }
        }
    }
}
