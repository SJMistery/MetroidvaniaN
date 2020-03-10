using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * 
 * COMO SE ESTA UTILIZANDO CINEMACHINE, ESTE SCRIPT ESTA EN DESHUSO, PENDIENTE DE SER BORRADO.
 * 
 * 
 */
public class CameraControl : MonoBehaviour
{
    public float speed;
    public float maxdistance = 6;
    private Rigidbody2D rb;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject player;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    private bool followPlayer;

    private float posY;
    private float newposY;

    private CharacterController2D_Mod cc;
    // hacer que la camara siga al jugador de manera por defecto.
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        followPlayer = true;
        rb = GameObject.Find("SrBeta1").GetComponent<Rigidbody2D>();

        cc = GameObject.Find("SrBeta1").GetComponent<CharacterController2D_Mod>();
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        // Si el jugador decide empezar a moverse mientras esta manipulando la camara automaticamente se redirije a su posicion.
        if (followPlayer || Mathf.Abs(rb.velocity.x) > 1 || Mathf.Abs(rb.velocity.y) > 1)
        {
            transform.position = player.transform.position + offset;
        }
    }


    void Update()
    {
        //Si el jugador esta en movimiento, no se activa el movimiento de la camara.  
        //Si se activa la camara y posteriormente se empieza a andar, la cámara se queda bloqueada durante un rato.
        if (Mathf.Abs(rb.velocity.x) < 1 || Mathf.Abs(rb.velocity.y) < 1)
        {
            newposY = transform.position.y;
            //al pulsar el las flechas abajo y arriba la camara del PJ se desplaza hacia dicha dirección.
            if (cc.controller && (Mathf.Abs(Input.GetAxis("Flechas MANDO")) > 0.3))
            {

                float moveCam = Input.GetAxisRaw("Flechas MANDO");
                followPlayer = false;
                float temp = moveCam * speed * Time.deltaTime;
                cam.transform.Translate(new Vector2(0, temp));

            }
            else if ((!cc.controller) && Input.GetButton("Flechas"))
            {
                float moveCam = Input.GetAxisRaw("Flechas");
                followPlayer = false;
                float temp = moveCam * speed * Time.deltaTime;
                cam.transform.Translate(new Vector2(0, temp));
            }

            //En cuanto se pulsa alguna de ambas flechas, se almacena la posicion de la camara.
            if ((cc.controller) && (Mathf.Abs(Input.GetAxis("Flechas MANDO")) > 0.5) || ((!cc.controller) && Input.GetButtonDown("Flechas")))
            {
                followPlayer = false;
            }


            //hacer que cuando se suelta el tecla Up Arrow/DownArrow la camara vuelva a la poscion 0,0,0.
            if ((cc.controller) && (Mathf.Abs(Input.GetAxis("Flechas MANDO")) <= 0.5) || ((!cc.controller) && Input.GetButtonUp("Flechas")))
            {
                followPlayer = true;
                posY = cam.transform.position.y;
                speed = 10;
            }

            //hacer que cuando la camara llegue a la distancia máxima se quede ahí y no se mueva.
            if (Mathf.Abs(posY - newposY) > maxdistance)
            {
                speed = 0;
            }

        }
        else
            transform.position = player.transform.position + offset;


    }
}
