using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 10f;
    public float playerHeightNormal = 1.9f;
    public float playerHeightFlashback = 5f;
    public Camera camera;
    public GameObject crosshair;
    public float interactDistance = 3f;
    public Text eInteractText;
    public float mouseSensitivity = 1;
    public bool isInFlashback = false;
    
    public float maxAngle = 30;

    private float movementX = 0f;
    private float movementY = 0f;
    private float xAxisClamp = 0.0f;
    private Transform playerTransform;

    private const float EPSILON = 0.0000000001f;
    private Vector2 lastCursorPosition;

    private InteractableItem itemToInteract;

    
    void Start()
    {
        this.playerTransform = gameObject.transform;
    }

    
    void Update()
    {
        UpdateMovement();
        UpdateRotation();
        this.itemToInteract = CheckIfInteractable();

        if (itemToInteract != null && Input.GetKeyDown(KeyCode.E))
        {
            this.itemToInteract.Interact();
        }
    }

    //PLayer movement
    private void UpdateMovement()
    {
        this.movementX = Input.GetAxisRaw("Horizontal");
        this.movementY = Input.GetAxisRaw("Vertical");

        if (System.Math.Abs(this.movementX) > EPSILON)
        {
            this.playerTransform.position += (this.playerTransform.right.normalized * this.walkingSpeed * Time.deltaTime * movementX);
        }
        if (System.Math.Abs(this.movementY) > EPSILON)
        {
            this.playerTransform.position += (this.playerTransform.forward.normalized * this.walkingSpeed * Time.deltaTime * movementY);
        }

        //Mantain player height
        this.playerTransform.position = new Vector3(playerTransform.position.x, isInFlashback ? playerHeightFlashback : playerHeightNormal, playerTransform.position.z);
    }

    //Player camera rotation
    /* private void UpdateRotation()
     {
         float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
         float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
         transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), mouseX * 360f, transform.localRotation.z));

         //if (Mathf.Abs(transform.localRotation.x) > 0.5)
         //{
         //    transform.localRotation = new Quaternion(Mathf.Sign(transform.localRotation.x) * 0.5f, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
         //}
         //if (Mathf.Abs(transform.localRotation.y) > 0.5)
         //{
         //    transform.localRotation = new Quaternion(transform.localRotation.x, Mathf.Sign(transform.localRotation.y) * 0.5f, transform.localRotation.z, transform.localRotation.w);
         //}

         //Debug.Log(transform.localRotation);
         //transform.LookAt(camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)), Vector3.up);
     }*/

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
        Physics.Raycast(transform.position, playerTransform.forward, out hit, interactDistance);
        InteractableItem collision = null;
        //Debug.DrawRay(transform.position, playerTransform.forward, Color.green);
        if (hit.collider != null)
        {
            collision = hit.collider.gameObject.GetComponent<InteractableItem>();
            
            if (collision != null) {
                eInteractText.gameObject.SetActive(true);
            } else {
                eInteractText.gameObject.SetActive(false);
            }
        }else {
            eInteractText.gameObject.SetActive(false);
        }
        return collision;
    }
}
