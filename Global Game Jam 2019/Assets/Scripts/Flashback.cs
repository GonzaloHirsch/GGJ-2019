using System.Collections;
using UnityEngine;

public class Flashback : MonoBehaviour
{
    public GameObject door;
    public Material outlineMaterial;
    public int flashBackId;

    public GameObject[] flashbackObjects;
    public float[] shaderWidths;
    [HideInInspector]
    public InteractableItemFlashback[] interactObjectsComponents;
    [HideInInspector]
    public MeshRenderer[] interactObjMaterials;
    [HideInInspector]
    public Material[] oldMaterials;

    private int flashbackObjIndex;
    private DoorRotator rotator;

    public void SetUpFlashback() {
        //Locking up the door
        if (door != null)
        {
            this.rotator = door.GetComponent<DoorRotator>();
            if (rotator.isOpen) 
            {
                rotator.interacted = true;
                StartCoroutine(DoorRotating());
            }
        }

        interactObjectsComponents = new InteractableItemFlashback[flashbackObjects.Length];
        interactObjMaterials = new MeshRenderer[flashbackObjects.Length];
        oldMaterials = new Material[flashbackObjects.Length];

        for (int i = 0; i < flashbackObjects.Length; i++) {
            interactObjectsComponents[i] = flashbackObjects[i].GetComponent<InteractableItemFlashback>();
            interactObjMaterials[i] = flashbackObjects[i].GetComponent<MeshRenderer>();
            oldMaterials[i] = flashbackObjects[i].GetComponent<MeshRenderer>().material;
            if ( i != 0) {
                interactObjectsComponents[i].enabled = false;
            } else {
                interactObjMaterials[i].material = outlineMaterial;
            }
        }
        flashbackObjIndex = 0;
        //acá trabar la salida
    }

    IEnumerator DoorRotating()
    {
        //rotator.OpenDoor();
        yield return new WaitForSeconds(3f);
        rotator.canOpen = false;
    }

    public void nextStep() {
        //cambiar mi material
        //flashbackObjects[flashbackObjIndex].GetComponent<MeshRenderer>().material = oldMeshRenderers[flashbackObjIndex].material;

        //Cambia al material que tenia antes
        interactObjMaterials[flashbackObjIndex].material = oldMaterials[flashbackObjIndex];

        //deshabilitar mi interacción
        Destroy(interactObjectsComponents[flashbackObjIndex]);

        //interactObjectsComponents[flashbackObjIndex].enabled = false;
        //habilitar la interacción del siguiente obj
        //cambiar el material
        //Ver que no se te pasa del indice del array
        if (flashbackObjIndex + 1 >= interactObjMaterials.Length)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().MovePlayer(0);

            //destraba la puerta y la abre
            if (door != null)
            {
                rotator.canOpen = true;
                rotator.interacted = true;
                //StartCoroutine(DoorRotating());
            }
        }
        else
        {
            //cambia el material del siguiente
            interactObjectsComponents[flashbackObjIndex + 1].enabled = true;
            outlineMaterial.SetFloat("Outline width", shaderWidths[flashbackObjIndex + 1]);
            interactObjMaterials[++flashbackObjIndex].material = outlineMaterial;
        }
    }
}
