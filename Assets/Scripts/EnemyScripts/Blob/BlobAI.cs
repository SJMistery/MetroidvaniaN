using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : MonoBehaviour
{
    public EnemyController2D Control;
    private Animator animator;
    public Transform leftPoint;
    public Transform rightPoint;


    private int direction;
    public float hSpd = 10f;



    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        Control = GetComponent<EnemyController2D>();
        direction = -1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Control.currentHP > 0)
        {
            animator.SetBool("Moving", true);
            Control.Move(((float)hSpd * direction * Time.fixedDeltaTime), false, false);


            if (rightPoint.position.x > transform.position.x)
            {
                direction = 1;
                //Debug.Log("left");
            }
            if (leftPoint.position.x < transform.position.x)
            {
                direction = -1;
                //Debug.Log("Right");
            }
        }
        else animator.SetBool("Moving", false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            direction = -direction;
        }
    }
}
