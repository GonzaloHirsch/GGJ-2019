using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemSecret : InteractableItem
{

    public override void Interact() {
        DG.Tweening.DOTweenModulePhysics.DOMoveX(gameObject.GetComponent<Rigidbody>(), -5f, 3f);
    }

    public override IEnumerator WaitforText() {
        throw new System.Exception();
    }


}
