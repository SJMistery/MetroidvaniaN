using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DoorsController : MonoBehaviour
{

    public Vector3 movement;
    public bool canMove = true;

    // Start is called before the first frame update


    public void Move()
    {
        if (canMove)
        {
            this.transform.position += movement;
            canMove = false;
        }
    }
}
