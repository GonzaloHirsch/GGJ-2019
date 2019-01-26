using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItemText : InteractableItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        StartCoroutine(WaitforText());
    }

    public override IEnumerator WaitforText()
    {
        textLabel.gameObject.SetActive(true);
        textLabel.text = textToShow;

        yield return new WaitForSeconds(this.timeToRead);

        textLabel.gameObject.SetActive(false);
    }
}
