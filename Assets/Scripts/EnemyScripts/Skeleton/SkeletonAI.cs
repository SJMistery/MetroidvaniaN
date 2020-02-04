using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{

    public Animator animator;
    public Transform skeleton;
    public Transform Apoint;
    public Transform Bpoint;
    public EnemyController2D Control;

    private int direction;
    public float hSpd = 10f;

    // Start is called before the first frame update
    void Start()
    {

        Control = GetComponent<EnemyController2D>();
        direction = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Control.currentHP > 0)
        {
            Control.Move(((float)hSpd * direction * Time.fixedDeltaTime), false, false);
            animator.SetBool("Moving", true);

            if (Apoint.position.x > skeleton.position.x)
            {
                direction = 1;
                //Debug.Log("left");
            }
            if (Bpoint.position.x < skeleton.position.x)
            {
                direction = -1;
                //Debug.Log("Right");
            }
        }
        else
            animator.SetBool("Moving", false);
              
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathTrap")
        {
            
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else if (other.gameObject.tag == "Enemy")
        {
            direction = -direction;
        }
    }
}
