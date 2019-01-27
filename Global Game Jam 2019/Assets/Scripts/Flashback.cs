using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashback : MonoBehaviour
{
    public Material outlineMaterial;

    public int flashBackId;
    public GameObject[] flashbackObjects;
    public InteractableItemFlashback[] interactObjectsComponents;
    public MeshRenderer[] interactObjMaterials;
    public Material[] oldMaterials;

    private int flashbackObjIndex;

    public void SetUpFlashback() {
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

    public void nextStep() {
        //cambiar mi material
        //flashbackObjects[flashbackObjIndex].GetComponent<MeshRenderer>().material = oldMeshRenderers[flashbackObjIndex].material;
        interactObjMaterials[flashbackObjIndex].material = oldMaterials[flashbackObjIndex];
        //deshabilitar mi interacción
        Destroy(interactObjectsComponents[flashbackObjIndex]);
        //interactObjectsComponents[flashbackObjIndex].enabled = false;
        //habilitar la interacción del siguiente obj
        //cambiar el material
        if (flashbackObjIndex + 1 >= interactObjMaterials.Length)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().MovePlayer(0);
        }
        else
        {
            interactObjectsComponents[flashbackObjIndex + 1].enabled = true;
            interactObjMaterials[++flashbackObjIndex].material = outlineMaterial;
        }
    }

}
