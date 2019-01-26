using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 10f;
    public float playerHeight = 1.9f;
    public Camera camera;

    private float movementX = 0f;
    private float movementY = 0f;
    private Transform playerTransform;

    private const float EPSILON = 0.0000000001f;
    private Vector2 lastCursorPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.playerTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateRotation();
    }

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
        this.playerTransform.position = new Vector3(playerTransform.position.x, playerHeight, playerTransform.position.z);
    }

    private void UpdateRotation()
    {
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), mouseX * 360f, transform.localRotation.z));
        //transform.LookAt(camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)), Vector3.up);
    }
}
