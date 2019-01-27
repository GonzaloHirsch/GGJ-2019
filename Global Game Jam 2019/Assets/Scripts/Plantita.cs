using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantita : InteractableItem
{
    public GameObject player;
    public float speed = 1.5f;
    public AudioSource source;

    private bool isAlive = false;
    private bool isMoving = false;

    private PlayerController controller;
    

    public override void Interact()
    {
        isAlive = !isAlive;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public override IEnumerator WaitforText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //controller = player.GetComponent<PlayerController>();
    }

    private void OnBecameVisible()
    {
        isMoving = false;
        source.Pause();
    }

    private void OnBecameInvisible()
    {
        isMoving = true;
        source.Play();
        source.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && isMoving)
        {
            UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        transform.LookAt(player.transform.position, Vector3.up);
        transform.position += (transform.forward.normalized * speed * Time.deltaTime);
    }
}
