﻿using System.Collections;
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

    void SetUpFlashback() {
        interactObjectsComponents = new InteractableItemText[flashbackObjects.Length];
        for(int i = 0; i < flashbackObjects.Length; i++) {
            interactObjectsComponents[i] = flashbackObjects[i].GetComponent<InteractableItemText>();
            interactObjMaterials[i] = flashbackObjects[i].GetComponent<MeshRenderer>();
            oldMeshRenderers[i] = flashbackObjects[i].GetComponent<MeshRenderer>();
        }
        for(int i = 1; i < flashbackObjects.Length; i++) {
            interactObjectsComponents[i].enabled = false;
        }
        flashbackObjIndex = 0;
    }

    void nextStep() {
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
