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

    public GameObject player;
    public Camera camera;
    private Transform playerTransform;

    private GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        //Singleton GameController
        if (instance != this)
            Destroy(instance);

        this.instance = this;
        DontDestroyOnLoad(this);

        Cursor.visible = false;


        this.playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Hacer que renderee y desrenderee el modelo que no se esta usando
    public void MovePlayer(int mode = 0)
    {
        switch (mode)
        {
            case 1:
                //player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                player.GetComponent<PlayerController>().isInFlashback = true;
                player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = true;
                break;
            case 0:
                player.GetComponent<PlayerController>().isInFlashback = false;
                player.transform.localRotation = Quaternion.identity;

                camera.GetComponent<PostProcessingBehaviour>().enabled = false;
                //player.transform.position = new Vector3(playerTransform.position.x - distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
        }
    }
}
