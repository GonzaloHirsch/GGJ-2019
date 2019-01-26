using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 10f;

    private float movementX = 0f;
    private float movementY = 0f;
    private Transform playerTransform;

    private const float EPSILON = 0.0000000001f;

    // Start is called before the first frame update
    void Start()
    {
        this.playerTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        this.movementX = Input.GetAxisRaw("Horizontal");
        this.movementY = Input.GetAxisRaw("Vertical");

        if (System.Math.Abs(this.movementX) > EPSILON)
        {
            this.playerTransform.position += (this.playerTransform.right.normalized * this.walkingSpeed * Time.deltaTime);
        }
        if (System.Math.Abs(this.movementY) > EPSILON)
        {
            this.playerTransform.position += (this.playerTransform.forward.normalized * this.walkingSpeed * Time.deltaTime);
        }

    }
}
