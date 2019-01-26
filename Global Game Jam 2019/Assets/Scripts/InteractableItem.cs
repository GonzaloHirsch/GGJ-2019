using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractableItem : MonoBehaviour
{
    public GameObject gameController;
    public Text textLabel;
    public string textToShow;
    public float timeToRead = 5f;

    public abstract void Interact();

    public abstract IEnumerator WaitforText();

}
