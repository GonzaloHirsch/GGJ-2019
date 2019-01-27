using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool isFlashback = false;
    //Distance to the alternate model
    public float distanceToFlashbackModel = 32f;
    public GameObject[] flashbacksGameObjects;
    public Flashback[] flashbacks;
    public Flashback currentFlashBack;
    private int flashbackIndex;

    public GameObject player;
    public Camera camera;

    private PlayerController playerController;
    private Transform playerTransform;
    public GameObject flashbackCocina;
    

    private GameController instance;
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

        //cahce flashback data
        flashbacks = new Flashback[flashbacksGameObjects.Length];
        for (int i = 0; i < flashbacksGameObjects.Length; i++)
        {
            flashbacks[i] = flashbacksGameObjects[i].GetComponent<Flashback>();
        }

        //menu setup
        SetUpMenu(true);
        this.isMenuSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isMenuSet && !isInstructionSet)
                {
                    this.SetUpMenu(false);
                    this.SetUpInstructions(true);
                    isInstructionSet = true;
                } else if (isInstructionSet)
                {
                    this.SetUpInstructions(false);
                    playerController.isAlive = true;
                } else if (isGameOverSet && !isCreditsSet)
                {
                    SetUpGameOver(false);
                    SetUpCredits(true);
                    isCreditsSet = true;
                }
            }
        }
    }

    //True is active, false is inactive
    private void SetUpMenu(bool state)
    {
        //TODO ver UI
    }

    private void SetUpInstructions(bool state)
    {
        //TODO ver UI
    }

    private void SetUpGameOver(bool state)
    {
        //TODO ver UI
    }

    private void SetUpCredits(bool state)
    {
        //TODO ver UI
    }

    public void GameOver()
    {
        playerController.isAlive = false;
        SetUpGameOver(true);
        isGameOverSet = true;
    }

    //TODO: Hacer que renderee y desrenderee el modelo que no se esta usando
    public void MovePlayer(int mode = 0)
    {
        switch (mode)
        {
            //Go to flashback cocina
            case 4:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                //flashbackCocina.SetUpFlashback();
                break;
            //Go to flashback cocina
            case 3:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                //flashbackCocina.SetUpFlashback();
                break;
            //Go to flashback cocina
            case 2:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                //flashbackCocina.SetUpFlashback();
                break;
            //Go to flashback cocina
            case 1:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                //player.transform.localRotation = Quaternion.identity;
                
                camera.GetComponent<PostProcessingBehaviour>().enabled = true;

                Destroy(flashbacks[0].GetComponent<BoxCollider>());
                flashbacks[0].SetUpFlashback();
                break;
            case 0:
                player.GetComponent<PlayerController>().isInFlashback = false;
                //player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = false;
                //player.transform.position = new Vector3(playerTransform.position.x - distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
        }
    }

    public Flashback getCurrentFlashBack() {
        return flashbacks[flashbackIndex];
    }
}
