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
    public MusicManager musicManager;
    public string flashbackText;
    public string text;

    public int flashbackNumber = 1;
    public int stepInFlashback = 1;

    private int hashedNumber;

    // Start is called before the first frame update
    void Start()
    {
        //Hashea haciendo 2^x * 3^y
        this.hashedNumber = ((int)Mathf.Pow(2f, flashbackNumber)) * ((int)Mathf.Pow(3f, stepInFlashback));
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

        //Plays the according sound
        SoundManagement();

        yield return new WaitForSeconds(this.timeToRead);

        textLabel.gameObject.SetActive(false);
        this.GoToFlashback();
    }

    public void SoundManagement()
    {
        switch (hashedNumber)
        {
            case 18:
                musicManager.hotWater.Play();
                break;
            case 24:
                musicManager.chairMovement.Play();
                musicManager.platesMoving.Play();
                break;
            case 54:
                musicManager.waterRunning.Play();
                break;
            case 72:
                musicManager.fireCrackling.Play();
                break;
            case 108:
                musicManager.television.Play();
                break;
            case 729:
                musicManager.placingGlass.Play();
                break;
        }
    }
}
