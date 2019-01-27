using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Flashback : MonoBehaviour
{
    public Material outlineMaterial;

    public int flashBackId;
    public GameObject[] flashbackObjects;
    public InteractableItemText[] interactObjectsComponents;
    public MeshRenderer[] interactObjMaterials, oldMeshRenderers;

    private int flashbackObjIndex;

    public void SetUpFlashback() {
        interactObjectsComponents = new InteractableItemText[flashbackObjects.Length];
        for(int i = 0; i < flashbackObjects.Length; i++) {
            interactObjectsComponents[i] = flashbackObjects[i].GetComponent<InteractableItemText>();
            interactObjMaterials[i] = flashbackObjects[i].GetComponent<MeshRenderer>();
            if( i != 0) {
                interactObjectsComponents[i].enabled = false;
            }else {
                interactObjMaterials[i].material = outlineMaterial;
            }
            oldMeshRenderers[i] = flashbackObjects[i].GetComponent<MeshRenderer>();
        }
        flashbackObjIndex = 0;
        //acá trabar la salida
    }

    public void nextStep() {
        //cambiar mi material
        interactObjMaterials[flashbackObjIndex] = oldMeshRenderers[flashbackObjIndex];
        //deshabilitar mi interración
        interactObjectsComponents[flashbackObjIndex].enabled = false;
        //habilitar la interacción del siguiente obj
        interactObjectsComponents[flashbackObjIndex + 1].enabled = true;
        //cambiar el material
        interactObjMaterials[flashbackObjIndex++].material = outlineMaterial;
    }

}
