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
    public DetectPlayer detector;
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
        bool chase = detector.detected;
        if (Control.currentHP > 0)
        {
            

            if(!chase){
                Control.Move(((float)hSpd * direction * Time.fixedDeltaTime), false, false);
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
            else Control.Move(((float)hSpd * direction * 2 * Time.fixedDeltaTime), false, false);
            animator.SetBool("Moving", true);

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
        else if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Attack", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Attack", false);
        }
    }
}
