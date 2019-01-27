using System.Collections;
using UnityEngine;

public class InteractableItemFlashback : InteractableItemText
{
    /*
     * 3 = toflashbackliving  
     * 2 = toflashbackfamily
     * 1 = toflashbackcocina
     * 0 = tonormal   
     */ 
    public int mode = 1;
    public GameObject musicManagerObject;

    //public string flashbackText;
    //public string text;

    public int flashbackNumber = 1;
    public int stepInFlashback = 1;

    private int hashedNumber;
    private MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        //Hashea haciendo 2^x * 3^y
        this.hashedNumber = ((int)Mathf.Pow(2f, flashbackNumber)) * ((int)Mathf.Pow(3f, stepInFlashback));
        this.musicManager = musicManagerObject.GetComponent<MusicManager>();
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

    //private void GoToFlashback()
    //{
    //    gameController.GetComponent<GameController>().getCurrentFlashBack().nextStep();
    //    //this.gameController.GetComponent<GameController>().MovePlayer(mode);
    //    //mode = 1 - mode;
    //}

    public override IEnumerator WaitforText()
    {
        textLabel.gameObject.SetActive(true);
        textLabel.text = textToShow;

        //Plays the according sound
        SoundManagement();

        yield return new WaitForSeconds(this.timeToRead);

        textLabel.gameObject.SetActive(false);

        //moving to the next step in the flashback
        gameController.GetComponent<GameController>().getCurrentFlashBack().nextStep();
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
