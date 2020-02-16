using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public bool isMoving = false;
    public bool moveUp = false;
    public int speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            ElevatorMove();
        }

    }

    void ElevatorMove()
    {
        // y de elevator = -55
        isMoving = true;
        //se plataforma se mueve
        if (transform.position.y >= -23)
        {
            moveUp = false;
            isMoving = false;

        }
        if (transform.position.y <= -55)
        {
            moveUp = true;
            isMoving = false;
        }

        if (moveUp)
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && Input.GetButton("Interact"))
        {
           
            isMoving = true;
        }
    }
}
