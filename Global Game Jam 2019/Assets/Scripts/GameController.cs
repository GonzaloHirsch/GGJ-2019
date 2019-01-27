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

    public Text SpaceContinue;
    public GameObject Instructions;
    public GameObject Menu;
    public GameObject gameOver;
    public GameObject Credits;
    public GameObject playthroughItems;

    public Image fadingPanel;

    public GameObject musicManagerObject;
    private MusicManager musicManager;

    //Singleton management
    private GameController instance;

    private int doneFlashbacks = 0;

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
        musicManager.normalMusic.volume = 0.5f;
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
                    musicManager.normalMusic.volume = 0.25f;
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
        else
        {
            if (doneFlashbacks == 3) { GameOver(); }
        }
    }

    //True is active, false is inactive
    private void SetUpMenu(bool state)
    {
        SpaceContinue.gameObject.SetActive(state);
        Menu.SetActive(state);
    }

    private void SetUpInstructions(bool state)
    {
        SpaceContinue.gameObject.SetActive(state);
        Instructions.SetActive(state);
    }

    private void SetUpGameOver(bool state)
    {
        gameOver.SetActive(state);
        SpaceContinue.gameObject.SetActive(state);
    }

    private void SetUpCredits(bool state)
    {
        Credits.SetActive(state);
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

                StartCoroutine(FadeMusic(musicManager.normalMusic, musicManager.flashbackMusic, 0.5f));
                //musicManager.normalMusic.Pause();
                //musicManager.flashbackMusic.Play();
                //musicManager.flashbackMusic.volume = 0.5f;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;
                flashbackIndex = 2;
                flashbacks[2].SetUpFlashback();
                break;
            case 2:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                StartCoroutine(FadeMusic(musicManager.normalMusic, musicManager.flashbackMusic, 0.5f));
                //musicManager.normalMusic.Pause();
                //musicManager.flashbackMusic.Play();
                //musicManager.flashbackMusic.volume = 0.5f;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;
                flashbackIndex = 1;
                flashbacks[1].SetUpFlashback();
                break;
            case 1:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                Destroy(flashbacks[0].GetComponent<BoxCollider>());

                StartCoroutine(FadeMusic(musicManager.normalMusic, musicManager.flashbackMusic, 0.5f));
                //musicManager.normalMusic.Pause();
                //musicManager.flashbackMusic.Play();
                //musicManager.flashbackMusic.volume = 0.5f;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;
                flashbackIndex = 0;
                flashbacks[0].SetUpFlashback();
                break;
            case 0:
                player.GetComponent<PlayerController>().isInFlashback = false;
                //player.transform.localRotation = Quaternion.identity;

                StartCoroutine(FadeMusic(musicManager.flashbackMusic, musicManager.normalMusic, 0.25f));

                //musicManager.flashbackMusic.Stop();
                //musicManager.normalMusic.Play();
                //musicManager.normalMusic.volume = 0.25f;

                ScreenFadeInOut();

                camera.GetComponent<PostProcessingBehaviour>().enabled = false;

                doneFlashbacks++;
                //player.transform.position = new Vector3(playerTransform.position.x - distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
        }
    }

    public Flashback getCurrentFlashBack() {
        return flashbacks[flashbackIndex];
    }

    IEnumerator FadeMusic(AudioSource sourceOut, AudioSource sourceIn, float volume)
    {
        DG.Tweening.DOTweenModuleAudio.DOFade(sourceOut, 0, 2);
        sourceOut.Pause();
        yield return new WaitForSeconds(2);
        sourceIn.Play();
        DG.Tweening.DOTweenModuleAudio.DOFade(sourceIn, volume, 2);
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
