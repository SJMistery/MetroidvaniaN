using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Vector3 startPos;
    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("e")) && (transform.position == startPos))
        {
            transform.position = transform.position + movement;
        }
        else if ((Input.GetKeyDown("e")) && (transform.position == (startPos + movement)))
        {
            transform.position = transform.position - movement;
        }

    }
}
