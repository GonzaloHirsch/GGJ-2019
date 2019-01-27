﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool isAlive = false;

    public float walkingSpeed = 10f;
    public float playerHeightNormal = 1.9f;
    public float playerHeightFlashback = 5f;
    public Camera camera;
    public GameObject crosshair;
    public float interactDistance = 30f;
    public Text eInteractText;
    public float mouseSensitivity = 1;
    public bool isInFlashback = false;

    public GameObject raycastOut;

    public float maxAngle = 30;

    private float movementX = 0f;
    private float movementY = 0f;
    private float xAxisClamp = 0.0f;
    private Transform playerTransform;

    private const float EPSILON = 0.0000000001f;
    private Vector2 lastCursorPosition;

    private InteractableItem itemToInteract;
    private DoorRotator doorToRotate;

    public GameObject gamecontroller;

    void Start()
    {
        this.playerTransform = gameObject.transform;
    }

    
    void Update()
    {
        if (isAlive)
        {
            UpdateMovement();
            UpdateRotation();
            this.itemToInteract = CheckIfInteractable();
            this.doorToRotate = CheckIfDoor();

            if (itemToInteract != null && itemToInteract.enabled && Input.GetKeyDown(KeyCode.E))
            {
                this.itemToInteract.Interact();
            }
            if (doorToRotate != null && itemToInteract.enabled && Input.GetKeyDown(KeyCode.E))
            {
                this.doorToRotate.interacted = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        //Debug.Log("ohlasldfaskdgnsdhfgvjhsdvfgsdf");
        int id = other.gameObject.GetComponent<Flashback>().flashBackId;
        gamecontroller.GetComponent<GameController>().MovePlayer(id);
    }

    //PLayer movement
    private void UpdateMovement()
    {
        this.movementX = Input.GetAxisRaw("Horizontal");
        this.movementY = Input.GetAxisRaw("Vertical");

        this.playerTransform.position += (this.playerTransform.right.normalized * this.walkingSpeed * Time.deltaTime * movementX);
        this.playerTransform.position += (this.playerTransform.forward.normalized * this.walkingSpeed * Time.deltaTime * movementY);
        //if (System.Math.Abs(this.movementX) > EPSILON)
        //{
        //    this.playerTransform.position += (this.playerTransform.right.normalized * this.walkingSpeed * Time.deltaTime * movementX);
        //}
        //if (System.Math.Abs(this.movementY) > EPSILON)
        //{
        //    this.playerTransform.position += (this.playerTransform.forward.normalized * this.walkingSpeed * Time.deltaTime * movementY);
        //}

        //Mantain player height
        this.playerTransform.position = new Vector3(playerTransform.position.x, isInFlashback ? playerHeightFlashback : playerHeightNormal, playerTransform.position.z);
        this.playerTransform.position = new Vector3(playerTransform.position.x, playerHeightNormal, playerTransform.position.z);
    }

    private void UpdateRotation() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        
        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotCam.y += rotAmountX;

        if (xAxisClamp > maxAngle) {
            xAxisClamp = maxAngle;
            targetRotCam.x = maxAngle;
        } else if (xAxisClamp < -maxAngle) {
            xAxisClamp = -maxAngle;
            targetRotCam.x = 360 - maxAngle;
        }


        transform.rotation = Quaternion.Euler(targetRotCam);


    }

    private InteractableItem CheckIfInteractable()
    {
        RaycastHit hit;
        Physics.Raycast(raycastOut.transform.position, playerTransform.forward, out hit, interactDistance);
        InteractableItem component = null;
        //Debug.DrawRay(transform.position, playerTransform.forward, Color.green);
        if (hit.collider != null)
        {
            component = hit.collider.gameObject.GetComponent<InteractableItem>();
            
            if (component != null && component.enabled) {
                eInteractText.gameObject.SetActive(true);
            } else {
                eInteractText.gameObject.SetActive(false);
            }
        }else {
            eInteractText.gameObject.SetActive(false);
        }
        return component;
    }

    private DoorRotator CheckIfDoor()
    {
        RaycastHit hit;
        Physics.Raycast(raycastOut.transform.position, playerTransform.forward, out hit, interactDistance);
        DoorRotator component = null;
        Debug.DrawRay(raycastOut.transform.position, playerTransform.forward, Color.green);
        if (hit.collider != null)
        {
            component = hit.collider.gameObject.GetComponent<DoorRotator>();
            //if (component == null || !component.canOpen)
            //{
            //    return null;
            //}
        }
        //Debug.Log(hit.collider.gameObject);
        return component;
    }
}
