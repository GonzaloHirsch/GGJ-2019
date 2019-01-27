using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemSecret : InteractableItem
{

    public override void Interact() {
        
        return;
    }

    public override IEnumerator WaitforText() {
        throw new System.Exception();
    }


}
