using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlants : MonoBehaviour
{
    private Quaternion oldRotation;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        oldRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        time += 0.25f;
        transform.Rotate(new Vector3(0,0,(Mathf.Sin(time) * 35f) - 15f));
        transform.localRotation = Quaternion.Euler(oldRotation.ToEuler().x, oldRotation.ToEuler().y, oldRotation.ToEuler().z + Mathf.Sin(time) * 35f);
        //transform.rotation += Quaternion.Euler(0, 0, 45f * Mathf.Sin(time));
    }
}
