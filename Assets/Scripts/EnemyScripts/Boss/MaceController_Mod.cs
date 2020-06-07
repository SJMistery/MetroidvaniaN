using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceController_Mod: MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public GameObject mace;
    public bool grounded = false;
    private GameObject player;
    public GameObject boss;
    public float speed;
    private Transform ground;
    public bool move = false;
    public Vector2 target;
    public GameObject maceExplosionSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("SrBeta1");
        ground = GameObject.Find("GroundPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grounded)
        {
            rb.bodyType = RigidbodyType2D.Static;
            anim.SetBool("isGrounded", true);

        }
        if (move)
        {
            MoveToTarget(); 
        }
   }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //cuando la maza toca el suelo, se deja de mover 
        if (collision.name == "Ground")
        {
            grounded = true;
        }
        if (grounded && collision.name == "BOSS"){
            Destroy(this.gameObject);
        }
    }
    public void GetTarget()
    {
        if (player.transform.position.x > boss.transform.position.x) target = new Vector2(player.transform.position.x + 2, ground.position.y);                             //creates a new vector with at posistion behind the player, to throw the mace there.
        else if (player.transform.position.x < boss.transform.position.x) target = new Vector2(player.transform.position.x - 2, ground.position.y);
        else if (player.transform.position.x == boss.transform.position.x) target = new Vector2(player.transform.position.x, ground.position.y);
    }

    public void MoveToTarget()
    {
        Vector2 newPosition;
        newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPosition);

    }
     public void DetroyThis()
    {
        Destroy(this.gameObject);
    }
}




