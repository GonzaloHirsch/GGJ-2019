using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool isFlashback = false;
    //Distance to the alternate model
    //public float distanceToFlashbackModel = 32f;
    public GameObject[] flashbacksGameObjects;
    public GameObject player;
    public Camera camera;

    private Flashback[] flashbacks;
    private int flashbackIndex;
    private Flashback currentFlashBack;

    private PlayerController playerController;
    private Transform playerTransform;
    //public GameObject flashbackCocina;

    public Text title;
    public Text spaceContinue;
    public Text instructionsText;
    public Text instructions;
    public Image titleBackground;
    public Text gameOverTitle;
    public Text gameOverPhrase;
    public Text creditsTitle;
    public Text credits;
    public GameObject playthroughItems;

    public Image fadingPanel;

    public GameObject musicManagerObject;
    private MusicManager musicManager;

    //Singleton management
    private GameController instance;

    //UI management
    private bool isMenuSet = false;
    private bool isInstructionSet = false;
    private bool isGameOverSet = false;
    private bool isCreditsSet = false;
    //private Flashback flashbackCocinaController;

    // Start is called before the first frame update
    void Start()
    {
        //flashbackCocinaController = flashbackCocina.GetComponent<Flashback>();

        //Singleton GameController
        if (instance != this)
            Destroy(instance);
        this.instance = this;
        DontDestroyOnLoad(this);

        Cursor.visible = false;
        flashbackIndex = 0;

        //cache player data
        this.playerTransform = player.transform;
        this.playerController = player.GetComponent<PlayerController>();

        //Cache music manager
        this.musicManager = musicManagerObject.GetComponent<MusicManager>();

        //cahce flashback data
        flashbacks = new Flashback[flashbacksGameObjects.Length];
        for (int i = 0; i < flashbacksGameObjects.Length; i++)
        {
            flashbacks[i] = flashbacksGameObjects[i].GetComponent<Flashback>();
        }

        //menu setup
        SetUpMenu(true);
        this.isMenuSet = true;

        musicManager.normalMusic.Play();
        musicManager.normalMusic.volume = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isGameOverSet)
                {
                    SetUpGameOver(false);
                    SetUpCredits(true);
                    isCreditsSet = true;
                } else if (isInstructionSet)
                {
                    this.SetUpInstructions(false);
                    musicManager.normalMusic.volume = 0.7f;
                    playerController.isAlive = true;
                    playthroughItems.SetActive(true);
                } else if (isMenuSet)
                {
                    this.SetUpMenu(false);
                    this.SetUpInstructions(true);
                    isInstructionSet = true;
                }
                //if (isMenuSet && !isInstructionSet)
                //{
                //    this.SetUpMenu(false);
                //    this.SetUpInstructions(true);
                //    isInstructionSet = true;
                //} else if (isInstructionSet)
                //{
                //    this.SetUpInstructions(false);

                //    musicManager.normalMusic.volume = 0.7f;
                //    //musicManager.normalMusic.Play();

                //    playerController.isAlive = true;
                //} else if (isGameOverSet && !isCreditsSet)
                //{
                //    SetUpGameOver(false);
                //    SetUpCredits(true);
                //    isCreditsSet = true;
                //}
            }
        }
    }

    //True is active, false is inactive
    private void SetUpMenu(bool state)
    {
        title.gameObject.SetActive(state);
        spaceContinue.gameObject.SetActive(state);
        titleBackground.gameObject.SetActive(state);
    }

    private void SetUpInstructions(bool state)
    {
        spaceContinue.gameObject.SetActive(state);
        instructions.gameObject.SetActive(state);
    }

    private void SetUpGameOver(bool state)
    {
        gameOverTitle.gameObject.SetActive(state);
        spaceContinue.gameObject.SetActive(state);
        gameOverPhrase.gameObject.SetActive(state);
    }

    private void SetUpCredits(bool state)
    {
        creditsTitle.gameObject.SetActive(state);
        credits.gameObject.SetActive(state);
    }

    public void GameOver()
    {
        playthroughItems.SetActive(true);
        playerController.isAlive = false;
        SetUpGameOver(true);
        isGameOverSet = true;
    }

    public void MovePlayer(int mode = 0)
    {
        switch (mode)
        {
            //case 4:
                ////player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                //player.GetComponent<PlayerController>().isInFlashback = true;
                ////player.transform.localRotation = Quaternion.identity;

                //camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                ////flashbackCocina.SetUpFlashback();
                //break;
            case 3:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                flashbacks[2].SetUpFlashback();
                break;
            case 2:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                flashbacks[1].SetUpFlashback();
                break;
            case 1:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                Destroy(flashbacks[0].GetComponent<BoxCollider>());

                //musicManager.flashbackMusic.Play();

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                flashbacks[0].SetUpFlashback();
                break;
            case 0:
                player.GetComponent<PlayerController>().isInFlashback = false;
                //player.transform.localRotation = Quaternion.identity;

                //musicManager.normalMusic.volume = 0.7f;
                //musicManager.normalMusic.Play();

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = false;
                //player.transform.position = new Vector3(playerTransform.position.x - distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
        }
    }

    public Flashback getCurrentFlashBack() {
        return flashbacks[flashbackIndex];
    }

    private void ScreenFadeInOut()
    {
        fadingPanel.gameObject.SetActive(true);
        //DG.Tweening.DOTweenModuleUI.DOFade(fadingPanel, 1, 10);
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn()
    {
        DG.Tweening.DOTweenModuleUI.DOFade(fadingPanel, 1, 1);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadingOut());
    }

    IEnumerator FadingOut()
    {
        DG.Tweening.DOTweenModuleUI.DOFade(fadingPanel, 0, 1);
        yield return new WaitForSeconds(1f);
        fadingPanel.gameObject.SetActive(false);
    }
}
