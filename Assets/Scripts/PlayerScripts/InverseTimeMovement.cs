using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseTimeMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 30;
    public bool startMoveIT = false;//para saber si el inverse time esta activo
    public GameObject animatedSpriteUI;
    public GameObject background;
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animatedSpriteUI.SetActive(false);
        background.SetActive(false);
        startMoveIT = false;
        GlobalController.Instance.moveIT = false;
    }

    public void StartMovingIT()
    {
        startMoveIT = true;
        GlobalController.Instance.moveIT = true;
        animatedSpriteUI.SetActive(true);
        background.SetActive(true);
        animatedSpriteUI.GetComponent<AnimatedSpriteUI>().enabled = true;
        animatedSpriteUI.GetComponent<AnimatedSpriteUI>().Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (animatedSpriteUI.GetComponent<AnimatedSpriteUI>().started)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //player.GetComponent<CapsuleCollider2D>().enabled = false;
        }


        if (GlobalController.Instance.inverseTimeActive == false)
        {
            GetComponent<InverseTime>().clockImage.SetActive(false);
            //GetComponent<InverseTime>().cooldnText.gameObject.SetActive(false);
            //GetComponent<InverseTime>().cooldown.SetActive(false);
        }
        else
        {
            GetComponent<InverseTime>().clockImage.SetActive(true);
            //GetComponent<InverseTime>().cooldnText.gameObject.SetActive(true);
            //GetComponent<InverseTime>().cooldown.SetActive(true);
        }

        end = animatedSpriteUI.GetComponent<AnimatedSpriteUI>().ended;
        if (startMoveIT && end)
        {
            //player.GetComponent<CharacterController2D_Mod>().state = player.GetComponent<CharacterController2D_Mod>().state;
            GetComponent<InverseTime>().enabled = false;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * speed);

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.GetComponent<CapsuleCollider2D>().enabled = false;

            if (player.transform.position == transform.position)
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 4;
                player.GetComponent<CapsuleCollider2D>().enabled = true;
                GetComponent<InverseTime>().enabled = true;
                startMoveIT = false;
                GlobalController.Instance.moveIT = false;
                animatedSpriteUI.SetActive(false);
                background.SetActive(false);
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                animatedSpriteUI.GetComponent<AnimatedSpriteUI>()._CurrentFrame = 0;
                animatedSpriteUI.GetComponent<AnimatedSpriteUI>().ended = false;
                animatedSpriteUI.GetComponent<AnimatedSpriteUI>().mImage.sprite = animatedSpriteUI.GetComponent<AnimatedSpriteUI>().mSprites[0];
                var col = animatedSpriteUI.GetComponent<AnimatedSpriteUI>().mImage.color;
                col.a = 1f;
                animatedSpriteUI.GetComponent<AnimatedSpriteUI>().mImage.color = col;
            }
        }
    }
}
