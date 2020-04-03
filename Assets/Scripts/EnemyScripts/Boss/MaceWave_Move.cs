using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceWave_Move : MonoBehaviour
{
    public float waveSpeed = 50f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * waveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Walls" || collision.gameObject.name == "SrBeta1")
        {
            Destroy(this.gameObject);
        }

    }

}
