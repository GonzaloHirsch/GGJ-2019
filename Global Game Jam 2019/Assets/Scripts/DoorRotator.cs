using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    public bool hingeIsLeftFromInside = false;
    public GameObject hinge;

    public bool canOpen = true;
    public bool isOpen = false;
    public bool interacted = false;
    private float timeToOpen = 0;

    private Quaternion oldRotation;

    public void Start()
    {
        oldRotation = hinge.transform.rotation;
    }

    public void Update()
    {
        if (interacted )
        {
            OpenDoor();
            interacted = false;
        }
    }

    public void OpenDoor()
    {
        if (canOpen)
        {
            if (!isOpen)
            {
                if (hingeIsLeftFromInside)
                {
                    hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(0, -90, 0), 1);
                }
                else
                {
                    hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(0, 90, 0), 1);
                }
            }
            else
            {
                hinge.transform.rotation = oldRotation;
            }
            isOpen = !isOpen;
        }
    }
}
