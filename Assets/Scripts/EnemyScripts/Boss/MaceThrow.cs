using UnityEngine;
using System.Collections;

public class MaceThrow : MonoBehaviour
{

    public GameObject mace;
    public Animator anim;
    public bool throwMace = false;
    private GameObject player;
    public GameObject boss;
    public float speed;
    private Transform ground;
    Vector2 target;


    void Start()
    {
        player = GameObject.Find("SrBeta1");
        ground = GameObject.Find("GroundPoint").GetComponent<Transform>();
    }
    private void Update()
    {
        if (throwMace)
        {
            anim.SetBool("Throw", true);
            mace.transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if (!throwMace)
        {
            anim.SetBool("Throw", false);
            mace.transform.position = new Vector2(boss.transform.position.x, boss.transform.position.y);
        }
    }
    public void GetTarget()
    {      
        if (player.transform.position.x > boss.transform.position.x) target = new Vector2(player.transform.position.x + 2, ground.position.y);                             //creates a new vector with at posistion behind the player, to throw the mace there.
        else if (player.transform.position.x < boss.transform.position.x) target = new Vector2(player.transform.position.x - 2, ground.position.y);
        else if (player.transform.position.x == boss.transform.position.x) target = new Vector2(player.transform.position.x, ground.position.y);
    }
}