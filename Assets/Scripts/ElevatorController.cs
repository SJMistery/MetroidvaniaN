using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public bool isMoving = false;
    public bool moveUp = false;
    public int speed;
    private CharacterController2D_Mod cc;
    // [SerializeField] private Transform childTransform;


    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
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
        if (other.gameObject.tag == "Player" && ((cc.controller && Input.GetButton("Interact MANDO")) || ((!cc.controller) && Input.GetButton("Interact"))))
        {
           
            isMoving = true;
        }
    }
    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(childTransform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
    */
}
