using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public int LifeBar = 2;
    public Transform skeleton;
    public Transform Apoint;
    public Transform Bpoint;
    public CharacterController2D Control;
    private float direction;
    public float hSpd = 10;
    public DetectPlayer playerDetection;
    // Start is called before the first frame update
    void Start()
    {
        direction = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Control.Move(hSpd * direction * Time.fixedDeltaTime, false, false);
        if (!playerDetection.detected)
        {
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided");
        if (other.gameObject.tag == "Weapon")
        {
            Debug.Log("damaged");
            LifeBar -= 1;
            if (LifeBar <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else if (other.gameObject.tag == "DeathTrap")
        {
            Destroy(this.gameObject);
        }
    }
}
