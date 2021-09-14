using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Button madeByButton;
    public Button thanksToButton;

    private bool madeByPressed = true;
    private bool thanksToPressed = false;


    // Update is called once per frame
    void Update()
    {   
        if(madeByPressed){
            madeByButton.Select();
        }

        if(thanksToPressed){
            thanksToButton.Select();
        }
        
    }

    public void MadeByPressed(){
        if(!madeByPressed){
            FindObjectOfType<AboutSceneManager>().PlayButtonSound();

            madeByPressed = true;
            thanksToPressed = false;

            gameObject.transform.Find("MadeByBackground").gameObject.SetActive(true);
            gameObject.transform.Find("ThanksToBackground").gameObject.SetActive(false);
        }
    }

    public void ThanksToPressed(){
        if(!thanksToPressed){
            FindObjectOfType<AboutSceneManager>().PlayButtonSound();

            thanksToPressed = true;
            madeByPressed = false;

            gameObject.transform.Find("ThanksToBackground").gameObject.SetActive(true);
            gameObject.transform.Find("MadeByBackground").gameObject.SetActive(false);
        }
    }
}
