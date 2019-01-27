using UnityEngine;

public class DoorRotator : MonoBehaviour
{
    //public bool hingeIsLeftFromInside = false;
    public GameObject hinge;
    //public GameObject door;

    public bool canOpen = true;
    public bool isOpen = false;
    public bool interacted = false;
    private float timeToOpen = 1.5f;
    private float timeOpening = 0f;

    private Quaternion oldRotation;

    public GameObject musicManagerObject;

    private MusicManager musicManager;

    public void Start()
    {
        //guarda la rotacion vieja para asignarla despues
        oldRotation = hinge.transform.rotation;
        //oldRotation = hinge.transform.localRotation;

        //Cache music manager
        //this.musicManager = musicManagerObject.GetComponent<MusicManager>();
    }

    /*
     *  El update se fija si alguien interactuo con la puerta.
     *  Si puede abrirse, se abre pero mantiene el estado de apertura asi se hace bien la animacion
     *  NO TOCAR, FUNCIONA.
     *  SI LO TOCAN SE ROMPE
     */  

    public void Update()
    {
        if (interacted && canOpen) { OpenDoor(); }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            //if (hingeIsLeftFromInside)
            //{
            //    hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(0, -90, 0), 0.01f);
            //}
            //else
            //{
            //door.transform.localRotation = Quaternion.Slerp(hinge.transform.localRotation, Quaternion.Euler(0, -90, 0), 0.1f);
            //timeOpening += Time.deltaTime;

            hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, Quaternion.Euler(0, 90, 0), 0.05f);
            timeOpening += Time.deltaTime;
            //}
            //musicManager.doorMovement.Play();
        }
        else
        {
            //door.transform.localRotation = Quaternion.Slerp(hinge.transform.localRotation, oldRotation, 0.1f);
            hinge.transform.rotation = Quaternion.Slerp(hinge.transform.rotation, oldRotation, 0.05f);
            timeOpening += Time.deltaTime;
        }
        //Una vez que termino de abrirse, cambia las variables
        if (timeOpening > timeToOpen)
        {
            isOpen = !isOpen;
            interacted = false;
            timeOpening = 0f;
        }
    }
}
