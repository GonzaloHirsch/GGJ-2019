using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public bool isFlashback = false;
    //Distance to the alternate model
    public float distanceToFlashbackModel = 32f;

    public GameObject player;
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


        this.playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Hacer que renderee y desrenderee el modelo que no se esta usando
    private void MovePlayer(string mode = "ToFlashback")
    {
        switch (mode)
        {
            case "ToFlashback":
                player.transform.position = new Vector3(playerTransform.position.x + distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
            case "ToNormal":
                player.transform.position = new Vector3(playerTransform.position.x - distanceToFlashbackModel, playerTransform.position.y, playerTransform.position.z);
                break;
        }
    }
}
