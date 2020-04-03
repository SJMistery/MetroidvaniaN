using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEverything : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shadow;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
    }

    // Update is called once per frame
    void Update()
    {
       /* if (GlobalController.Instance.stopAll)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                //enemies[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                enemies[i].GetComponent<EnemyController2D>().StopMovement();
            }
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<CharacterController2D_Mod>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;

            if (GlobalController.Instance.inverseTimeActive)
                shadow.GetComponent<InverseTime>().enabled = false;
        }*/
    }
}
