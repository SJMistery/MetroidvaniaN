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
        end = animatedSpriteUI.GetComponent<AnimatedSpriteUI>().ended;
        if (startMoveIT && end)
        {
            player.GetComponent<CharacterController2D_Mod>().state = player.GetComponent<CharacterController2D_Mod>().state;
            GetComponent<InverseTime>().enabled = false;
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, Time.deltaTime * speed);

            if (player.transform.position == transform.position)
            {
                GetComponent<InverseTime>().enabled = true;
                startMoveIT = false;
                GlobalController.Instance.moveIT = false;
                animatedSpriteUI.SetActive(false);
                background.SetActive(false);
                player.GetComponent<Rigidbody2D>().simulated = true;
                player.GetComponent<PlayerMovement>().colliders[0].enabled = true;
                player.GetComponent<PlayerMovement>().colliders[1].enabled = true;

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
