using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItemFlashback : InteractableItemText
{
    /*
     * 1 = toflashback
     * 0 = tonormal   
     */  
    public int mode = 1;
    public string flashbackText;
    public string text;
    public int flashbackNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        //switch (mode)
        //{
        //    case 1:
        //        text = flashbackText;
        //        break;
        //    case 0:
        //        text = textToShow;
        //        break;
        //}
        StartCoroutine(WaitforText());
    }

    private void GoToFlashback()
    {
        gameController.GetComponent<GameController>().getCurrentFlashBack().nextStep();
        //this.gameController.GetComponent<GameController>().MovePlayer(mode);
        //mode = 1 - mode;
    }

    public override IEnumerator WaitforText()
    {
        textLabel.gameObject.SetActive(true);
        textLabel.text = text;

        yield return new WaitForSeconds(this.timeToRead);

        textLabel.gameObject.SetActive(false);
        this.GoToFlashback();
    }
}
