using System.Collections;
using UnityEngine;

public class InteractableItemText : InteractableItem
{
    public override void Interact() { StartCoroutine(WaitforText()); }

    public override IEnumerator WaitforText()
    {
        textLabel.gameObject.SetActive(true);
        textLabel.text = textToShow;

        yield return new WaitForSeconds(this.timeToRead);

        textLabel.gameObject.SetActive(false);
    }
}
