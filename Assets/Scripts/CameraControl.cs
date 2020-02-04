using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed;
    public float maxdistance;
    public Rigidbody2D rb;
    public GameObject cam;
    public GameObject player;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    private bool followPlayer;

    private float posY;
    private float newposY;

    // hacer que la camara siga al jugador de manera por defecto.
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        followPlayer = true;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        // Si el jugador decide empezar a moverse mientras esta manipulando la camara automaticamente se redirije a su posicion.
        if (followPlayer || Input.GetButton("Horizontal"))
        {
            transform.position = player.transform.position + offset;
        }
    }


    void Update()
    {
        //Si el jugador esta en movimiento, no se activa el movimiento de la camara.  
        //Si se activa la camara y posteriormente se empieza a andar, la cámara se queda bloqueada durante un rato.
        if (rb.velocity.x < 1)
        {
            newposY = transform.position.y;
            //al pulsar el las flechas abajo y arriba la camara del PJ se desplaza hacia dicha dirección.
            if (Input.GetKey(KeyCode.DownArrow))
            {
                followPlayer = false;
                cam.transform.Translate(new Vector2(0, -speed * Time.deltaTime));

            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                followPlayer = false;
                cam.transform.Translate(new Vector2(0, speed * Time.deltaTime));
            }

            //En cuanto se pulsa alguna de ambas flechas, se almacena la posicion de la camara.
            if (Input.GetButtonDown("Flechas"))
            {
                posY = cam.transform.position.y;
            }


            //hacer que cuando se suelta el tecla Up Arrow/DownArrow la camara vuelva a la poscion 0,0,0.
            if (Input.GetButtonUp("Flechas"))
            {
                followPlayer = true;
            }


            //hacer que cuando la camara llegue a la distancia máxima se quede ahí y no se mueva.
            if (posY - newposY >= maxdistance || posY - newposY <= -maxdistance)
            {
                speed = 0;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                speed = 10;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                speed = 10;
            }
        }
        else
            transform.position = player.transform.position + offset;


    }
}
