using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public int LifeBar = 2;
    public Transform skeleton;
    public Transform Apoint;
    public Transform Bpoint;
    public EnemyController2D Control;
    private int direction;
    public float hSpd = 10f;
    public DetectPlayer playerDetection;
    private BoxCollider2D hitbox;
    // Start is called before the first frame update
    void Start()
    {
        EnemyController2D Control = GetComponent<EnemyController2D>();
        hitbox = GameObject.Find("Espada").GetComponent<BoxCollider2D>();
        direction = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Control.Move(((float)hSpd * direction * Time.fixedDeltaTime), false, false);
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
        if (other.gameObject.tag == "Weapon" && hitbox.enabled)
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
        else if (other.gameObject.tag == "Enemy")
        {
            direction = -direction;
        }
    }
}
